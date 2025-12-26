namespace Catalog.Application.Queries;
using Catalog.Application.Dtos;
using MediatR;
using Catalog.Application.Interfaces;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

// 2. The Handler (Business Logic)
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            return null;
        }

        return new ProductDto(product.Id, product.Name, product.Price, product.Sku);
    }
}