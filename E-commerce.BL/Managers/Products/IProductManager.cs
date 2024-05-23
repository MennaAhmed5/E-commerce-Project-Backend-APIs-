using E_commerce.BL.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Products
{
    public interface IProductManager
    {
        IEnumerable<ProductReadDto> GetAll();

        ProductReadDto? GetById(int id);

        void Add(ProductAddDto productAddDto);

        bool Update(ProductUpdateDto productUpdateDto, int id);
        bool Delete(int id);

    }
}
