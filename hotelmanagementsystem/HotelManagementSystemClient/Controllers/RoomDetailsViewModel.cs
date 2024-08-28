using BusinessObjects.Models;

namespace HotelManagementSystemClient.Controllers
{
    internal class RoomDetailsViewModel
    {
        public Room Room { get; set; }
        public List<Room> OtherRooms { get; set; }
    }
}

