using E_commerce.BL.Dtos.Orders;
using E_commerce.BL.Managers.Carts;
using E_commerce.BL.Managers.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }
        [Authorize]
        [HttpPost]
        public ActionResult PlaceOrder([FromBody] List<OrderRequestDto> orderRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract the user ID from the claims
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = _orderManager.PlaceOrder(userId, orderRequest);
            if (!result)
            {
                return BadRequest("Failed to place order.");
            }

            return Ok("Order placed successfully.");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetOrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract the user ID from the claims
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var orderHistory = _orderManager.GetOrderHistory(userId);

            

            return Ok(orderHistory);
        }
    }
}
