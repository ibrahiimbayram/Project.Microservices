using Job.Producer.Models.Order;
using Job.Producer.Models.Product;
using Newtonsoft.Json;
using Quartz;
using System.Text;

namespace Job.Producer
{
    public class TasksJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataProduct = await GetProduct();

            if (dataProduct.Count < 10)
            {
                var productNames = new List<string>() { "Laptop", "Phone", "Router", "Glasses", "Freezer", "Bag", "Hour", "Necklace", "Headphone", "Mouse" };

                for (int a = 0; a < 10; a++)
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var stringChars = new char[8];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    var randomQuantity = new Random();
                    var quantityValue = randomQuantity.Next(5000, 8000);
                    var model = new ProductModel
                    {
                        Name = productNames[a],
                        Code = finalString,
                        Quantity = quantityValue
                    };

                    await CreateProduct(model);
                }
                   Console.WriteLine("Started");
                Console.WriteLine("Products Added");

            }

            var afterCreatingProductGetData = await GetProduct();

            if (afterCreatingProductGetData.Count != 0)
            {
                foreach (var item in afterCreatingProductGetData)
                {
                    var dataOrder = await GetOrders();

                    var lastOrderNumber = dataOrder?.Count == 0 ? new OrderModel { OrderNumber = "0" } : dataOrder?.Last();

                    var createOrderNumber = Convert.ToInt32(lastOrderNumber?.OrderNumber);

                    var rnd = new Random();

                    int orderQuantity = rnd.Next(1, 50);

                    var model = new OrderModel
                    {
                        OrderNumber = (createOrderNumber + 1).ToString(),
                        ProductCode = item.Code,
                        Quantity = orderQuantity
                    };

                    await CreateOrder(model);
                }



                Thread.Sleep(20);

                Console.WriteLine("Orders Added");

                int c = 0;
                while (c < 3)
                {
                    var orderList = await GetOrders();

                    var lastOrder = orderList?.Last();

                    Random rand = new Random();

                    int randomId = rand.Next(1, 4);

                    int id = lastOrder.Id - randomId;

                    var getOrderByIdDetail = await GetByIdOrder(id);

                    var random = new Random();

                    int orderQuantity = random.Next(1, 50);

                    var updateModel = new OrderModel
                    {
                        Id = getOrderByIdDetail.Id,
                        OrderNumber = getOrderByIdDetail.OrderNumber,
                        Quantity = orderQuantity,
                        ProductCode = getOrderByIdDetail.ProductCode
                    };

                    await UpdateOrder(updateModel);

                    c++;
                }
                Thread.Sleep(20);
                int b = 0;
                while (b < 3)
                {
                    var orderList = await GetOrders();

                    var lastOrder = orderList?.Last();

                    Random rand = new Random();

                    int randomId = rand.Next(5, 9);

                    int id = lastOrder.Id - randomId;
                    await DeleteOrder(id);

                    b++;
                }

            }

            await StartConsumerApp();

        }
        public async Task<OrderModel> GetByIdOrder(int id)
        {
            HttpClient client = new HttpClient();

            var getOrder = await client.GetAsync($"http://order:6000/api/order/{id}");

            var OrderContent = await getOrder.Content.ReadAsStringAsync();

            var orderJsonConvert = JsonConvert.DeserializeObject<OrderModel>(OrderContent);

            return orderJsonConvert;
        }

        public async Task<string> UpdateOrder(OrderModel model)
        {
            HttpClient client = new HttpClient();

            var jsonOrderModel = JsonConvert.SerializeObject(model);

            var createOrder = new StringContent(jsonOrderModel, Encoding.UTF8, "application/json");

            await client.PutAsync("http://order:6000/api/order", createOrder);

            return "succes";
        }
        public async Task<string> DeleteOrder(int id)
        {
            HttpClient client = new HttpClient();


            await client.DeleteAsync($"http://order:6000/api/order/{id}");


            return "succes";
        }
        public async Task<string> CreateOrder(OrderModel model)
        {
            HttpClient client = new HttpClient();

            var jsonOrderModel = JsonConvert.SerializeObject(model);

            var createOrder = new StringContent(jsonOrderModel, Encoding.UTF8, "application/json");

            await client.PostAsync("http://order:6000/api/order", createOrder);


            return "succes";
        }

        public async Task<List<OrderModel>> GetOrders()
        {
            HttpClient client = new HttpClient();

            var getOrder = await client.GetAsync("http://order:6000/api/order");

            var OrderContent = await getOrder.Content.ReadAsStringAsync();

            var orderJsonConvert = JsonConvert.DeserializeObject<List<OrderModel>>(OrderContent);

            return orderJsonConvert;
        }

        public async Task<string> CreateProduct(ProductModel model)
        {
            HttpClient client = new HttpClient();

            var jsonProductModel = JsonConvert.SerializeObject(model);

            var createProduct = new StringContent(jsonProductModel, Encoding.UTF8, "application/json");

            await client.PostAsync("http://product:5000/api/product", createProduct);


            return "succes";
        }

        public async Task<List<ProductModel>> GetProduct()
        {
            HttpClient client = new HttpClient();

            var product = await client.GetAsync("http://product:5000/api/product");

            var content = await product.Content.ReadAsStringAsync();

            var jsonConvert = JsonConvert.DeserializeObject<List<ProductModel>>(content);

            return jsonConvert;
        }

        public async Task<string> StartConsumerApp()
        {
            HttpClient client = new HttpClient();
            client.GetAsync("http://job.consumer.api:4000/api/consumer/incoming-order");
            HttpClient client2 = new HttpClient();
            client2.GetAsync("http://job.consumer.api:4000/api/consumer/canceled-order");
            HttpClient client3 = new HttpClient();
            client3.GetAsync("http://job.consumer.api:4000/api/consumer/update-order");
            return "succes";
        }
    }
}