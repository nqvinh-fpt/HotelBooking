using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Abstractions;
using Repositories.Repositories;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbacksController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }


        [HttpGet("GetFeedBacksByRoomId/{id}")]
        public async Task<ActionResult<List<Feedback>>> GetFeedbacksByRoomId(int id)
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetListFeedbackByRoomId(id);
                return Ok(feedbacks);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback([FromBody]Feedback feedback)
        {
            try
            {
                await _feedbackRepository.AddFeedBack(feedback);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("edit-feedback/{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] Feedback feedback)
        {
            try
            {
                await _feedbackRepository.UpdateFeedBack(feedback);
                return Ok("Feedback updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-feedback/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _feedbackRepository.DeleteFeedBack(id);
                return Ok("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
