using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystemClient.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateAsync()
        {
            //var staff = JsonSerializer.Deserialize<Staff>(HttpContext.Session.GetString("Staff"));
            //ViewBag.StaffId = staff.StaffId;
            //var model = new ApiModel { OrderApiUrl = ApiUrl, OrderId = 0 };
            return View("Create");
        }

        public async Task<ActionResult> UpdateAsync(int id)
        {
            //var staff = JsonSerializer.Deserialize<Staff>(HttpContext.Session.GetString("Staff"));
            //ViewBag.StaffId = staff.StaffId;
            //var model = new ApiModel { OrderApiUrl = ApiUrl, OrderId = 0 };
            return View("Update", id);
        }
    }

    public class RoomDTO
    {
        public int RoomId { get; set; }
    }
}
