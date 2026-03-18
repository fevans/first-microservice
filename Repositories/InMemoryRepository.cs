using Catalog.Service.Domain;

namespace Catalog.Service.Repositories;

public class InMemoryRepository
{
        private readonly List<Item> _items = [];
    
        public InMemoryRepository()
        {
            _items.Add(new Item
            {
                Id = Guid.NewGuid(),
                Name = "Potion",
                Description = "Restores a small amount of HP",
                Price = 9,
                CreatedDate = DateTimeOffset.UtcNow
            });
            _items.Add(new Item
            {
                Id = Guid.NewGuid(),
                Name = "Iron Sword",
                Description = "Deals a moderate amount of damage",
                Price = 20,
                CreatedDate = DateTimeOffset.UtcNow
            });
            _items.Add(new Item
            {
                Id = Guid.NewGuid(),
                Name = "Bronze Shield",
                Description = "Provides a moderate amount of protection",
                Price = 18,
                CreatedDate = DateTimeOffset.UtcNow
            });
        }
    
        public IEnumerable<Item> GetAll() => _items;
       
        public Item? GetById(Guid id) => _items.SingleOrDefault(item => item.Id == id);
        
        public void Create(Item item) => _items.Add(item);
    
        public void Update(Item item)
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == item.Id);
            if (index > 0) _items[index] = item;
        }
    
        public void Delete(Guid id)
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == id);
            if (index > 0 ) _items.RemoveAt(index);
        }
}