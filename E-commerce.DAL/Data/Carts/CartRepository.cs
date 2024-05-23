using E_commerce.DAL.Data.Categories;
using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Generic_Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Carts
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(MyDbContext context) : base(context)
        {

        }



        public Cart? GetCartByUserId(string userId)
        {
            return _context.Set<Cart>().FirstOrDefault(c => c.ApplicationUserId == userId);

        }

        public Cart? GetUserCartWithItems(string id)
        {
            return _context.Set<Cart>().Include(c => c.CartItems).FirstOrDefault(c => c.ApplicationUserId == id);

        }     

         
    }
}



