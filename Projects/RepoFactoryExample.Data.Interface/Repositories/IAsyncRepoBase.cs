namespace RepoFactoryExample.Data.Interface.Repositories;

public interface IAsyncRepoBase<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity entity, CancellationToken ct = default);
    Task<IList<TEntity>> ListAsync(CancellationToken ct = default);
}