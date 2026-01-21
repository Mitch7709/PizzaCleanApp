using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaCleanApp.Core.Features.Users;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Infrastructure.Database;
using PizzaCleanApp.Infrastructure.Identity;

namespace PizzaCleanApp.UnitTests.TestSetup;

internal class IdentityTestSetup : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ServiceProvider _provider;

    public AppDbContext DbContext { get; }
    public UserManager<AppUser> UserManager { get; }
    public RoleManager<IdentityRole> RoleManager { get; }
    public ITokenService TokenService { get; }
    public IUserService UserService { get; }

    public IdentityTestSetup()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(_connection));

        services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        // JwtOptions for TokenService; use simple test values
        var jwtOptions = new JwtOptions
        {
            Key = "test-secret-key-that-is-long-enough-for-hs256",
            Issuer = "test-issuer",
            Audience = "test-audience",
            ExpirationInDays = 1
        };

        services.AddSingleton(jwtOptions);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        _provider = services.BuildServiceProvider();

        DbContext = _provider.GetRequiredService<AppDbContext>();
        DbContext.Database.EnsureCreated();

        UserManager = _provider.GetRequiredService<UserManager<AppUser>>();
        RoleManager = _provider.GetRequiredService<RoleManager<IdentityRole>>();
        TokenService = _provider.GetRequiredService<ITokenService>();
        UserService = _provider.GetRequiredService<IUserService>();

        // Ensure basic roles exist for tests that add "User" role on registration
        EnsureRoleExists("User").GetAwaiter().GetResult();
        EnsureRoleExists("Admin").GetAwaiter().GetResult();
    }

    private async Task EnsureRoleExists(string roleName)
    {
        if (!await RoleManager.RoleExistsAsync(roleName))
        {
            await RoleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    public void Dispose()
    {
        DbContext?.Dispose();
        _provider?.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}