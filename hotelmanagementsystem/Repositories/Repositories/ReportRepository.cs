using BusinessObjects.DTO;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(HotelContext hotelContext) : base(hotelContext)
        {
        }

        public List<Booking> GetBookings(DateTime startDate, DateTime endDate)
        {
            var results = _hotelContext.Bookings.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate).Include(b => b.Room);
            return results.ToList();
        }

        public List<Customer> GetTopBookingCustomer(int topCustomer, DateTime startDate, DateTime endDate)
        {
            var customers = _hotelContext.Customers
                            .Include(r => r.Bookings.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate))
                            .OrderByDescending(c => c.Bookings.Count)
                            .Take(topCustomer);
            return customers.ToList();
        }

        public List<Room> GetRoomByTypes(string? roomType)
        {
            var room = _hotelContext.Rooms.Where(c => c.RoomType.Equals(roomType));
            return room.ToList();
        }

        public List<Room> GetTopBookingRooms(string? roomType, int topRoom, DateTime startDate, DateTime endDate)
        {
            var room = _hotelContext.Rooms
                       .Where(c => c.RoomType.Equals(roomType))
                .Include(r => r.Bookings.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate))
                       .OrderByDescending(r => r.Bookings.Count)
                       .Take(topRoom);
            return room.ToList();
        }

        public List<Room> GetTopBookingRooms(int topRoom, DateTime startDate, DateTime endDate)
        {
            var room = _hotelContext.Rooms
                .Include(r => r.Bookings.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate))
                       .OrderByDescending(r => r.Bookings.Count)
                       .Take(topRoom);
            return room.ToList();
        }

        public List<RoomRevenueDTO> GetTopRevenueRoom(string? roomType, int topRoom, DateTime startDate, DateTime endDate)
        {
            var room = _hotelContext.Rooms
                       .Where(c => c.RoomType.Equals(roomType))
                       .Include(r => r.Bookings.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate))
                       .AsEnumerable()
                       .Select(r => new RoomRevenueDTO
                       {
                           RoomNumber = r.RoomNumber,
                           TotalRevenue = CalculatePrice(r)
                       })
                       .OrderByDescending(r => r.TotalRevenue)
                       .Take(topRoom);
            return room.ToList();
        }

        public List<RoomRevenueDTO> GetTopRevenueRoom(int topRoom, DateTime startDate, DateTime endDate)
        {
            var rooms = _hotelContext.Rooms
                .Include(r => r.Bookings.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate))
                .AsEnumerable() // Process further in memory for CalculatePrice method
                .Select(r => new RoomRevenueDTO
                {
                    RoomNumber = r.RoomNumber,
                    TotalRevenue = CalculatePrice(r)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .Take(topRoom)
                .ToList();

            return rooms;
        }

        private double CalculatePrice(Room room)
        {
            double bookedDays = 0;

            foreach (var booking in room.Bookings)
            {
                if (booking.CheckInDate.HasValue && booking.CheckOutDate.HasValue)
                {
                    bookedDays += (booking.CheckOutDate.Value - booking.CheckInDate.Value).TotalHours;
                }
            }

            return (double)room.Price * bookedDays;
        }

        public List<Booking> GetBookingsFromCustomer(int CustomerId)
        {
            var bookings = _hotelContext.Bookings
                           .Where(b => b.CustomerId == CustomerId);
            return bookings.ToList();
        }

        public List<Booking> GetBookingsFromCustomer(int CustomerId, DateTime startDate, DateTime endDate)
        {
            var bookings = _hotelContext.Bookings
                           .Where(b => b.CustomerId == CustomerId && b.CheckInDate >= startDate && b.CheckOutDate <= endDate);
            return bookings.ToList();
        }

        public List<Feedback> GetRoomFeedbacks(int roomId)
        {
            var feedbacks = _hotelContext.Feedbacks.Where(fb => fb.RoomId == roomId);
            return feedbacks.ToList();
        }

        public List<Feedback> GetRoomFeedbacks(int roomId, DateTime startDate, DateTime endDate)
        {
            var feedbacks = _hotelContext.Feedbacks
                            .Where(fb => fb.RoomId == roomId)
                            .Include(fb => fb.Room)
                            .ThenInclude(r => r.Bookings.Where(b => b.CheckOutDate >= startDate && b.CheckOutDate <= endDate))
                            ;
            return feedbacks.ToList();
        }

        public List<Feedback> GetRoomTypeFeedbacks(string? roomType)
        {
            var feedbacks = _hotelContext.Feedbacks
                            .Include(fb => fb.Room)
                            .Where(fb => fb.Room.RoomType.Equals(roomType))
                            ;
            return feedbacks.ToList();
        }

        public List<Feedback> GetRoomTypeFeedbacks(string? roomType, DateTime startDate, DateTime endDate)
        {
            var feedbacks = _hotelContext.Feedbacks
                            .Include(fb => fb.Room)
                            .ThenInclude(r => r.Bookings.Where(b => b.CheckOutDate >= startDate && b.CheckOutDate <= endDate))
                            .Where(fb => fb.Room.RoomType.Equals(roomType))
                            ;
            return feedbacks.ToList();
        }

        public double GetRoomOccupancyPercentCount()
        {
            var occupiedRoom = _hotelContext.Rooms.Where(r => r.Status == "Available");

            return CalculateOcupancy(occupiedRoom.ToList());
        }

        private double CalculateOcupancy(List<Room> rooms)
        {
            var occupiedRoom = rooms.Count();
            var totalRooms = _hotelContext.Rooms.Count();
            if (totalRooms == 0)
            {
                return 0.0; // Return 0 if there are no rooms to avoid division by zero.
            }
            return ((double)(totalRooms - occupiedRoom) / totalRooms) * 100;
        }

        /// <summary>
        /// return percent of room usage in a period of time for example 20/40 days = 50%
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RoomOcupancyDTO> GetRoomOccupiedTimePeriod(DateTime startDate, DateTime endDate)
        {
            var occupiedRoom = _hotelContext.Rooms
                               .Include(r => r.Bookings
                                        .Where(b => (b.CheckOutDate >= startDate && b.CheckOutDate <= endDate)
                                                    || (b.CheckInDate >= startDate && b.CheckInDate <= endDate && b.CheckOutDate > endDate))
                                        )
                               .ToList();
            var results = occupiedRoom.Select(r => new RoomOcupancyDTO
            {
                RoomId = r.RoomId,
                UsedHours = CalculateRoomUse(r.Bookings.ToList(), startDate, endDate),
                OcupancyPercent = CalculateUsagePercent(CalculateRoomUse(r.Bookings.ToList(), startDate, endDate), startDate, endDate)
            });
            return results.ToList();
        }

        private int CalculateRoomUse(List<Booking> bookings, DateTime startDate, DateTime endDate)
        {
            int UsedHours = 0;
            foreach (Booking booking in bookings)
            {
                var checkInDate = booking.CheckInDate;
                var checkOutDate = booking.CheckOutDate;
                if (checkOutDate >= startDate && checkOutDate <= endDate)
                {
                    if (checkInDate >= startDate) UsedHours += (checkOutDate - checkInDate).Value.Hours;
                    else UsedHours += (checkOutDate - startDate).Value.Hours;
                }
                else
                {
                    UsedHours += (endDate - checkInDate).Value.Hours;
                }
            }
            return UsedHours;
        }

        private double CalculateUsagePercent(int UsedHours, DateTime startDate, DateTime endDate)
        {
            TimeSpan duration = endDate - startDate;
            double totalHours = duration.TotalHours;

            if (totalHours > 0)
            {
                return (double)UsedHours / totalHours;
            }
            else
            {
                // Handle the case where totalHours is zero or negative (if it's logically possible in your application)
                return 0.0; // Or another appropriate default value
            }
        }
    }
}