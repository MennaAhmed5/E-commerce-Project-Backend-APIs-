﻿using E_commerce.DAL.Data.Context;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Generic_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }


        
    }
}
