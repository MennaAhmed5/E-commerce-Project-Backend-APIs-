﻿using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Generic_Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category? GetWithProductsById(int id);

    }
}