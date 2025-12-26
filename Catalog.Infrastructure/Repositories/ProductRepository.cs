using Catalog.Application.Interfaces;
using Catalog.Domain;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(CatalogDbContext db) : base(db) { }

    public async Task AddAsync(Product product) => await _db.Products.AddAsync(product);

    public async Task<Product?> GetByIdAsync(Guid id) => await _db.Products.FindAsync(id);
}