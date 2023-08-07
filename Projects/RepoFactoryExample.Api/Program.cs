using Microsoft.EntityFrameworkCore;
using RepoFactoryExample.Data.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context; registered by default as scoped.
// This here is a good explanation why: https://stackoverflow.com/a/37511175/366313
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
