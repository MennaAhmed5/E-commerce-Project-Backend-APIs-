using E_commerce.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos.Categories
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

     }
}
