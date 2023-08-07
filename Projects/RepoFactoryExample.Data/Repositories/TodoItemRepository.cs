using RepoFactoryExample.Data.Infrastructure;
using RepoFactoryExample.Data.Interface.Entities;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Data.Repositories;

public class TodoItemRepository : AsyncRepoBase<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(RepoFactoryExampleDbContext dbContext) : base(dbContext)
    {
    }
}