using ET_Backend.Models;
using ET_Backend.Data.IRepositories.IAuthRepo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ET_Backend.Data.Repositories
{
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // public async Task<User> GetByUsernameAsync(string username)
        // {
        //     return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        // }

        // public async Task<bool> ValidateCredentialsAsync(string username, string password)
        // {
        //     var user = await GetByUsernameAsync(username);
        //     return user != null && user.Password == password;
        // }
    }
}
