namespace Catalog.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Catalog.Application.Interfaces;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;

public static class ServiceCollectionExtensions
{
    public class DatabaseOptions
{
    public const string SectionName = "Database";
    public string ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; } = 3;
}
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddDbContext<CatalogDbContext>(opts => 
            opts.UseNpgsql(connectionString));
        return services;
    }
}