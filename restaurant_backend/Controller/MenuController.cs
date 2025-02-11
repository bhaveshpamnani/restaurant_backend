using DefaultNamespace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuRepository _MenuRepository;
        private readonly CloudinaryService _cloudinaryService;


        public MenuController(MenuRepository MenuRepository,CloudinaryService cloudinaryService)
        {
            _MenuRepository = MenuRepository;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public IActionResult GetAllMenu()
        {
            var Menu = _MenuRepository.GetAllMenu();
            return Ok(Menu);
        }

        [HttpGet("{MenuID}")]
        public IActionResult GetMenuByID(int MenuID)
        {
            var Menu = _MenuRepository.GetMenuByID(MenuID);
            return Ok(Menu);
        }
        
        [HttpGet("CategoryID/{CategoryID}")]
        public IActionResult GetByMenuByCategoyID(int CategoryID)
        {
            var Menu = _MenuRepository.GetMenuByCategoryID(CategoryID);
            return Ok(Menu);
        }
        
        public class MenuDto
        {
            public int? MenuID { get; set; }
            public int CategoryID { get; set; }
            public string DishName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public bool AvailabilityStatus { get; set; }
            public int Rating { get; set; }
            public IFormFile ImagePath { get; set; } // New property for image
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromForm] MenuDto MenuModel)
        {
            if (MenuModel == null || MenuModel.ImagePath == null)
                return BadRequest("Invalid data.");
            var imageUrl = await _cloudinaryService.UploadImageAsync(MenuModel.ImagePath);
            if (string.IsNullOrEmpty(imageUrl))
            {
                return StatusCode(500, "Image upload failed.");
            }

            var menu = new MenuModel()
            {
                CategoryID = MenuModel.CategoryID,
                DishName = MenuModel.DishName,
                Description = MenuModel.Description,
                Price = MenuModel.Price,
                AvailabilityStatus = MenuModel.AvailabilityStatus,
                Rating = MenuModel.Rating,
                ImageURL = imageUrl
            };
            var value = _MenuRepository.CreateMenu(menu);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateMenu(MenuModel MenuModel)
        {
            var value = _MenuRepository.UpdateMenu(MenuModel);
            return Ok(value);
        }
        
        [HttpDelete("{MenuID}")]
        public IActionResult DeleteMenu(int MenuID)
        {
            var value = _MenuRepository.DeleteMenu(MenuID);
            return Ok(value);
        }
    }
}
