using E_commerce.BL.Dtos.Categories;
using E_commerce.BL.Dtos.Products;
using E_commerce.BL.Managers;
using E_commerce.BL.Managers.Categories;
using E_commerce.BL.Managers.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        [Authorize]
        [HttpGet]

        public ActionResult<IEnumerable<CategoryReadDto>> GetAll()
        {
            var categories = _categoryManager.GetAll();
            return categories.ToList();
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public ActionResult<CategoryWithProductsDto> GetById(int id)
        {
            var category = _categoryManager.GetById(id);
            if(category == null)
            {
               return NotFound(new { Message = "Category Not Found" });
            }
            return category;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public ActionResult Add(CategoryAddDto categoryAddDto)
        {
            _categoryManager.Add(categoryAddDto);

            return StatusCode(StatusCodes.Status201Created, new { Message = "Category Created successfully" });

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("{id}")]
        public ActionResult Edit(CategoryUpdateDto categoryUpdateDto, int id)
        {
            var isFound = _categoryManager.Update(categoryUpdateDto, id);
            if (!isFound)
            {
                return NotFound(new { Message = "Category Not Found" });

            }
            return StatusCode(StatusCodes.Status200OK, new { Message = "Category Updated successfully" });

        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var isFound = _categoryManager.Delete(id);
            if (!isFound)
            {
                return NotFound(new { Message = "Category Not Found" });
            }


            return NoContent();
        }
    }
}
