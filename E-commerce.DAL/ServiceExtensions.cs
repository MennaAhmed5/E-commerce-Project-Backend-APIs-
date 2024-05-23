using E_commerce.DAL.Data.CartItems;
using E_commerce.DAL.Data.Carts;
using E_commerce.DAL.Data.Categories;
using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Orders;
using E_commerce.DAL.Data.Products;
using E_commerce.DAL.Unit_of_work;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL
{
    public static class  ServiceExtensions
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();

        }
    }
}
