namespace ECommerceAPI.Models
{
    public class ProductOrder
    {
        public ProductOrder(Guid productId, int quantity, decimal price) 
        {
            if (quantity <= 0)
                throw new InvalidOperationException($"Invalid quantity {quantity} given for product {productId}.");

            if (price <= 0)
                throw new InvalidOperationException($"Invalid price {price} given for product {productId}.");

            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}