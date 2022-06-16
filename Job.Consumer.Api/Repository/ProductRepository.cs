using Microsoft.EntityFrameworkCore;
using Job.Consumer.Api.Context;
using Job.Consumer.Api.Models;

namespace Job.Consumer.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(ProductEntity entity)
        {
            context.Product.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductEntity entity)
        {
            context.Product.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<ProductEntity>> GetAll()
        {
            return await context.Product.ToListAsync();
        }

        public ProductEntity GetByProductCode(string code)
        {
            var response = context.Product.FirstOrDefault(x => x.Code == code);
            if (response == null)
                return new ProductEntity();
            return response;
        }

        public void Update(ProductEntity entity)
        {
            context.Product.Update(entity);
            context.SaveChanges();
        }
    }
}
