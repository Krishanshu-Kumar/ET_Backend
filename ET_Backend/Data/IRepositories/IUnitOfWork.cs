using System;
using System.Threading.Tasks;

public interface IUnitOfWork : IDisposable
{
    // IRepo<YourEntity> YourEntityRepo { get; }
    Task<int> SaveAsync();
}
