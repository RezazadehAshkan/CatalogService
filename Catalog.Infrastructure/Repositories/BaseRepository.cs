using Catalog.Domain;
using Catalog.Infrastructure.Persistence;

namespace Catalog.Infrastructure.Repositories;

public class BaseRepository
{
    protected readonly CatalogDbContext _db;

    public BaseRepository(CatalogDbContext db) => _db = db;

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}