using System.Data.Entity;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
            public DbSet<ProductDescription> ProductDescriptions { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasRequired(a => a.ProductCategory);
              modelBuilder.Entity<Product>().HasRequired(a => a.ProductDescription);

        }

    }
}
