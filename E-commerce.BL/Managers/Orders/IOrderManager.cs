using E_commerce.BL.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Orders
{
    public interface IOrderManager
    {
        bool PlaceOrder(string userId, List<OrderRequestDto> orderRequest);
        IEnumerable<OrderHistoryDto> GetOrderHistory(string userId);

    }
}
