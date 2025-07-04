using System;
using System.Threading.Tasks;
using ET_Backend.Data.IRepositories.IAuthRepo;
using ET_Backend.Data.IRepositories.IAccountsRepository;

namespace ET_Backend.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IAuthRepository Auth { get; }
    IAccountsRepo Accounts { get; }
    int Complete();
    Task<int> CompleteAsync();
}
