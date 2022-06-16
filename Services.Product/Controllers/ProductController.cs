using Microsoft.AspNetCore.Mvc;
using Services.Product.Models;
using Services.Product.Repository;

namespace Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await productRepository.GetAll());
        }

        [HttpPost]
        public async Task<string> Post(ProductEntity productEntity)
        {
            if (productEntity == null)
                return "error";
            await productRepository.AddAsync(productEntity);
            return "succes";
        }

        [HttpPut]
        public async Task<string> Put(ProductEntity productEntity)
        {
            if (productEntity == null)
                return "error";
            await productRepository.UpdateAsync(productEntity);
            return "succes";
        }

        [HttpDelete]
        public async Task<string> Delete(ProductEntity productEntity)
        {
            if (productEntity == null)
                return "error";
            await productRepository.DeleteAsync(productEntity);
            return "succes";
        }

    }
}