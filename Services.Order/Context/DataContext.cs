using Microsoft.EntityFrameworkCore;
using Services.Order.Models;

namespace Services.Order.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<OrderEntity> Order { get; set; }
    }
}
