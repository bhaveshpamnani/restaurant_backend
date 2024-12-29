using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly CustomerOrderRepository _CustomerOrderRepository;

        public CustomerOrderController(CustomerOrderRepository CustomerOrderRepository)
        {
            _CustomerOrderRepository = CustomerOrderRepository;
        }

        [HttpGet]
        public IActionResult GetAllCustomerOrder()
        {
            var CustomerOrder = _CustomerOrderRepository.GetAllCustomerOrder();
            return Ok(CustomerOrder);
        }

        [HttpGet("{OrderID}")]
        public IActionResult GetCustomerOrderByID(int OrderID)
        {
            var CustomerOrder = _CustomerOrderRepository.GetCustomerOrderByID(OrderID);
            return Ok(CustomerOrder);
        }

        [HttpPost]
        public IActionResult CreateCustomerOrder(CustomerOrderModel CustomerOrderModel)
        {
            var value = _CustomerOrderRepository.CreateCustomerOrder(CustomerOrderModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateCustomerOrder(CustomerOrderModel CustomerOrderModel)
        {
            var value = _CustomerOrderRepository.UpdateCustomerOrder(CustomerOrderModel);
            return Ok(value);
        }
        
        [HttpDelete("{OrderID}")]
        public IActionResult DeleteCustomerOrder(int OrderID)
        {
            var value = _CustomerOrderRepository.DeleteCustomerOrder(OrderID);
            return Ok(value);
        }
    }
}
