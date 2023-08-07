using RepoFactoryExample.Data.Interface.Infrastructure;

namespace RepoFactoryExample.Data.Infrastructure;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TRepository GetRepository<TRepository>() where TRepository : class
    {
        return (_serviceProvider.GetService(typeof(TRepository)) as TRepository)!;
    }
}