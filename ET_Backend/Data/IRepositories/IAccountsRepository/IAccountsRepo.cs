using ET_Backend.Models.AccountsModel;
using ET_Backend.DTOs.AccountsDTO;

namespace ET_Backend.Data.IRepositories.IAccountsRepository;

public interface IAccountsRepo : IRepository<AccountsModel>
{
    Task<bool> UpdateAccount(AccountsReqDTO req, Guid rowid, string userid);
    Task<bool> DeleteAccount(Guid id, string userId);
}

