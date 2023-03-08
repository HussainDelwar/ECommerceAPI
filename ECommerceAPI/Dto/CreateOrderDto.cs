namespace ECommerceAPI.Models
{
    public class CreateOrderDto
    {
        public CustomerDto Customer { get; set; }
        public IEnumerable<ProductOrderDto> ProductOrders { get; set; }
    }
}