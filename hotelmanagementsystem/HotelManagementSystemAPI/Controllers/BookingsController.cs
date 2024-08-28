using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using Repositories.Abstractions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ODataController
    {
        private readonly IBookingRepository _bookingRepository;
        
        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // GET: api/Booking
        [HttpGet("get-all-bookings")]
        [EnableQuery]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // GET: api/Booking/5
        [HttpGet("get-booking-by-id/{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST: api/Booking
        [HttpPost("create-booking")]
        public async Task<IActionResult> AddBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBooking = await _bookingRepository.AddBookingAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = newBooking.BookingId }, newBooking);
        }

        // PUT: api/Booking/5
        [HttpPut("update-booking/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.BookingId)
            {
                return BadRequest("Booking ID mismatch");
            }

            var updatedBooking = await _bookingRepository.UpdateBookingAsync(booking);
            return Ok(updatedBooking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("remove-booking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
            return NoContent();
        }

        // GET: api/Booking/customer/5
        [HttpGet("get-customer-booking/{customerId}")]
        public async Task<IActionResult> GetBookingsByCustomer(int customerId)
        {
            var bookings = await _bookingRepository.GetBookingsByCustomerIdAsync(customerId);
            return Ok(bookings);
        }

        // GET: api/Booking/room/5
        [HttpGet("get-room-booking/{roomId}")]
        public async Task<IActionResult> GetBookingsByRoom(int roomId)
        {
            var bookings = await _bookingRepository.GetBookingsByRoomIdAsync(roomId);
            return Ok(bookings);
        }

        // GET: api/Booking/date
        [HttpGet("get-booking-by-date-range")]
        public async Task<IActionResult> GetBookingsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var bookings = await _bookingRepository.GetBookingsByDateRangeAsync(startDate, endDate);
            return Ok(bookings);
        }

        // GET: api/Booking/availability
        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckRoomAvailability([FromQuery] int roomId, [FromQuery] DateTime checkInDate, [FromQuery] DateTime checkOutDate)
        {
            var isAvailable = await _bookingRepository.IsRoomAvailableAsync(roomId, checkInDate, checkOutDate);
            return Ok(new { IsAvailable = isAvailable });
        }

        // GET: api/Booking/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchBookings([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int? customerId, [FromQuery] int? roomId)
        {
            var bookings = await _bookingRepository.GetBookingsByFilterAsync(startDate, endDate, customerId, roomId);
            return Ok(bookings);
        }
    }
}