using Microsoft.EntityFrameworkCore;
using PizzaCleanApp.API.Extensions;
using PizzaCleanApp.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddHttpContextAccessor()
                .AddCustomConfiguration(builder.Configuration)
                .AddDatabase()
                .AddSecurity(builder.Configuration)
                .AddDependencyInjection();

builder.Services.AddControllers();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseCors(Security.CorsPolicy)
    .UseAuthentication()
    .UseAuthorization()
    .UseDatabase()
    .UseMinimalApiEndpoints();

app.UseHttpsRedirection();

app.Run();

