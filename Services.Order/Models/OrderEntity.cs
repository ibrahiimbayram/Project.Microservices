namespace Services.Order.Models
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public int Quantity { get; set; }
        public string? ProductCode { get; set; }
    }
}
