using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackCategoryController : ControllerBase
    {
        private readonly FeedbackCategoryRepository _FeedbackCategoryRepository;

        public FeedbackCategoryController(FeedbackCategoryRepository FeedbackCategoryRepository)
        {
            _FeedbackCategoryRepository = FeedbackCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetAllFeedbackCategory()
        {
            var FeedbackCategory = _FeedbackCategoryRepository.GetAllFeedbackCategory();
            return Ok(FeedbackCategory);
        }

        [HttpGet("{FeedbackCategoryID}")]
        public IActionResult GetFeedbackCategoryByID(int FeedbackCategoryID)
        {
            var FeedbackCategory = _FeedbackCategoryRepository.GetFeedbackCategoryByID(FeedbackCategoryID);
            return Ok(FeedbackCategory);
        }

        [HttpPost]
        public IActionResult CreateFeedbackCategory(FeedbackCategoryModel FeedbackCategoryModel)
        {
            var value = _FeedbackCategoryRepository.CreateFeedbackCategory(FeedbackCategoryModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateFeedbackCategory(FeedbackCategoryModel FeedbackCategoryModel)
        {
            var value = _FeedbackCategoryRepository.UpdateFeedbackCategory(FeedbackCategoryModel);
            return Ok(value);
        }
        
        [HttpDelete("{FeedbackCategoryID}")]
        public IActionResult DeleteFeedbackCategory(int FeedbackCategoryID)
        {
            var value = _FeedbackCategoryRepository.DeleteFeedbackCategory(FeedbackCategoryID);
            return Ok(value);
        }
    }
}
