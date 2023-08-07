using Microsoft.EntityFrameworkCore;
using RepoFactoryExample.Data.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<RepoFactoryExampleDbContext>(
    options => options.UseInMemoryDatabase("ExampleDb"));

builder.Services.AddRepositories();
builder.Services.AddRepositoryFactory();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
