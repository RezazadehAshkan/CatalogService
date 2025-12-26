using Catalog.Application.Interfaces;
using Catalog.Domain;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(CatalogDbContext db) : IProductRepository
{
    public async Task AddAsync(Product product) => await db.Products.AddAsync(product);
    public async Task SaveChangesAsync() => await db.SaveChangesAsync();
}