namespace ECommerceAPI.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        private List<ProductOrderDto> _productOrders;

        public IEnumerable<ProductOrderDto> productOrders => _productOrders;

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerAddressLine3 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerRegion { get; set; }

        public string CustomerPostcode { get; set; }
    }
}