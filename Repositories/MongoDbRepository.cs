using Catalog.Service.Domain;
using MongoDB.Driver;

namespace Catalog.Service.Repositories;

public class MongoDbRepository<T>(IMongoDatabase database, string collectionName) : IRepository<T> where T: IEntity
{
    private readonly IMongoCollection<T> dbCollection = database.GetCollection<T>(collectionName);
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public async Task<IReadOnlyCollection<T>> GetAllAsync() =>
        await dbCollection.Find(filterBuilder.Empty).ToListAsync();

    public async Task<T?> GetAsync(Guid id) =>
        await dbCollection.Find(filterBuilder.Eq(e => e.Id, id)).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await dbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await dbCollection.ReplaceOneAsync(filterBuilder.Eq(e => e.Id, entity.Id), entity);
    }

    public async Task RemoveAsync(Guid id) =>
        await dbCollection.DeleteOneAsync(filterBuilder.Eq(e => e.Id, id));
}