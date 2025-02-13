using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository;

        public CartController(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        [HttpPost]
        public IActionResult AddItemInCart(CartModel cartModel)
        {
            var value = _cartRepository.AddCart(cartModel);
            return Ok(value);
        }
        
        [HttpPut]
        public IActionResult UpdateCartItem(CartModel cartModel)
        {
            var value = _cartRepository.UpdateCart(cartModel);
            return Ok(value);
        }
        
        [HttpGet("{UserID}")]
        public IActionResult GetCartItemByUserID(int UserID)
        {
            var cart = _cartRepository.GetCartItemByUserID(UserID);
            return Ok(cart);
        }
    }
}
