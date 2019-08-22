using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Controllers
{
    [Route("api/Feedback")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        [HttpGet]
        [Route("GetCurrentGameFeedback/{GameID}")]
        public async Task<IActionResult> GetCurrentGameFeedback(string GameID)
        {
            IEnumerable<object> GameFeedback = await feedbackService.GetCurrentGameFeedback(GameID);
            return Ok(GameFeedback);
        }

        [HttpGet]
        [Route("GetUserFeedback/{Username}")]
        public async Task<IActionResult> GetUserFeedback(string Username)
        {
            IEnumerable<object> UserFeedback = await feedbackService.GetUserFeedback(Username);
            return Ok(UserFeedback);
        }

        [HttpPost]
        [Route("AddFeedback")]
        public async Task<IActionResult> AddFeedback([FromBody]FeedbackBll feedback)
        {
            try
            {
                await feedbackService.AddFeedback(feedback);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteFeedback/{FeedbackID}")]
        public async Task<IActionResult> DeleteFeedback(string FeedBackID)
        {
            if (FeedBackID != null)
            {
                await feedbackService.RemoveFeedback(FeedBackID);
                return Ok();
            }
            return NotFound();
        }
    }
}
