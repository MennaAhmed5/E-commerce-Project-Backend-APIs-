using E_commerce.BL.Dtos.Categories;
using E_commerce.BL.Dtos.Products;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Categories
{
    public class CategoryManager : ICategoryManager
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(CategoryAddDto categoryFromRequest)
        {
            Category category = new Category
            {
                Name = categoryFromRequest.Name,
                 
            };
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.SaveChanges();
        }

        public bool Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return false;
            }

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<CategoryReadDto> GetAll()
        {

            IEnumerable<Category> categoriesFromDb = _unitOfWork.CategoryRepository.GetAll();

            return categoriesFromDb.Select(d => new CategoryReadDto
            {
                Id = d.Id,
                Name = d.Name,             

            });
        }

        public CategoryWithProductsDto? GetById(int id)
        {
            Category? categoryFromDb = _unitOfWork.CategoryRepository.GetWithProductsById(id);
            if (categoryFromDb == null)
            {
                return null;
            }
            return new CategoryWithProductsDto
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name,
                Products = categoryFromDb.Products.Select(p => new ProductChildReadDto
                {
                     Id = p.Id,
                     Name = p.Name,
                     Price = p.Price,
                     Description = p.Description,
                     CategoryId = p.CategoryId,
                     Image =p.Image
                })

            };
        }

        public bool Update(CategoryUpdateDto categoryUpdateDto, int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);

            if (category is null)
            {
                return false;
            }

            category.Name = categoryUpdateDto.Name;


            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();

            return true;
        }
    }
}
