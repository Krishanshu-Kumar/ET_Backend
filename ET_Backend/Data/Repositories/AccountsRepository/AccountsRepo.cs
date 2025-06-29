using ET_Backend.Models;
using ET_Backend.Data.IRepositories.IAccountsRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ET_Backend.Models.AccountsModel;

namespace ET_Backend.Data.Repositories.AccountsRepository;

public class AccountsRepo : Repository<AccountsModel>, IAccountsRepo
{
    private readonly AppDbContext dBcontext;

    public AccountsRepo(AppDbContext context) : base(context)
    {
        dBcontext = context;
    }

    // public async Task<User> UpdateAccount(string username)
    // {
    //     return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    // }

}
