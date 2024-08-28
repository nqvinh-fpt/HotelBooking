using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Abstractions;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Repositories;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/Rooms")]
    //[ApiController]
    public class RoomsController : ODataController
    {
        private readonly IRoomRepository _roomRepository;
        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        [HttpGet("pagingRooms")]
        public async Task<ActionResult> GetRoomsPaging(int page, int pageSize, string? roomType, string? status)
        {
            try
            {
                List<Room> temp_list = (List<Room>)await _roomRepository.GetListRoom();
                if (!string.IsNullOrEmpty(roomType))
                {
                    temp_list = temp_list.Where(r => r.RoomType == roomType).ToList();
                }

                var totalRooms = temp_list.Count;
                var rooms = await _roomRepository.GetPagedRooms(page, pageSize, roomType, status);

                var totalPages = (int)Math.Ceiling(totalRooms / (double)pageSize);

                return Ok(new { rooms, totalPages });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetListRoom();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByRoomId(int id)
        {
            try
            {
                var room = await _roomRepository.GetRoomById(id);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-other-room/{id}/{roomType}")]
        public async Task<ActionResult<List<Feedback>>> GetOtherRoomsByRoomType(int id, string roomType)
        {
            try
            {
                var room = await _roomRepository.GetOtherRoomsbyRoomType(id, roomType);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRatings/{id}")]
        public async Task<ActionResult<List<Feedback>>> GetRatingsByRoomId(int id)
        {
            try
            {
                var rating = await _roomRepository.GetRoomRatingById(id);
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoom([FromBody] Room employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newRoom = await _roomRepository.CreateRoom(employee);
                return CreatedAtAction(nameof(GetRoomsByRoomId), new { id = newRoom.RoomId }, newRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Room/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.RoomId)
            {
                return BadRequest("Room ID mismatch");
            }

            try
            {
                var updatedRoom = await _roomRepository.UpdateRoom(employee);
                return Ok(updatedRoom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Room/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _roomRepository.DeleteRoom(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
