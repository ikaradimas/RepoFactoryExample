using Microsoft.EntityFrameworkCore;
using RepoFactoryExample.Data.Infrastructure;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Data.Repositories;

public abstract class AsyncRepoBase<TEntity> : IAsyncRepoBase<TEntity> where TEntity : class
{
    private readonly RepoFactoryExampleDbContext _dbContext;
    
    protected AsyncRepoBase(RepoFactoryExampleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken ct = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<IList<TEntity>> ListAsync(CancellationToken ct = default)
    {
        return await _dbContext.Set<TEntity>().ToListAsync(ct);
    }
}