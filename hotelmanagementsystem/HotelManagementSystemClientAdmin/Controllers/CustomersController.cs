using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace HotelManagementSystemClientAdmin.Controllers
{
    [Route("Customers")]
    public class CustomersController : Controller
    {
        // GET: CustomersController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpGet("update/{id}")]
        public IActionResult Update(int id)
        {
            return View();
        }

        // POST: CustomersController/Edit/5
        

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomersController/Delete/5
    }
}
