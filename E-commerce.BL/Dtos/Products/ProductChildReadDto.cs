﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos.Products
{
    public class ProductChildReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }  

        public int CategoryId { get; set; }

    }
}
