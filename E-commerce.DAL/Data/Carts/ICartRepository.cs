using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Generic_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Carts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Cart? GetUserCartWithItems(string id);
         Cart? GetCartByUserId(string userId);

 
    }
}

