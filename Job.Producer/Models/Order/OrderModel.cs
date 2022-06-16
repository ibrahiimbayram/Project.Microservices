using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Producer.Models.Order
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int Quantity { get; set; }
        public string ProductCode { get; set; }
    }
}
