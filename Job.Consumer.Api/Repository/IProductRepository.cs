using Job.Consumer.Api.Models;

namespace Job.Consumer.Api.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAll();
        ProductEntity GetByProductCode(string code);
        Task AddAsync(ProductEntity entity);
        void Update(ProductEntity entity);
        Task DeleteAsync(ProductEntity entity);

    }
}
