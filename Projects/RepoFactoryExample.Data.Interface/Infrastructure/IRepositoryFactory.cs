namespace RepoFactoryExample.Data.Interface.Infrastructure;

public interface IRepositoryFactory
{
    TRepository GetRepository<TRepository>() where TRepository : class;
}