using System.Collections.Concurrent;
using Catalog.Service.Domain;

//namespace Catalog.Service.Repositories;
//
// public class InMemoryRepository : IItemRepository
// {
//         //private readonly List<Item> _items = [];
//     
//         public InMemoryRepository()
//         {
//             var item1 = new Item
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Potion",
//                 Description = "Restores a small amount of HP",
//                 Price = 9,
//                 CreatedDate = DateTimeOffset.UtcNow
//             };
//             _items[item1.Id] = item1;
//               
//             var id = Guid.NewGuid();
//             _items[id] = (new Item
//             {
//                 Id = id,
//                 Name = "Iron Sword",
//                 Description = "Deals a moderate amount of damage",
//                 Price = 20,
//                 CreatedDate = DateTimeOffset.UtcNow
//             });
//             
//             id = Guid.NewGuid();
//             _items[id] = new Item
//             {
//                 Id = Guid.NewGuid(),
//                 Name = "Bronze Shield",
//                 Description = "Provides a moderate amount of protection",
//                 Price = 18,
//                 CreatedDate = DateTimeOffset.UtcNow
//             };
//         }
//         private readonly ConcurrentDictionary<Guid, Item> _items = new();
//         
//
//         public Task<IReadOnlyCollection<Item>> GetAllAsync() =>
//             Task.FromResult((IReadOnlyCollection<Item>)_items.Values
//                 .OrderBy(x => x.Name)
//                 .ToArray());
//
//         public Task<Item?> GetAsync(Guid id)
//         {
//             _items.TryGetValue(id, out var item);
//             return Task.FromResult(item);
//         }
//
//         public Task CreateAsync(Item item)
//         {
//             _items[item.Id] = item;
//             return Task.CompletedTask;
//         }
//
//         public Task UpdateAsync(Item item)
//         {
//             _items[item.Id] = item;
//             return Task.CompletedTask;
//         }
//
//         public Task DeleteAsync(Guid id)
//         {
//             _items.TryRemove(id, out _);
//             return Task.CompletedTask;
//         }
//
// }