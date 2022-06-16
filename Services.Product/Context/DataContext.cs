using Microsoft.EntityFrameworkCore;
using Services.Product.Models;

namespace Services.Product.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<ProductEntity> Product { get; set; }
    }
}
