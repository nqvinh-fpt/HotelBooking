using Microsoft.AspNetCore.Mvc;
using StoreManagementClient.Controllers;

namespace HotelManagementSystemClient.Controllers
{
	public class HomeController : BaseController
    {
        public HomeController()
        {
            ApiUrl = "https://localhost:7092/api/Rooms";
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the login page or home page
            return RedirectToAction("Index", "Home");
        }
    }
}
