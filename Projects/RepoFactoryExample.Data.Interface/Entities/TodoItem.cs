namespace RepoFactoryExample.Data.Interface.Entities;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}