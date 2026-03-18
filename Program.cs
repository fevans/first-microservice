using Catalog.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

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
builder.Services.AddSingleton<IItemRepository, InMemoryRepository>();


var app = builder.Build();

// configure http request pipeline
app.UseAuthorization();
app.MapControllers();

app.Run();
