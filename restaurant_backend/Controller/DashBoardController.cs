using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly DashboardRepository _dashboardRepository;

        public DashBoardController(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var data = await _dashboardRepository.GetDashboardDataAsync();
            return Ok(data);
        }
    }
}
