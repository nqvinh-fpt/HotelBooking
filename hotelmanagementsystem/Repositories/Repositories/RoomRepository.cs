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
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(HotelContext hotelContext) : base(hotelContext)
        {
        }
        public async Task<List<Room>> GetPagedRooms(int page, int pageSize, string? roomType, string? status)
        {
            var query = _hotelContext.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(roomType))
            {
                query = query.Where(r => r.RoomType == roomType);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(r => r.Status == status);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Room> CreateRoom(Room room)
        {
            try
            {
                _hotelContext.Rooms.Add(room);
                await _hotelContext.SaveChangesAsync();
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding room: {ex.Message}");
            }
        }

        public async Task DeleteRoom(int id)
        {
            try
            {
                var room = await GetRoomById(id);
                if (room != null)
                {
                    _hotelContext.Rooms.Remove(room);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Room with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting employee with ID {id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Room>> GetListRoom()
        {
            List<Room> list;
            try
            {
                list = await _hotelContext.Rooms.ToListAsync();

            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return list;
        }

        public async Task<Room> GetRoomById(int roomId)
        {
            Room list;
            try
            {
                list = await _hotelContext.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return list;
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            try
            {
                _hotelContext.Rooms.Update(room);
                await _hotelContext.SaveChangesAsync();
                return room;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating employee with ID {room.RoomId}: {ex.Message}");
            }
        }

        public async Task<float> GetRoomRatingById(int roomId)
        {
            try
            {
                int sum_Star = 0;
                foreach(var Feedbacks in _hotelContext.Feedbacks.Where(r => r.RoomId == roomId))
                {
                    sum_Star += (Feedbacks.Rating == null)?0: (int)Feedbacks.Rating;
                }
                float rating = (float)sum_Star/ _hotelContext.Feedbacks.Where(r => r.RoomId == roomId).Count();
                return rating;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<Room>> GetOtherRoomsbyRoomType(int id, string roomType)
        {
            List<Room> list;
            try
            {
                list = await _hotelContext.Rooms.Where(r => r.RoomId != id && r.RoomType == roomType).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return list;
        }
    }
}
