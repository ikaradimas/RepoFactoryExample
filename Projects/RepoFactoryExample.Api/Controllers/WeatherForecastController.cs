using Microsoft.AspNetCore.Mvc;
using RepoFactoryExample.Data.Infrastructure;
using RepoFactoryExample.Data.Interface.Entities;
using RepoFactoryExample.Data.Interface.Infrastructure;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepositoryFactory _repositoryFactory;

    public WeatherForecastController(IRepositoryFactory repositoryFactory, ILogger<WeatherForecastController> logger, RepoFactoryExampleDbContext dbContext)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
        _dbContext = dbContext;
    }
    
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly RepoFactoryExampleDbContext _dbContext;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var todoRepository = _repositoryFactory.GetRepository<ITodoItemRepository>();
        todoRepository!.CreateAsync(new TodoItem()).Wait();
        var allTodoItems = todoRepository.ListAsync().Result;
        var all2 = _dbContext.TodoItems.ToList();
        
        if (allTodoItems.Count != all2.Count || allTodoItems[0] != all2[0])
            throw new Exception();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
