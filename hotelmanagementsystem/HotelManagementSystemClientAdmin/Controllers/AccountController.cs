using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystemClientAdmin.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
