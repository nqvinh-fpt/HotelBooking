using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public BookingRepository(HotelContext hotelContext) : base(hotelContext)
        {
        }

        // Get all bookings
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            try
            {
                return await _hotelContext.Bookings
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllBookingsAsync: {ex.Message}");
                return Enumerable.Empty<Booking>();
            }
        }

        // Get booking by ID
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            try
            {
                var booking = await _hotelContext.Bookings
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .FirstOrDefaultAsync(b => b.BookingId == bookingId);
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetBookingByIdAsync: {ex.Message}");
            }
        }

        // Add a new booking
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            using var transaction = await _hotelContext.Database.BeginTransactionAsync();

            try
            {
                // Add the booking
                _hotelContext.Bookings.Add(booking);
                await _hotelContext.SaveChangesAsync();

                // Update the room status to "Occupied"
                var room = await _hotelContext.Rooms.FindAsync(booking.RoomId);
                if (room == null)
                {
                    throw new Exception("Room not found");
                }

                room.Status = "Occupied";
                _hotelContext.Rooms.Update(room);
                await _hotelContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return booking;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error in AddBookingAsync: {ex.Message}");
            }
        }

        // Update an existing booking
        public async Task<Booking> UpdateBookingAsync(Booking booking)
        {
            try
            {
                _hotelContext.Bookings.Update(booking);
                await _hotelContext.SaveChangesAsync();
                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UpdateBookingAsync: {ex.Message}");
            }
        }

        // Delete a booking
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            try
            {
                var booking = await GetBookingByIdAsync(bookingId);
                if (booking != null)
                {
                    _hotelContext.Bookings.Remove(booking);
                    await _hotelContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in DeleteBookingAsync: {ex.Message}");
            }
        }

        // Get bookings by customer ID
        public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _hotelContext.Bookings
                    .Where(b => b.CustomerId == customerId)
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetBookingsByCustomerIdAsync: {ex.Message}");
            }
        }

        // Get bookings by room ID
        public async Task<IEnumerable<Booking>> GetBookingsByRoomIdAsync(int roomId)
        {
            try
            {
                return await _hotelContext.Bookings
                    .Where(b => b.RoomId == roomId)
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetBookingsByRoomIdAsync: {ex.Message}");
            }
        }

        // Get bookings within a date range
        public async Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _hotelContext.Bookings
                    .Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate)
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetBookingsByDateRangeAsync: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsByFilterAsync(DateTime startDate, DateTime endDate, int? customerId = null, int? roomId = null)
        {
            try
            {
                var query = _hotelContext.Bookings.AsQueryable();

                if (startDate != DateTime.MinValue)
                {
                    query = query.Where(b => b.CheckInDate >= startDate);
                }

                if (endDate != DateTime.MinValue)
                {
                    query = query.Where(b => b.CheckOutDate <= endDate);
                }

                if (customerId.HasValue)
                {
                    query = query.Where(b => b.CustomerId == customerId.Value);
                }

                if (roomId.HasValue)
                {
                    query = query.Where(b => b.RoomId == roomId.Value);
                }

                return await query
                    .Include(b => b.Customer)
                    .Include(b => b.Room)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetBookingsByDateRangeAsync: {ex.Message}");
            }
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            
            try
            {
                var isAvailable = !await _hotelContext.Bookings
                    .AnyAsync(b => b.RoomId == roomId &&
                                   ((b.CheckInDate <= checkInDate && b.CheckOutDate >= checkInDate) ||
                                    (b.CheckInDate <= checkOutDate && b.CheckOutDate >= checkOutDate) ||
                                    (b.CheckInDate >= checkInDate && b.CheckOutDate <= checkOutDate)));
                return isAvailable;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking room availability for room ID {roomId}: {ex.Message}");
            }
        }
    }
}
