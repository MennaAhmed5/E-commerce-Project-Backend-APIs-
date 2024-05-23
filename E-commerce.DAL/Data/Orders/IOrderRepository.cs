using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Data.Products;
using E_commerce.DAL.Generic_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Orders
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        IEnumerable<Order> GetOrdersByUserId(string userId);

    }
}
