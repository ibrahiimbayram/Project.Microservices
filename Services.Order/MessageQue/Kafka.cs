using Confluent.Kafka;
using Newtonsoft.Json;
using Services.Order.Models;
using Services.Order.Repository;
using System.Text;

namespace Services.Order.MessageQue
{
    public class Kafka : IKafka
    {
        private readonly IOrderRepository orderRepository;

        public Kafka(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async void IncomingOrder(ProductModel productModel)
        {

            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            try
            {
                await producer.ProduceAsync("IncomingOrder-topic", new Message<Null, string> { Value = JsonConvert.SerializeObject(productModel) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void CanceledOrder(ProductModel productModel)
        {

            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            try
            {
                await producer.ProduceAsync("CanceledOrder-topic", new Message<Null, string> { Value = JsonConvert.SerializeObject(productModel) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void UpdatedOrder(ProductModel productModel)
        {
            var config = new ProducerConfig { BootstrapServers = "kafka:9092" };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            try
            {
                await producer.ProduceAsync("UpdatedOrder-topic", new Message<Null, string> { Value = JsonConvert.SerializeObject(productModel) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async void UpdatedOrderTransactions(int id, ProductModel updateProductModel)
        {
            var orderDetail = await orderRepository.GetById(id);

            if (orderDetail.Quantity > updateProductModel.Quantity)
            {
                var productModel = new ProductModel()
                {
                    Code = updateProductModel.Code,
                    Quantity = orderDetail.Quantity - updateProductModel.Quantity,
                };
                UpdatedOrder(productModel);
            }
            if (orderDetail.Quantity < updateProductModel.Quantity)
            {
                var productModel = new ProductModel()
                {
                    Code = updateProductModel.Code,
                    Quantity = orderDetail.Quantity - updateProductModel.Quantity,
                };
                UpdatedOrder(productModel);
            }
        }
    }
}
