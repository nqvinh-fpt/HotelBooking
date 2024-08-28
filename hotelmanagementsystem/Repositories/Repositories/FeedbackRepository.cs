using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class FeedbackRepository : BaseRepository, IFeedbackRepository
    {
        public FeedbackRepository(HotelContext hotelContext) : base(hotelContext)
        {
        }

        public async Task AddFeedBack(Feedback feedback)
        {
            try
            {
                _hotelContext.Feedbacks.Add(feedback);
                await _hotelContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteFeedBack(int feedbackid)
        {
            try
            {
                var existingFeedBack = await _hotelContext.Feedbacks.FindAsync(feedbackid);
                if (existingFeedBack != null)
                {
                    _hotelContext.Feedbacks.Remove(existingFeedBack);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Feedback not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while updating the Feedback.", ex);
            }
        }

        public async Task<List<Feedback>> GetListFeedbackByRoomId(int roomId)
        {
            List<Feedback> feedbacks = null;
            try {
                if (_hotelContext.Rooms.Where(d => d.RoomId == roomId).FirstOrDefault() == null)
                {
                    return null;
                }
                feedbacks = _hotelContext.Feedbacks.Where(f => f.RoomId == roomId).Include(u => u.Customer).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return feedbacks;
        }

        public async Task UpdateFeedBack(Feedback feedback)
        {
            try
            {
                var existingFeedBack = await _hotelContext.Feedbacks.FindAsync(feedback.FeedbackId);
                if (existingFeedBack != null)
                {
                    _hotelContext.Entry(existingFeedBack).CurrentValues.SetValues(feedback);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Feedback not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while updating the Feedback.", ex);
            }
        }
    }
}
