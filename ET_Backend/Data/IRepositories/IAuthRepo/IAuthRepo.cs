using ET_Backend.Models;
using System.Threading.Tasks;

namespace ET_Backend.Data.IRepositories.IAuthRepo
{
    public interface IAuthRepository : IRepository<User>
    {
        // Task<User> GetByUsernameAsync(string username);
        // Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}

