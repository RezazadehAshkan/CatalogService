using Catalog.Domain;

namespace Catalog.Application.Interfaces;

// The "Port". Application says: "I need a way to save products."
// Infrastructure will answer: "Here is the implementation."
public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
}