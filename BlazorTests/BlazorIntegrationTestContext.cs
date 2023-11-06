using BlazorApp1.Data;
using Bunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace BlazorTests;

public class BlazorIntegrationTestContext : TestContext, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;

    public BlazorIntegrationTestContext()
    {
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithPassword("Strong_password_123!")
            .Build();

        Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
        Services.AddScoped<WeatherForecastService>();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var dbContext = Services.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
