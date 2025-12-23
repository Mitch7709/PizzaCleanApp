using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace PizzaCleanApp.UnitTests.TestSetup;

internal class DBBuilder : IDisposable
{
    private SqliteConnection? _connection;

    public IDbContext CreateDBContext()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        var dbContext = new AppDbContext(options);
        dbContext.Database.EnsureCreated();

        return dbContext;
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
