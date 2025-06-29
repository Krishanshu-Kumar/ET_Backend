using ET_Backend.Models;
using ET_Backend.Models.AccountsModel;
using System.Threading.Tasks;

namespace ET_Backend.Data.IRepositories.IAccountsRepository;
public interface IAccountsRepo : IRepository<AccountsModel>
{
    // Task<bool> UpdateAccount();
}

