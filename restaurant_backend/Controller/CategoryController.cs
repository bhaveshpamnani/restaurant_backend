using DefaultNamespace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _CategoryRepository;
        private readonly CloudinaryService _cloudinaryService;

        public CategoryController(CategoryRepository CategoryRepository,CloudinaryService cloudinaryService)
        {
            _CategoryRepository = CategoryRepository;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        
        public IActionResult GetAllCategory()
        {
            var Category = _CategoryRepository.GetAllCategory();
            return Ok(Category);
        }

        [HttpGet("{CategoryID}")]
        public IActionResult GetCategoryByID(int CategoryID)
        {
            var Category = _CategoryRepository.GetCategoryByID(CategoryID);
            return Ok(Category);
        }

        public class CategoryDto
        {
            public int? CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public IFormFile ImagePath { get; set; } // New property for image
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryDto CategoryModel)
        {
            if (CategoryModel == null || CategoryModel.ImagePath == null)
                return BadRequest("Invalid data.");
            var imageUrl = await _cloudinaryService.UploadImageAsync(CategoryModel.ImagePath);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return StatusCode(500, "Image upload failed.");
            }
            var category = new CategoryModel()
            {
                CategoryName = CategoryModel.CategoryName,
                Description = CategoryModel.Description,
                ImagePath = imageUrl, 
            };
            var value = _CategoryRepository.CreateCategory(category);
            return Ok(value);
        }



        [HttpPut]
        public IActionResult UpdateCategory([FromForm] CategoryModel CategoryModel, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                // Save the new image
                var filePath = Path.Combine("wwwroot/images", Image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                // Set the new image path in the model
                CategoryModel.ImagePath = filePath;
            }

            // Update the category in the database
            var value = _CategoryRepository.UpdateCategory(CategoryModel);
            return Ok(value);
        }

        
        [HttpDelete("{CategoryID}")]
        public IActionResult DeleteCategory(int CategoryID)
        {
            var value = _CategoryRepository.DeleteCategory(CategoryID);
            return Ok(value);
        }
    }
}
