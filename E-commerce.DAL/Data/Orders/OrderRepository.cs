using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Data.Products;
using E_commerce.DAL.Generic_Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Orders
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(MyDbContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            return _context.Orders.Where(o => o.UserId == userId)
                   .Include(o => o.OrderItems)
                   .ToList();
        }
    }
}
