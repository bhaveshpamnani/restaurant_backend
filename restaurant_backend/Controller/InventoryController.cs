using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryRepository _InventoryRepository;

        public InventoryController(InventoryRepository InventoryRepository)
        {
            _InventoryRepository = InventoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllInventory()
        {
            var Inventory = _InventoryRepository.GetAllInventory();
            return Ok(Inventory);
        }

        [HttpGet("{InventoryID}")]
        public IActionResult GetInventoryByID(int InventoryID)
        {
            var Inventory = _InventoryRepository.GetInventoryByID(InventoryID);
            return Ok(Inventory);
        }

        [HttpPost]
        public IActionResult CreateInventory(InventoryModel InventoryModel)
        {
            var value = _InventoryRepository.CreateInventory(InventoryModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateInventory(InventoryModel InventoryModel)
        {
            var value = _InventoryRepository.UpdateInventory(InventoryModel);
            return Ok(value);
        }
        
        [HttpDelete("{InventoryID}")]
        public IActionResult DeleteInventory(int InventoryID)
        {
            var value = _InventoryRepository.DeleteInventory(InventoryID);
            return Ok(value);
        }
    }
}
