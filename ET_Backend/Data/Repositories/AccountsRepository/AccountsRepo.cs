using ET_Backend.Data.IRepositories.IAccountsRepository;
using Microsoft.EntityFrameworkCore;
using ET_Backend.Models.AccountsModel;
using ET_Backend.DTOs.AccountsDTO;

namespace ET_Backend.Data.Repositories.AccountsRepository;

public class AccountsRepo : Repository<AccountsModel>, IAccountsRepo
{
    private readonly AppDbContext dBcontext;

    public AccountsRepo(AppDbContext context) : base(context)
    {
        dBcontext = context;
    }

    public async Task<bool> UpdateAccount(AccountsReqDTO req, Guid rowid, string userid)
    {
        var result = await dBcontext.Accounts
            .Where(prop => prop.Id == rowid)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Name, p => req.Name)
                .SetProperty(p => p.Type, p => req.Type)
                .SetProperty(p => p.Balance, p => req.Balance)
                .SetProperty(p => p.Currency, p => req.Currency)
                .SetProperty(p => p.ModifiedBy, p => userid)
                .SetProperty(p => p.ModifiedDate, p => DateTime.UtcNow)
            );
        return result > 0;
    }
    public async Task<bool> DeleteAccount(Guid id, string userId)
    {
        var account = await dBcontext.Accounts.FirstOrDefaultAsync(a => a.Id == id);

        if (account == null)
            return false;

        dBcontext.Accounts.Remove(account);
        await dBcontext.SaveChangesAsync();
        return true;
    }



}
