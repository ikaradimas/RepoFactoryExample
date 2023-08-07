using Microsoft.AspNetCore.Mvc;
using RepoFactoryExample.Data.Interface.Entities;
using RepoFactoryExample.Data.Interface.Infrastructure;
using RepoFactoryExample.Data.Interface.Repositories;

namespace RepoFactoryExample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepositoryFactory _repositoryFactory;

    public WeatherForecastController(IRepositoryFactory repositoryFactory, ILogger<WeatherForecastController> logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var todoRepository = _repositoryFactory.GetRepository<ITodoItemRepository>();
        todoRepository.CreateAsync(new TodoItem()).Wait();
        var allTodoItems = todoRepository.ListAsync().Result;
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
