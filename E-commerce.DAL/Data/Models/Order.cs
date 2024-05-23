using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<OrderItem> OrderItems { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalOrderPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}