using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagementClient.Controllers;
using System.Text.Json;

namespace HotelManagementSystemClient.Controllers
{
    public class RoomsController : BaseController
    {
        public RoomsController()
        {
            ApiUrl = "https://localhost:7092/api/Rooms";
        }
        // GET: RoomsController1
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Room> rooms = JsonSerializer.Deserialize<List<Room>>(strData, options);

            return View(rooms);
        }

        // GET: RoomsController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            string tempUrl = ApiUrl + $"/{id}";
            HttpResponseMessage responseMessage = await client.GetAsync(tempUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Room room = JsonSerializer.Deserialize<Room>(strData, options);

            tempUrl = ApiUrl + $"/get-other-room/{id}/{room.RoomType}";
            responseMessage = await client.GetAsync(tempUrl);
            strData = await responseMessage.Content.ReadAsStringAsync();
            List<Room> rooms = JsonSerializer.Deserialize<List<Room>>(strData, options);
            
            var viewModel = new RoomDetailsViewModel
            {
                Room = room,
                OtherRooms = rooms
            };
            ViewData["RoomId"] = id;
            string okValue = Request.Query["ok"];
            ViewData["ok"] = okValue;
            return View(viewModel);
        }

        // GET: RoomsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomsController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomsController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomsController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
