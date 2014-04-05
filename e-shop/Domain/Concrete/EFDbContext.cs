using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } 
    }
}
