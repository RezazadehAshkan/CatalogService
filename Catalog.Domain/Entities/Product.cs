namespace Catalog.Domain;

public sealed record Product
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required string Sku { get; init; }
}