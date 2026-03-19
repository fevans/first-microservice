using Catalog.Service.Domain;
using Catalog.Service.Repositories;
using Catalog.Service.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Catalog.Service;

public static class ConfigServiceCollectionExtensions
{
    /// <summary>
    /// Configures core service dependencies for the application, including controllers and API behavior options.
    /// This method ensures consistent API responses for invalid model states.
    /// </summary>
    /// <param name="services">The service collection to which the core service configurations will be added.</param>
    /// <returns>The updated service collection with core services configured.</returns>
    public static IServiceCollection AddConfigService(this IServiceCollection services)
    {
        services.AddControllers()
    
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .ToDictionary(
                            e => e.Key,
                            e => e.Value!.Errors
                                .Select(x => x.ErrorMessage)
                                .ToArray()
                        );
                    return new BadRequestObjectResult(new
                    {
                        Title = "Validation failed",
                        Status = 400,
                        Errors = errors
                    });
                };
            });
        
        
        
        return services;
    }

    /// <summary>
    /// Adds MongoDB-related services to the service collection. This includes the configuration
    /// of MongoDB client, database, and repository for managing domain entities.
    /// </summary>
    /// <param name="services">The service collection to which the MongoDB services will be added.</param>
    /// <param name="configuration">The application configuration instance used to retrieve MongoDB settings.</param>
    /// <returns>The updated service collection with MongoDB services configured.</returns>
    public static IServiceCollection AddMongoDbService(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
        
        services.AddSingleton<IMongoClient>(_ =>
            new MongoClient(mongoDbSettings.ConnectionString));

        services.AddSingleton<IRepository<Item>>(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var database = mongoClient.GetDatabase("Catalog");
            return new MongoDbRepository<Item>(database, "items");
        });
        
        return services;
    }
}