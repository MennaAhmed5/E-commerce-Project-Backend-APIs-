using E_commerce.BL.Dtos.CartItems;
using E_commerce.BL.Dtos.Carts;
using E_commerce.BL.Dtos.Categories;
using E_commerce.BL.Managers.CartItems;
using E_commerce.BL.Managers.Carts;
using E_commerce.BL.Managers.Categories;
using E_commerce.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartsController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }
        [Authorize]
        [HttpPost]
        public ActionResult<CartWithItemsDto> AddToCart(AddCartItemDto addCartItemDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract the user ID from the claims

            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var cart = _cartManager.AddToCart(addCartItemDto, userId);
            if (cart == null)
            {
                return BadRequest("please enter valid product id and valid quantity");
            }


            return cart;
        }
        [Authorize]
        [HttpDelete]
        [Route("{productId}")]
        public ActionResult RemoveItemFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract the user ID from the claims
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var res = _cartManager.RemoveItemFromCart(productId , userId);
            if(res == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize]
        [HttpPatch]       
        public ActionResult  EditItemInCart(EditCartItemDto editCartItemDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract the user ID from the claims
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var res = _cartManager.EditItemQuantityInCart(editCartItemDto, userId);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(new { Message = "Cart item Updated Successfully" }); 
        }

        [Authorize]
        [HttpGet]
        public ActionResult<CartWithItemsDto> GetUserCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Extract the user ID from the claims
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var cart = _cartManager.GetUserCartWithItems(userId);
            if(cart == null)
            {
                return NotFound("Cart Not Found");

            }

            return cart;
        }


    }
    
    
}
