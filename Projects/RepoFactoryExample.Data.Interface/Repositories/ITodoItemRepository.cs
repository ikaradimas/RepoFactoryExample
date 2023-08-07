using RepoFactoryExample.Data.Interface.Entities;

namespace RepoFactoryExample.Data.Interface.Repositories;

public interface ITodoItemRepository : IAsyncRepoBase<TodoItem>
{
}