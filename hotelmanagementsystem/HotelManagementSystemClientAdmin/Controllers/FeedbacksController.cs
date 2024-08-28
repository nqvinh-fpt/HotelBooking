using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystemClientAdmin.Controllers
{
    [Route("Feedbacks")]
    public class FeedbacksController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("update/{id}")]
        public IActionResult Update(int id)
        {
            return View();
        }
    }
}