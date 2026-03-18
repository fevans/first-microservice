namespace Catalog.Service.Dtos;

// Returned on Get Requests
public record ItemDto( 
    Guid Id, 
    string Name, 
    string Description, 
    decimal Price, 
    DateTimeOffset CreatedDate
    );