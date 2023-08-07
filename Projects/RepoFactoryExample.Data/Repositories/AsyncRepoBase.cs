using Microsoft.EntityFrameworkCore;
using RepoFactoryExample.Data.Infrastructure;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Data.Repositories;

/// <summary>
/// This class represents a way for us to have most generic operations implemented within a "base" repository,
/// avoiding repeated and varying implementations of common tasks.
///
/// Note that we use <code><![CDATA[_dbContext.Set<TEntity>()]]></code> to get a set through which we can manipulate 
/// what EF tracks, instead of the property registered in the context class itself. This doesn't matter, since all sets
/// within a scoped session share the same objects, so we can use either the generic method above, or the typed
/// DBSet registered in the context with the same exact effect.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
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