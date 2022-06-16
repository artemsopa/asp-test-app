using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAPI.DAL.Entities;
using TestAPI.DAL.Functions.Implementations;
using TestAPI.DAL.Functions.Interfaces;
using TestAPI.Logic.Models;
using TestAPI.Logic.Services.Interfaces;

namespace TestAPI.Logic.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAuth _auth = new Auth();
        private readonly ICrud _crud = new Crud();

        public async Task<User_ResultSet> AuthenticateUser(string login, string password)
        {
            try
            {
                User user = await _auth.ReadUser(login, password);

                User_ResultSet userAuthenticated = new User_ResultSet
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role.ToString()
                };

                return userAuthenticated;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User_ResultSet> AddUser(string username, string email, string password, string role)
        {
            try
            {
                User user = new User
                {
                    UserName = username,
                    Email = email,
                    Password = password,
                    Role = ((Role)Enum.Parse(typeof(Role), role, true)).ToString()
                };

                user = await _crud.Create<User>(user);

                User_ResultSet userAdded = new User_ResultSet
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role
                };

                return userAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
