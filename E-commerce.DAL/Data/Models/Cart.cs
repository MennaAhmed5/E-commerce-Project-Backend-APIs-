using E_commerce.DAL.Data.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        [NotMapped]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalCartPrice {
            get
            {
                return CartItems?.Sum(i => i.TotalPrice) ?? 0;
            }
        }


      
        
    }
}
