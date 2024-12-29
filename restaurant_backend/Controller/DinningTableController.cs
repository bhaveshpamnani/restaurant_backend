using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DinningTableController : ControllerBase
    {
        private readonly DinningTableRepository _DinningTableRepository;

        public DinningTableController(DinningTableRepository DinningTableRepository)
        {
            _DinningTableRepository = DinningTableRepository;
        }

        [HttpGet]
        public IActionResult GetAllDinningTable()
        {
            var DinningTable = _DinningTableRepository.GetAllDinningTable();
            return Ok(DinningTable);
        }

        [HttpGet("{TableID}")]
        public IActionResult GetDinningTableByID(int TableID)
        {
            var DinningTable = _DinningTableRepository.GetDinningTableByID(TableID);
            return Ok(DinningTable);
        }

        [HttpPost]
        public IActionResult CreateDinningTable(DinningTableModel DinningTableModel)
        {
            var value = _DinningTableRepository.CreateDinningTable(DinningTableModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateDinningTable(DinningTableModel DinningTableModel)
        {
            var value = _DinningTableRepository.UpdateDinningTable(DinningTableModel);
            return Ok(value);
        }
        
        [HttpDelete("{TableID}")]
        public IActionResult DeleteDinningTable(int TableID)
        {
            var value = _DinningTableRepository.DeleteDinningTable(TableID);
            return Ok(value);
        }
    }
}
