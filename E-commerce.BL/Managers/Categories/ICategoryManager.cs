using E_commerce.BL.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Categories
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryReadDto> GetAll();
        CategoryWithProductsDto? GetById(int id);

        void Add(CategoryAddDto categoryAddDto);
        bool Update(CategoryUpdateDto categoryUpdateDto, int id);

        bool Delete(int id);
    }
}
