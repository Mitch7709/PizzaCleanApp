using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddCustomConfiguration(builder.Configuration)
                .AddDatabase()
                .AddDependencyInjection();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseDatabase()
    .UseMinimalApiEndpoints();

app.UseHttpsRedirection();

app.Run();

