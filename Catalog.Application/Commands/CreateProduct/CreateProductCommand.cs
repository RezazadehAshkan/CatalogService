using MediatR;
using Catalog.Domain;
using Catalog.Application.Interfaces;

namespace Catalog.Application.Commands.CreateProduct;

// 1. The Command (DTO)
public sealed record CreateProductCommand(string Name, decimal Price, string Sku) : IRequest<Guid>;

// 2. The Handler (Business Logic)
public class CreateProductHandler(IProductRepository repository) 
    : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken token)
    {
        // Add business validation here if needed
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Sku = request.Sku
        };

        await repository.AddAsync(product);
        await repository.SaveChangesAsync();

        return product.Id;
    }
}