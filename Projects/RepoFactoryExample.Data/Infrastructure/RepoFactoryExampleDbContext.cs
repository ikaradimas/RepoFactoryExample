using Microsoft.EntityFrameworkCore;
using RepoFactoryExample.Data.Interface.Entities;

namespace RepoFactoryExample.Data.Infrastructure;

public class RepoFactoryExampleDbContext : DbContext
{
    private readonly Dictionary<Type, object> _dbSets = new();

    public RepoFactoryExampleDbContext(DbContextOptions<RepoFactoryExampleDbContext> options) 
        : base(options) {}

    public DbSet<TodoItem> TodoItems { get; set; } = default!;
}