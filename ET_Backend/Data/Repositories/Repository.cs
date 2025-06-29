using ET_Backend.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ET_Backend.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetAsync(int id) => await _dbSet.FindAsync(id);

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.FirstOrDefaultAsync(predicate);

    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.SingleOrDefaultAsync(predicate);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(predicate).ToListAsync();

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null) =>
        predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);

    public void Add(T entity) => _dbSet.Add(entity);
    public void AddRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);
    public void Remove(T entity) => _dbSet.Remove(entity);
    public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);
    public void Update(T entity) => _dbSet.Update(entity);
}
