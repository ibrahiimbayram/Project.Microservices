using Microsoft.EntityFrameworkCore;
using Job.Consumer.Api.Models;

namespace Job.Consumer.Api.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<OrderEntity> Order { get; set; }
    }
}
