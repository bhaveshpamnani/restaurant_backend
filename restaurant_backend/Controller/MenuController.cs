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

        public MenuController(MenuRepository MenuRepository)
        {
            _MenuRepository = MenuRepository;
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

        [HttpPost]
        public IActionResult CreateMenu(MenuModel MenuModel)
        {
            var value = _MenuRepository.CreateMenu(MenuModel);
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
