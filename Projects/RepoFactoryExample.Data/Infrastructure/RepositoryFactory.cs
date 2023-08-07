using System.Reflection;
using RepoFactoryExample.Data.Interface.Infrastructure;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Data.Infrastructure;

/// <summary>
/// This factory bridges the standard service collection which we use to manage our instances and their lifetime to
/// a simple interface through which we can request specific types; here it is used to retrieve repositories,
/// avoiding the need for multiple repository injections to repository consumers.
/// </summary>
public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// This determines which types are allowed to be retrieved directly from the container.
    /// Presumably, we don't want a direct, unfiltered line to the container available at any given time, since it
    /// can be abused.
    /// 
    /// It's static because we ideally want it instantiated and populated once across all instances of this class,
    /// since we use reflection, which is kind of expensive. We also want it front-loaded; we don't want to have to
    /// "warm up" the service separately to populate it the first time.
    /// </summary>
    private static List<Type>? _allowedTypes;
    
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _allowedTypes ??= Assembly.GetAssembly(typeof(IAsyncRepoBase<>))!.GetTypes()
            .Where(x => x is { IsInterface: true, IsGenericType: false, Namespace: not null }
                        && x.Namespace.Contains("Repositories"))
            .ToList();
    }

    public TRepository? GetRepository<TRepository>() where TRepository : class
    {
        if (_allowedTypes!.All(x => x != typeof(TRepository))) return null;
        return (_serviceProvider.GetService(typeof(TRepository)) as TRepository)!;
    }
}