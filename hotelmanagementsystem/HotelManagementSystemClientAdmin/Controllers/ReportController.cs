using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HotelManagementSystemClientAdmin.Controllers
{
    public class ReportController : Controller
    {
        protected readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> IndexAsync()
        {
            /*            HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:7092/api/Reports/occupancy");
                        string strData = await responseMessage.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        ViewBag.occupiedPercent = JsonSerializer.Deserialize<double>(strData, options);*/
            return View();
        }
    }
}