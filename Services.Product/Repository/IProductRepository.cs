using Services.Product.Models;

namespace Services.Product.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAll();
        Task<ProductEntity> GetByProductCode(string code);
        Task AddAsync(ProductEntity entity);
        Task UpdateAsync(ProductEntity entity);
        Task DeleteAsync(ProductEntity entity);

    }
}
