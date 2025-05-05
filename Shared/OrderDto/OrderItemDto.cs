namespace Shared.OrderDto
{
    public class OrderItemDto
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
    }
}