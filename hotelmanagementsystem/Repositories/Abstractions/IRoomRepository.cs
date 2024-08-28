using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstractions
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetListRoom();
        Task<List<Room>> GetPagedRooms(int page, int pageSize, string? roomType, string? status);
        Task<Room> GetRoomById(int roomId);
        Task<float> GetRoomRatingById(int roomId);
        Task<List<Room>> GetOtherRoomsbyRoomType(int id, string roomType);
        Task<Room> CreateRoom(Room room);
        Task DeleteRoom(int id);
        Task<Room> UpdateRoom(Room room);
    }
}
