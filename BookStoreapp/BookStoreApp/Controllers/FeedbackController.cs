using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        IFeedBackBL  feedbackBL;

        public FeedbackController(IFeedBackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.Users)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(AddFeedbackModel addFeedbackModel)
        {
            try
            {
            
                var result = this.feedbackBL.AddFeedback(addFeedbackModel);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Feedback Added Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Failed" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Users)]
        [HttpGet("getFeedback/{bookId}")]
        public IActionResult getFeedback(int bookId)
        {
            try
            {
                var result = this.feedbackBL.getFeedback(bookId);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, status = 200, data = result });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Could not fetched" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
