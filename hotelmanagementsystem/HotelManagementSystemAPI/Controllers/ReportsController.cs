using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstractions;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet("bookings")]
        public ActionResult GetBookings(DateTime startDate, DateTime endDate, [FromQuery] int? cid = null)
        {
            int resultCount;
            if (cid == null)
            {
                resultCount = _reportRepository.GetBookings(startDate, endDate).Count;
            }
            else
            {
                resultCount = _reportRepository.GetBookingsFromCustomer(cid.Value, startDate, endDate).Count;
            }
            return Ok(resultCount);
        }
        [HttpGet("bookingstest")]
        public ActionResult GetBookingstest(DateTime startDate, DateTime endDate, [FromQuery] int? cid = null)
        {
            var resultCount = new List<Booking>();
            if (cid == null)
            {
                resultCount = _reportRepository.GetBookings(startDate, endDate);
            }
            else
            {
                resultCount = _reportRepository.GetBookingsFromCustomer(cid.Value, startDate, endDate);
            }
            return Ok(resultCount);
        }
        [HttpGet("revenue")]
        public ActionResult GetRevenue(DateTime startDate, DateTime endDate)
        {
            var bookings = _reportRepository.GetBookings(startDate, endDate);
            double revenue = 0;
            foreach (var book in bookings)
            {
                try
                {
                    revenue += (double)book.Room.Price * (book.CheckOutDate.Value - book.CheckInDate.Value).TotalHours;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("qua den cho 1 cuoc tinh " + ex.Message);
                }
            }
            return Ok(revenue);
        }

        /// <summary>
        /// top customer example top1 top2 top5 top10
        /// </summary>
        /// <param name="topNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("topCustomer")]
        public ActionResult GetTopCustomer(int topCustomer, DateTime startDate, DateTime endDate)
        {
            var result = _reportRepository.GetTopBookingCustomer(topCustomer, startDate, endDate);

            return Ok(result.Select(r => new
            {
                r.CustomerId,
                name = r.FirstName + " " + r.LastName,
                TotalBookings = r.Bookings.Count ,
            }).OrderByDescending(r => r.TotalBookings));
        }

        [HttpGet("topRoom")]
        public ActionResult GetTopBookingRoom(int topRoom, DateTime startDate, DateTime endDate, [FromQuery] string? roomType = null)
        {
            var result = roomType == null
                ? _reportRepository.GetTopBookingRooms(topRoom, startDate, endDate)
                : _reportRepository.GetTopBookingRooms(roomType, topRoom, startDate, endDate);

            return Ok(result.Select(r => new
            {
                r.RoomNumber,
                TotalBooking = r.Bookings.Count,
            }).OrderByDescending(r=>r.TotalBooking));
        }

        [HttpGet("topRevenueRoom")]
        public ActionResult GetTopRevenueRoom(int topRoom, DateTime startDate, DateTime endDate, [FromQuery] string? roomType = null)
        {
            var result = roomType == null
                ? _reportRepository.GetTopRevenueRoom(topRoom, startDate, endDate)
                : _reportRepository.GetTopRevenueRoom(roomType, topRoom, startDate, endDate);

            return Ok(result);
        }

        // GET: /feedbacks/room/{roomId}
        [HttpGet("feedbacks/room/{roomId}")]
        public ActionResult GetRoomFeedbacks(int roomId, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                var result = _reportRepository.GetRoomFeedbacks(roomId, startDate.Value, endDate.Value);
                return Ok(result);
            }
            else
            {
                var result = _reportRepository.GetRoomFeedbacks(roomId);
                return Ok(result);
            }
        }

        // GET: /feedbacks/roomType/{roomType}
        [HttpGet("feedbacks/roomType/{roomType}")]
        public ActionResult GetRoomTypeFeedbacks(string roomType, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (startDate.HasValue && endDate.HasValue)
            {
                var result = _reportRepository.GetRoomTypeFeedbacks(roomType, startDate.Value, endDate.Value);
                return Ok(result);
            }
            else
            {
                var result = _reportRepository.GetRoomTypeFeedbacks(roomType);
                return Ok(result);
            }
        }

        // GET: /occupancy
        [HttpGet("occupancy")]
        public ActionResult GetRoomOccupancyPercent()
        {
            double occupancyPercent = _reportRepository.GetRoomOccupancyPercentCount();
            return Ok(occupancyPercent);
        }

        // GET: /rooms/occupied
        [HttpGet("rooms/usage")]
        public ActionResult GetRoomOccupiedTimePeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate) return BadRequest("StartDate must be earlier than EndDate");
            var results = _reportRepository.GetRoomOccupiedTimePeriod(startDate, endDate);

            return Ok(results);
        }
    }
}