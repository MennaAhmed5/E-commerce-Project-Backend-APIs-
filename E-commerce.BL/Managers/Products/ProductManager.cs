using E_commerce.BL.Dtos.Products;
using E_commerce.BL.Managers.Products;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Data.Products;
using E_commerce.DAL.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers
{
    public class ProductManager : IProductManager
    {
       

        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(ProductAddDto productAddDto)
        {
            Product product = new Product
            {
                Name = productAddDto.Name,
                Price = productAddDto.Price,
                Description = productAddDto.Description,
                CategoryId = productAddDto.CategoryId,
                Image = productAddDto.Image
            };
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.SaveChanges();
        }

        public bool Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return false;
            }

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<ProductReadDto> GetAll()
        {
            IEnumerable<Product> productsFromDb =_unitOfWork.ProductRepository.GetAll();
            
            return productsFromDb.Select(d=>new ProductReadDto{
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                Description = d.Description,
                CategoryId = d.CategoryId,
                Image = d.Image

            });
        }

        public ProductReadDto? GetById(int id)
        {
            Product? productFromDb = _unitOfWork.ProductRepository.GetById(id);
            if(productFromDb == null)
            {
                return null;
            }
            return new ProductReadDto
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Price = productFromDb.Price,
                Description = productFromDb.Description,
                CategoryId = productFromDb.CategoryId,
                Image = productFromDb.Image
            };
        }

        public bool Update(ProductUpdateDto productUpdateDto, int id)
        {
            var  product = _unitOfWork.ProductRepository.GetById(id);

            if (product is null)
            {
                return false;
            }

            product.Name = productUpdateDto.Name;
            product.Price = productUpdateDto.Price;
            product.Description = productUpdateDto.Description;
            product.CategoryId = productUpdateDto.CategoryId;
            product.Image = productUpdateDto.Image;

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();

            return true;
        }
    }
}
