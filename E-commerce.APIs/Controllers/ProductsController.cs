using E_commerce.BL.Dtos;
using E_commerce.BL.Dtos.Products;
using E_commerce.BL.Managers;
using E_commerce.BL.Managers.Products;
using E_commerce.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace E_commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        #region Helper
        private ActionResult<UplaodFileDto> Upload(IFormFile file)
        {
            try
            {
                #region Check extension
                var extension = Path.GetExtension(file.FileName);
                Console.WriteLine($"File extension: {extension}");

                var allowedExtensions = new string[]
                {
                  ".png",
                  ".jpg",
                  ".svg"
                };

                bool isExtensionsAllowed = allowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase);
                if (!isExtensionsAllowed)
                {
                    Console.WriteLine("Extension not allowed");
                    return BadRequest(new UplaodFileDto(false, "Extension is not valid"));
                }
                #endregion

                #region Check Length
                bool isSizeAllowed = file.Length > 0 && file.Length <= 4_000_000;

                if (!isSizeAllowed)
                {
                    Console.WriteLine("File size not allowed");
                    return BadRequest(new UplaodFileDto(false, "Size is not allowed"));
                }
                #endregion

                #region Storing image
                var newFileName = $"{Guid.NewGuid()}{extension}";
                var imagesPath = Path.Combine(Environment.CurrentDirectory, "Images");
                Console.WriteLine($"Images path: {imagesPath}");

                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fullFilePath = Path.Combine(imagesPath, newFileName);
                Console.WriteLine($"Full file path: {fullFilePath}");

                using var stream = new FileStream(fullFilePath, FileMode.Create);
                file.CopyTo(stream);
                #endregion

                #region Generating URL
                var url = $"{Request.Scheme}://{Request.Host}/Images/{newFileName}";
                Console.WriteLine($"Generated URL: {url}");
                return Ok(new UplaodFileDto(true, "Success", url));
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Upload: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new UplaodFileDto(false, "Internal server error"));
            }
        }

        #endregion


        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAll([FromQuery] string? Name, int CategoryId)
        {
            var products = _productManager.GetAll();

            if (!String.IsNullOrWhiteSpace(Name))
            {
                products=products.Where(p => p.Name.Equals(Name));
            }

            if (CategoryId != 0)
            {
                products = products.Where(p => p.CategoryId.Equals(CategoryId));
            }

            return products.ToList();
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public ActionResult <ProductReadDto> GetById(int id)
        {
            var product = _productManager.GetById(id);
            if (product == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            return product;
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public ActionResult Add([FromForm] ProductFromRequestDto productFromRequestDto)
        {
            if (productFromRequestDto.Image != null)
            {
                var uploadResult = Upload(productFromRequestDto.Image);

                if (uploadResult.Result is BadRequestObjectResult badRequestResult)
                {
                    Console.WriteLine("BadRequest result from Upload");
                    return badRequestResult;
                }

                if (uploadResult.Result is StatusCodeResult statusCodeResult && statusCodeResult.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    Console.WriteLine("Internal server error from Upload");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
                }

                var uploadFileDto = (uploadResult.Result as OkObjectResult)?.Value as UplaodFileDto;
                if (uploadFileDto == null || !uploadFileDto.IsSuccess)
                {
                    Console.WriteLine("Failed to upload image.");
                    return BadRequest("Failed to upload image.");
                }

                string imagePath = uploadFileDto.Url;
                Console.WriteLine($"Image path: {imagePath}");

                var productAddDto = new ProductAddDto
                {
                    Name = productFromRequestDto.Name,
                    Description = productFromRequestDto.Description,
                    Price = productFromRequestDto.Price,
                    CategoryId = productFromRequestDto.CategoryId,
                    Image = imagePath
                };

                _productManager.Add(productAddDto);

                return StatusCode(StatusCodes.Status201Created, new { Message = "Product created successfully" });
            }

            Console.WriteLine("Image is required.");
            return BadRequest("Image is required.");
        }


         
        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        [Route("{id}")]
        
        public ActionResult Edit([FromForm] ProductFromRequestDto productFromRequestDto, int id)
        {
            string newImagePath = null;

            // Check if a new image file is provided
            if (productFromRequestDto.Image != null)
            {
                var uploadResult = Upload(productFromRequestDto.Image);

                if (uploadResult.Result is BadRequestObjectResult badRequestResult)
                {
                    return badRequestResult;
                }

                var uploadFileDto = (uploadResult.Result as OkObjectResult)?.Value as UplaodFileDto;
                if (uploadFileDto == null || !uploadFileDto.IsSuccess)
                {
                    return BadRequest("Failed to upload image.");
                }

                newImagePath = uploadFileDto.Url;
            }

             
            var productUpdateDto = new ProductUpdateDto
            {
                Name = productFromRequestDto.Name,
                Description = productFromRequestDto.Description,
                Price = productFromRequestDto.Price,
                CategoryId = productFromRequestDto.CategoryId,
                Image = newImagePath  
            };

            var isFound = _productManager.Update(productUpdateDto, id);
            if (!isFound)
            {
                return NotFound(new { Message = "Product Not Found" });
            }

            return StatusCode(StatusCodes.Status200OK, new { Message = "Product Updated successfully" });
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
           var isFound = _productManager.Delete(id);
            if (!isFound)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            

            return NoContent();
        }
    }
}
