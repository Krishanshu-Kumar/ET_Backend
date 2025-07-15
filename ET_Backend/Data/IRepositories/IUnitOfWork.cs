using ET_Backend.Data.IRepositories.IAuthRepo;
using ET_Backend.Data.IRepositories.IAccountsRepository;
using ET_Backend.Data.IRepositories.ICategoriesRepository;
using ET_Backend.Data.IRepositories.IExpensesRepository;

namespace ET_Backend.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IAccountsRepo Accounts { get; }
    ICategoriesRepo Categories { get; }
    IExpensesRepo Expenses { get; }
    int Complete();
    Task<int> CompleteAsync();
}
