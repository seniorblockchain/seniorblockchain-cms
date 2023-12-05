using powerpage.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace powerpage.DataLayer.InMemoryDatabase;

public class InMemoryDatabaseContext : ApplicationDbContext
{
    public InMemoryDatabaseContext(DbContextOptions options) : base(options)
    {
    }
}