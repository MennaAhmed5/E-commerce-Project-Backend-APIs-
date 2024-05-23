using E_commerce.DAL.Data.CartItems;
using E_commerce.DAL.Data.Carts;
using E_commerce.DAL.Data.Categories;
using E_commerce.DAL.Data.Orders;
using E_commerce.DAL.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Unit_of_work
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public ICartRepository CartRepository { get; }

        public ICartItemRepository CartItemRepository { get; }

        public IOrderRepository OrderRepository { get; }
        void SaveChanges();
    }
}
