using System.Threading.Tasks;
using Api.Models;

namespace Api.Data
{
    public interface IAuthRepo
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);

    }
}