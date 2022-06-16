using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TestAPI.Auth.Options;
using TestAPI.Logic.Models;
using TestAPI.Logic.Services.Implementations;
using TestAPI.Logic.Services.Interfaces;
using TestAPI.WEB.Models.ViewModels;

namespace TestAPI.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IOptions<AuthOptions> options;
        private readonly IAccountService accountService = new AccountService();

        public AccountController(IOptions<AuthOptions> _options, IAccountService _accountService)
        {
            this.options = _options;
            this.accountService = _accountService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Registration([FromBody] RegisterViewModel user)
        {
            try
            {
                var result = await accountService.AddUser(user.UserName, user.Email, GenerateHash(user.PasswordConfirm), user.Role);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthViewModel request)
        {
            var user = await accountService.AuthenticateUser(request.Login, GenerateHash(request.Password));

            if (user != null)
            {
                var token = await Task.Run(() => GenerateJWT(user));

                return Ok(new
                {
                    accessToken = token,
                    role = user.Role
                });
            }

            return Unauthorized();
        }

        private static string GenerateHash(string value)
        {
            var salt = System.Text.Encoding.UTF8.GetBytes("1234asdqwe7r");
            var password = System.Text.Encoding.UTF8.GetBytes(value);

            var hmacMD5 = new HMACMD5(salt);
            var saltedHash = hmacMD5.ComputeHash(password);

            return Convert.ToBase64String(saltedHash);
        }

        private string GenerateJWT(User_ResultSet user)
        {
            var authParams = options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
