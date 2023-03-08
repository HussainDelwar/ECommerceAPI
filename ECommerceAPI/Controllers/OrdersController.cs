using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly StoreContext _context;

        public OrdersController(ILogger<OrdersController> logger, StoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("CreateOrder")]
        //Send Guid.Empty for guest customer id
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto order)
        {
            try
            {
                Customer exisitingCustomer = await _context.Customer.SingleOrDefaultAsync(c => c.Id == order.Customer.Id);
                Order entity;

                if (exisitingCustomer == null)
                {
                    entity = new Order(order.Customer);
                }
                else
                {
                    entity = new Order(exisitingCustomer.Id);
                    entity.AddCustomerDetails(exisitingCustomer);
                }

                foreach (ProductOrderDto productOrder in order.ProductOrders)
                {
                    Product product = await _context.Product.SingleOrDefaultAsync(c => c.Id == productOrder.ProductId);

                    if (product == null)
                    {
                        _logger.LogDebug($"Product {productOrder.ProductId} does not exist.");
                        return BadRequest($"Product {productOrder.ProductId} does not exist.");
                    }

                    entity.AddProductOrder(productOrder.ProductId, productOrder.Quantity, product.Price);
                }

                if (entity.ProductOrders == null)
                {
                    _logger.LogDebug($"This order doesn't contain any items.");
                    return BadRequest($"This order doesn't contain any items.");
                }

                await _context.Order.AddAsync(entity);
                await _context.SaveChangesAsync();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}