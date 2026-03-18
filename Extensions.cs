using Catalog.Service.Dtos;
using Catalog.Service.Domain;

namespace Catalog.Service;

public static class Extensions
{
    public static ItemDto AsDto(this Item item) => 
        new(item.Id,
            item.Name,
            item.Description,
            item.Price,
            item.CreatedDate
        );
    
    
}