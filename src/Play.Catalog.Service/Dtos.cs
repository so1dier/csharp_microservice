using System;

namespace Play.Catalog.Service.Dtos
{

    public record ItemDto(Guid id, string name, string description, decimal price, DateTimeOffset createdDate);
    public record CreateItemDto(string name, string description, decimal price);
    public record UpdateItemDto(string name, string description, decimal price);
}
