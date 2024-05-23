using System.Diagnostics.CodeAnalysis;
using DotNet.Testcontainers.Builders;
using GymManagement.Api;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;

namespace GymManagement.Integration.Tests.SubscriptionTests;

public class SubscriptionApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("mydb")
        .WithUsername("workshop")
        .WithPassword("changeme")
        // .WithPortBinding("5555", "5432") let for choosing random port for many containers
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
        .Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(GymManagementDbContext));
            services.AddDbContext<GymManagementDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
                // options.UseNpgsql("Server=localhost;Port=5432;Database=mydb;User ID=course;Password=changeme;");
            });
        });
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}