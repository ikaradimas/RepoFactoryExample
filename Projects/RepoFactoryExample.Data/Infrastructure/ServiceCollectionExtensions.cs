using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RepoFactoryExample.Data.Interface.Infrastructure;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Data.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Helper method to discover and register all repository interfaces and their implementations.
    ///
    /// Please note that since we have registered the db context through the Entity extension methods, which registers
    /// the db context as scoped, repositories and the repository factory must follow suit. 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var interfaceTypes = Assembly.GetAssembly(typeof(IAsyncRepoBase<>))!.GetTypes()
            .Where(x => x is { IsInterface: true, IsGenericType: false, Namespace: not null } 
                        && x.Namespace.Contains("Repositories"))
            .ToList();

        foreach (var interfaceType in interfaceTypes)
        {
            var implementation = Assembly.GetAssembly(typeof(RepoFactoryExampleDbContext))!
                .GetTypes()
                .Single(x => interfaceType.IsAssignableFrom(x));

            services.Add(new ServiceDescriptor(interfaceType, implementation, ServiceLifetime.Scoped));
        }

        return services;
    }

    /// <summary>
    /// Helper method to register the repository factory.
    ///
    /// Please note that since we have registered the db context through the Entity extension methods, which registers
    /// the db context as scoped, repositories and the repository factory must follow suit. 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRepositoryFactory(this IServiceCollection services)
    {
        services.Add(new ServiceDescriptor(typeof(IRepositoryFactory), typeof(RepositoryFactory),
            ServiceLifetime.Scoped));

        return services;
    }
}