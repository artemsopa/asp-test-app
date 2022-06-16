using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestAPI.DAL.DataContext;
using TestAPI.DAL.Entities;
using TestAPI.DAL.Functions.Interfaces;

namespace TestAPI.DAL.Functions.Implementations
{
    public class Auth : IAuth
    {
        public async Task<User> ReadUser(string login, string password)
        {
            try
            {
                using (DatabaseContext context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
                {
                    return await context.Users.FirstOrDefaultAsync(u => u.Email == login || u.UserName == login && u.Password == password);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
