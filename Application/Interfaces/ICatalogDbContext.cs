using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface ICatalogDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Item> Items { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
