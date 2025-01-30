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

        public CategoryController(CategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
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

        [HttpPost]
        public IActionResult CreateCategory([FromForm] CategoryModel CategoryModel, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Ensure directory exists
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var newFilePath = Path.Combine(imageDirectory, Image.FileName);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                // Assign the new image path
                CategoryModel.ImagePath = $"/images/{Image.FileName}";
            }

            var value = _CategoryRepository.CreateCategory(CategoryModel);
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
