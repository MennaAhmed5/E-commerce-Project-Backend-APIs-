using E_commerce.DAL.Data.Carts;
using E_commerce.DAL.Data.Categories;
using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Generic_Repo;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.CartItems
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(MyDbContext context) : base(context)
        {
        }

        public CartItem? GetCartItemByCartIdAndProductId (int cartId, int productId)
        {
            return _context.Set<CartItem>().FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

        }

    }
}
