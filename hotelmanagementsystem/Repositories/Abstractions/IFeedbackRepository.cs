using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstractions
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetListFeedbackByRoomId(int roomId);

        Task AddFeedBack(Feedback feedback);

        Task UpdateFeedBack(Feedback feedback);

        Task DeleteFeedBack(int feedbackid);
    }
}