using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackRepository _feedbackRepository;

        public FeedbackController(FeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public IActionResult GetAllFeedback()
        {
            var feedback = _feedbackRepository.GetAllFeedback();
            return Ok(feedback);
        }

        [HttpGet("{FeedbackID}")]
        public IActionResult GetFeedbackByID(int FeedbackID)
        {
            var feedback = _feedbackRepository.GetFeedbackByID(FeedbackID);
            return Ok(feedback);
        }

        [HttpPost]
        public IActionResult CreateFeedback(FeedbackModel feedbackModel)
        {
            var value = _feedbackRepository.CreateFeedback(feedbackModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateFeedback(FeedbackModel feedbackModel)
        {
            var value = _feedbackRepository.UpdateFeedback(feedbackModel);
            return Ok(value);
        }
        
        [HttpDelete("{FeedbackID}")]
        public IActionResult DeleteFeedback(int FeedbackID)
        {
            var value = _feedbackRepository.DeleteFeedback(FeedbackID);
            return Ok(value);
        }
    }
}
