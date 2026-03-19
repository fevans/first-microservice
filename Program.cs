using Catalog.Service;
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
builder.Services.AddConfigService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongoDbService(builder.Configuration);

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
