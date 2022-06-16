using System.Threading.Tasks;
using TestAPI.DAL.Entities;

namespace TestAPI.DAL.Functions.Interfaces
{
    public interface IAuth
    {
        Task<User> ReadUser(string email, string password);
    }
}
