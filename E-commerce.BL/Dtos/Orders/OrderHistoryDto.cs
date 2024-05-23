using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos.Orders
{
    public class OrderHistoryDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal TotalOrderPrice { get; set; }
    }
}
