using BusinessObjects.Models;
using HotelManagementSystemClient.Models.Bookings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagementClient.Controllers;
using System.Security.Claims;
using System.Text.Json;

namespace HotelManagementSystemClient.Controllers
{
    public class BookingsController : BaseController
    {
        public BookingsController()
        {
            ApiUrl = "https://localhost:7092/api/Bookings";
        }
        // GET: BookingsController

        public async Task<ActionResult> Place(int id)
        {
            var customerId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToAction("/Accounts/Login");
            }
            var response = await client.GetAsync($"https://localhost:7092/api/customer/get-customer-by-id/{customerId}");

            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Customer customer = JsonSerializer.Deserialize<Customer>(strData, options);

            response = await client.GetAsync($"https://localhost:7092/api/Rooms/{id}");
            strData = await response.Content.ReadAsStringAsync();
            var room = JsonSerializer.Deserialize<Room>(strData, options);
            var model = new BookingViewModel
            {
                CustomerId = customer.CustomerId,
                Customer = customer,
                RoomId = room.RoomId,
                Room = room
            };
            return View(model);
        }
        // GET: BookingsController/Details/5
        public ActionResult BookingsHistory(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Place(BookingViewModel model)
        {
            // Assume you have a booking service to handle bookings
            var booking = new Booking
            {
                CustomerId = model.CustomerId,
                RoomId = model.RoomId,
                CheckInDate = model.CheckInDate.Value,
                CheckOutDate = model.CheckOutDate.Value,
                Status = 0 // Set appropriate status
            };

            // Send booking data to your API or service
            var response = await client.PostAsJsonAsync("https://localhost:7092/api/bookings/create-booking", booking);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingsHistory");
            }

            else
                // If we got this far, something failed, redisplay form
                return View(model);
        }


        // GET: BookingsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingsController/Edit/5
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

        // GET: BookingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingsController/Delete/5
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
