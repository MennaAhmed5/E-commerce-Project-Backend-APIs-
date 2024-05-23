using E_commerce.BL.Managers;
using E_commerce.BL.Managers.CartItems;
using E_commerce.BL.Managers.Carts;
using E_commerce.BL.Managers.Categories;
using E_commerce.BL.Managers.Orders;
using E_commerce.BL.Managers.Products;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL
{
    public static  class ServicesExtensions
    {
        public static void AddBlServices(this IServiceCollection services)
        {
            services.AddScoped<IProductManager,ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ICartItemManager, CartItemManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderManager, OrderManager>();
        }
    }
}
