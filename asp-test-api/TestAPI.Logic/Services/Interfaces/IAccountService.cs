using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Logic.Models;

namespace TestAPI.Logic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User_ResultSet> AuthenticateUser(string email, string password);

        Task<User_ResultSet> AddUser(string username, string email, string password, string role);
    }
}
