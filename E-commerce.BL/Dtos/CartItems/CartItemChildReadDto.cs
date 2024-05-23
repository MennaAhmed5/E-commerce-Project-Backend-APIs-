using E_commerce.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos.CartItems
{
    public class CartItemChildReadDto
    {
       
        public int ProductId { get; set; }
        public int Quantity { get; set; }

         public decimal Price { get; set; }
         
    }
}
