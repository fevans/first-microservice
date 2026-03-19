using Catalog.Service.Domain;
using Catalog.Service.Repositories;
using Catalog.Service.Settings;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<IItemRepository, InMemoryRepository>();
var mongoDbSettings = builder.Configuration
    .GetSection(nameof(MongoDbSettings))
    .Get<MongoDbSettings>();

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoDbSettings.ConnectionString));

builder.Services.AddSingleton<IRepository<Item>>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase("Catalog");
    return new MongoDbRepository<Item>(database, "items");
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(_ => { });
    app.UseSwaggerUI();
}

// configure http request pipeline
app.UseAuthorization();
app.MapControllers();
app.Run();
