namespace Catalog.Application.Dtos;

public record ProductDto(Guid Id, string Name, decimal Price, string Sku);