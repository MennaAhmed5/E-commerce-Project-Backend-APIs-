using E_commerce.DAL.Data.CartItems;
using E_commerce.DAL.Data.Carts;
using E_commerce.DAL.Data.Categories;
using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Orders;
using E_commerce.DAL.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Unit_of_work
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        public ICartRepository CartRepository { get; }

        public ICartItemRepository CartItemRepository { get; }

        public IOrderRepository OrderRepository { get; }
        public UnitOfWork(  MyDbContext context, IProductRepository productRepository,ICategoryRepository categoryRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository, IOrderRepository orderRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            CartRepository = cartRepository;
            CartItemRepository = cartItemRepository;
            OrderRepository = orderRepository;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
