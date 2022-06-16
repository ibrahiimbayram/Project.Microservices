using Microsoft.EntityFrameworkCore;
using Services.Order.Context;
using Services.Order.Models;

namespace Services.Order.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(OrderEntity entity)
        {
            context.Order.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var response = context.Order.Find(id);
            if (response != null)
            {
                context.Remove(response);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderEntity>> GetAll()
        {
            return await context.Order.ToListAsync();
        }

        public async Task<OrderEntity> GetById(int id)
        {
            var response = await context.Order.FindAsync(id);
            return response;
        }

        public async Task UpdateAsync(OrderEntity entity)
        {
                context.Order.Update(entity);
                await context.SaveChangesAsync();

        }
    }
}
