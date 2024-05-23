using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(10,2)")]

        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }



        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
    }
}
