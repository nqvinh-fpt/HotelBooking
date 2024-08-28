using BusinessObjects.Models;

namespace Repositories.Abstractions
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Booking>> GetBookingsByRoomIdAsync(int roomId);
        Task<IEnumerable<Booking>> GetBookingsByFilterAsync(DateTime startDate, DateTime endDate, int? customerId = null, int? roomId = null);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate);
        Task<Booking> UpdateBookingAsync(Booking booking);
    }
}