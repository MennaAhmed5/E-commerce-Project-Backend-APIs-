using E_commerce.BL.Dtos.CartItems;
using E_commerce.BL.Dtos.Products;
using E_commerce.DAL.Data.Clients;
using E_commerce.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos.Carts
{
    public class CartWithItemsDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public IEnumerable<CartItemChildReadDto> CartItems { get; set; } = [];
        public decimal TotalCartPrice { 
           
            get
            {
                    return CartItems?.Sum(i => i.Price) ?? 0;
                
            }
        }
         
    }
}
