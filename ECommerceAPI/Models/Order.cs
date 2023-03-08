using System.Net.Mail;

namespace ECommerceAPI.Models
{
    public class Order
    {
        public Order(Guid customerId)
        {
            CustomerId = customerId;
            _productOrders = new List<ProductOrder>();
        }

        public Order(CustomerDto customer)
        {
            CustomerId = Guid.Empty;
            _productOrders = new List<ProductOrder>();
            CustomerName = customer.Name;
            CustomerEmail = ReturnEmailAddress(customer.Email);
            CustomerPhone = ReturnPhoneNumber(customer.Phone);
            //TODO validate address fields
            CustomerAddressLine1 = customer.AddressLine1;
            CustomerAddressLine2 = customer.AddressLine2;
            CustomerAddressLine3 = customer.AddressLine3;
            CustomerCity = customer.City;
            CustomerRegion = customer.Region;
            CustomerPostcode = customer.Postcode;
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        private List<ProductOrder> _productOrders;

        public IEnumerable<ProductOrder> ProductOrders => _productOrders;

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerAddressLine3 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerRegion { get; set; }

        public string CustomerPostcode { get; set; }

        public void AddProductOrder(Guid productId, int quantity, decimal price)
        {
            _productOrders.Add(new ProductOrder(productId, quantity, price * quantity));
        }

        public void AddCustomerDetails(Customer customer)
        {
            CustomerName = customer.Name;
            CustomerEmail = customer.Email;
            CustomerPhone = customer.Phone;
            CustomerAddressLine1 = customer.AddressLine1;
            CustomerAddressLine2 = customer.AddressLine2;
            CustomerAddressLine3 = customer.AddressLine3;
            CustomerCity = customer.City;
            CustomerRegion = customer.Region;
            CustomerPostcode = customer.Postcode;
        }

        public string ReturnEmailAddress(string email)
        {
            return new MailAddress(email).Address;
        }

        public string ReturnPhoneNumber(string phoneNumber) 
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, "GB");
            if (phoneNumberUtil.IsValidNumber(parsedPhoneNumber))
                return phoneNumberUtil.Format(parsedPhoneNumber, PhoneNumbers.PhoneNumberFormat.E164);
            else
                throw new Exception($"{phoneNumber} is not a valid number.");
        }
    }
}