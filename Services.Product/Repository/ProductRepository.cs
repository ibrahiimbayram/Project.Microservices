using Microsoft.EntityFrameworkCore;
using Services.Product.Context;
using Services.Product.Models;

namespace Services.Product.Repository
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

        public async Task<ProductEntity> GetByProductCode(string code)
        {
            return await context.Product.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task UpdateAsync(ProductEntity entity)
        {
            context.Product.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
