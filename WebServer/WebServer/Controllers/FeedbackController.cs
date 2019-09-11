using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

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
                List<Feedback> feedbacks = await feedbackService.AddFeedback(feedback);
                return Ok(feedbacks);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateFeedback")]
        public async Task<IActionResult> UpdateFeedback([FromBody]FeedbackBll model)
        {
            await feedbackService.UpdateFeedback(model);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteFeedback")]
        public async Task<IActionResult> DeleteFeedback([FromQuery]string Username, [FromQuery]int FeedBackID)
        {
            if (Username != null)
            {
                List<UserFeedbackBll> feedbacks = await feedbackService.RemoveFeedback(Username, FeedBackID);
                return Ok(feedbacks);
            }
            return NotFound();
        }
    }
}
