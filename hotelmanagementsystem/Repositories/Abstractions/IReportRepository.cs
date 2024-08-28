using BusinessObjects.DTO;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstractions
{
    public interface IReportRepository
    {
        List<Booking> GetBookings(DateTime startDate, DateTime endDate);

        List<Booking> GetBookingsFromCustomer(int CustomerId);

        List<Booking> GetBookingsFromCustomer(int CustomerId, DateTime startDate, DateTime endDate);

        List<Room> GetRoomByTypes(string? roomType);

        List<Feedback> GetRoomFeedbacks(int roomId);

        List<Feedback> GetRoomFeedbacks(int roomId, DateTime startDate, DateTime endDate);

        double GetRoomOccupancyPercentCount();

        List<RoomOcupancyDTO> GetRoomOccupiedTimePeriod(DateTime startDate, DateTime endDate);

        List<Feedback> GetRoomTypeFeedbacks(string? roomType);

        List<Feedback> GetRoomTypeFeedbacks(string? roomType, DateTime startDate, DateTime endDate);

        List<Customer> GetTopBookingCustomer(int topCustomer, DateTime startDate, DateTime endDate);

        List<Room> GetTopBookingRooms(string? roomType, int topRoom, DateTime startDate, DateTime endDate);

        List<Room> GetTopBookingRooms(int topRoom, DateTime startDate, DateTime endDate);

        List<RoomRevenueDTO> GetTopRevenueRoom(string? roomType, int topRoom, DateTime startDate, DateTime endDate);

        List<RoomRevenueDTO> GetTopRevenueRoom(int topRoom, DateTime startDate, DateTime endDate);
    }
}