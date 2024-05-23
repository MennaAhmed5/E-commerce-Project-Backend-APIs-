using E_commerce.BL.Dtos.CartItems;
using E_commerce.BL.Dtos.Carts;
using E_commerce.DAL.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Carts
{
    public interface ICartManager
    {
        CartWithItemsDto?  GetUserCartWithItems(string userId);
        Cart AddCart(string userId);

        bool DeleteCart(string UserId);
        CartWithItemsDto? AddToCart(AddCartItemDto addCartItemDto, string userId);

        bool RemoveItemFromCart(int productId, string userId);
        bool EditItemQuantityInCart(EditCartItemDto editCartItemDto , string userId);
       

    }
}
