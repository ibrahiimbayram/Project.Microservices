using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Order.MessageQue;
using Services.Order.Models;
using Services.Order.Repository;
using System.Transactions;

namespace Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IKafka kafka;

        public OrderController(IOrderRepository productRepository, IKafka kafka)
        {
            this.orderRepository = productRepository;
            this.kafka = kafka;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await orderRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await orderRepository.GetById(id));
        }

        [HttpPost]
        public async Task<string> Post(OrderEntity orderEntity)
        {
            if (orderEntity == null)
                return "error";
            await orderRepository.AddAsync(orderEntity);
            var productModel = new ProductModel()
            {
                Code = orderEntity.ProductCode,
                Quantity = orderEntity.Quantity
            };
            kafka.IncomingOrder(productModel);
            return "succes";
        }

        [HttpPut]
        public async Task<string> Update(OrderEntity updateOrderEntity)
        {
            if (updateOrderEntity == null)
                return "error";
            var productmodel = new ProductModel()
            {
                Code = updateOrderEntity.ProductCode,
                Quantity = updateOrderEntity.Quantity
            };
            kafka.UpdatedOrderTransactions(updateOrderEntity.Id, productmodel);
            await orderRepository.UpdateAsync(updateOrderEntity);
            return "succes";
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            var response = await orderRepository.GetById(id);
            if (response != null)
            {
                var productModel = new ProductModel()
                {
                    Code = response.ProductCode,
                    Quantity = response.Quantity
                };
                kafka.CanceledOrder(productModel);
            }
            await orderRepository.DeleteAsync(id);
            return "succes";
        }

    }
}