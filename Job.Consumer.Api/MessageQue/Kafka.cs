using Job.Consumer.Api.Repository;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Job.Consumer.Api.MessageQue
{
    public class Kafka : IKafka
    {
        private readonly IProductRepository productRepository;

        public Kafka(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public void IncomingOrder()
        {
            var config = new ConsumerConfig
            {
                GroupId = "IncomingOrder-consumer-group",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("IncomingOrder-topic");
            CancellationTokenSource token = new();
            try
            {
                while (true)
                {
                    var response = consumer.Consume(token.Token);
                    if (response.Message != null)
                    {
                        ReduceProductQuantity(response.Message.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ReduceProductQuantity(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<Model>(data);

            if (jsonData != null)
            {
                var response = productRepository.GetByProductCode(jsonData.Code);
                if (response != null)
                {
                    response.Quantity = response.Quantity - jsonData.Quantity;
                    productRepository.Update(response);
                }
            }
        }

        public void UpdatedOrder()
        {
            var config = new ConsumerConfig
            {
                GroupId = "UpdatedOrder-consumer-group",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("UpdatedOrder-topic");
            CancellationTokenSource token = new();
            try
            {
                while (true)
                {
                    var response = consumer.Consume(token.Token);
                    if (response.Message != null)
                    {
                        UpdateProductQuantity(response.Message.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateProductQuantity(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<Model>(data);

            if (jsonData != null)
            {
                var response = productRepository.GetByProductCode(jsonData.Code);
                if (response != null)
                {
                    response.Quantity = response.Quantity + (jsonData.Quantity);
                    productRepository.Update(response);
                }
            }
        }


        public void CanceledOrder()
        {
            var config = new ConsumerConfig
            {
                GroupId = "CanceledOrder-consumer-group",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("CanceledOrder-topic");
            CancellationTokenSource token = new();
            try
            {
                while (true)
                {
                    var response = consumer.Consume(token.Token);
                    if (response.Message != null)
                    {
                        AddProductQuantity(response.Message.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddProductQuantity(string data)
        {
            var jsonData = JsonConvert.DeserializeObject<Model>(data);

            if (jsonData != null)
            {
                var response = productRepository.GetByProductCode(jsonData.Code);
                if (response != null)
                {
                    response.Quantity = response.Quantity + jsonData.Quantity;
                    productRepository.Update(response);
                }

            }

        }
        public class Model
        {
            public string Code { get; set; }
            public int Quantity { get; set; }
        }
    }
}
