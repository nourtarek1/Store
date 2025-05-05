namespace Domian.Models.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem Product, int quantity, decimal Price)
        {
            this.Product = Product;
            this.Quantity = quantity;
            this.Price = Price;
        }

        public ProductInOrderItem? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}