using Services.Order.Models;

namespace Services.Order.Repository
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> GetAll();
        Task<OrderEntity> GetById(int id);
        Task AddAsync(OrderEntity entity);
        Task UpdateAsync(OrderEntity entity);
        Task DeleteAsync(int id);
    }
}
