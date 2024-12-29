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
        public IActionResult CreateCategory(CategoryModel CategoryModel)
        {
            var value = _CategoryRepository.CreateCategory(CategoryModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryModel CategoryModel)
        {
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
