using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Generic_Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Categories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(MyDbContext context) : base(context)
        {
             
        }

        public Category? GetWithProductsById(int id)
        {
            return _context.Set<Category>().Include(c=>c.Products).FirstOrDefault(c=>c.Id==id);
        }
    }
}

