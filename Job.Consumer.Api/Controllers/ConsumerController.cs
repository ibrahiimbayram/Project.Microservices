using Job.Consumer.Api.MessageQue;
using Microsoft.AspNetCore.Mvc;

namespace Job.Consumer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {

        private readonly IKafka kafka;

        public ConsumerController(IKafka kafka)
        {
            this.kafka = kafka;
        }

        [HttpGet("incoming-order")]
        public string IncomingOrder()
        {
            kafka.IncomingOrder();
            return "succes";
        }

        [HttpGet("update-order")]
        public string UpdateOrder()
        {
            kafka.UpdatedOrder();
            return "succes";
        }

        [HttpGet("canceled-order")]
        public string CanceledOrder()
        {
            kafka.CanceledOrder();
            return "succes";
        }
    }
}
