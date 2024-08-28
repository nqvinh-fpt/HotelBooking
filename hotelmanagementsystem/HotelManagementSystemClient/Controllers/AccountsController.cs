using BusinessObjects.Models;
using HotelManagementSystemClient.Models.Accounts;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using StoreManagementClient.Controllers;
using StoreManagementClient.Helpers;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace HotelManagementSystemClient.Controllers
{
    public class AccountsController : BaseController
	{
        public AccountsController()
        {
            ApiUrl = "https://localhost:7092/api/customer";
        }
        // GET: Accounts/Login
        public ActionResult Login()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://localhost:7092/api/account/login-customer", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var tokenResponse = JsonSerializer.Deserialize<string>(responseContent, options);
                    if (tokenResponse != null)
                    {
                        HttpContext.Session.SetString("JWTToken", tokenResponse);

                        var decodedToken = JwtHelper.DecodeJwtToken(tokenResponse);
                        var customer = new Customer
                        {
                            CustomerId = decodedToken.CustomerId,
                            Username = decodedToken.Username,
                            Email = decodedToken.Email,
                        };
                        HttpContext.Session.SetString("Staff", JsonSerializer.Serialize(customer, options));
						HttpContext.Session.SetString("UserId", customer.CustomerId.ToString());
						HttpContext.Session.SetString("UserName", customer.Username);
						HttpContext.Session.SetString("Role", decodedToken.Role.ToString());
                        HttpClientFactory.SetToken(tokenResponse); // Ensure future requests use the token
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, responseContent);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred during login.");
            }

            return View(model);
        }

		// GET: Accounts/Register
		[HttpGet]
        public ActionResult Register()
		{
			return View();
		}

        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            string userId = HttpContext.Session.GetString("UserId");
            string temp_Url = ApiUrl + "/get-customer-by-id/" + userId;
            HttpResponseMessage responseMessage = await client.GetAsync(temp_Url);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Customer customer = JsonSerializer.Deserialize<Customer>(strData, options);

            return View(customer);
        }
        [HttpPost]
        public async Task<ActionResult> Profile(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Assuming you have a way to get the UserId from session or other context
                    string userId = HttpContext.Session.GetString("UserId");
                    string temp_Url = ApiUrl + "/edit-customer/" + userId; // Adjust URL as needed

                    var content = new StringContent(
                        JsonSerializer.Serialize(customer),
                        Encoding.UTF8,
                        "application/json"
                    );

                    HttpResponseMessage responseMessage = await client.PutAsync(temp_Url, content);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        // Handle successful update
                        ViewBag.SuccessMessage = "Profile updated successfully.";
                        return View(customer);
                    }
                    else
                    {
                        // Handle failure
                        string responseContent = await responseMessage.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = $"Error updating profile: {responseContent}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Exception: {ex.Message}";
                }
            }

            // If we got here, something went wrong; redisplay the form
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var customer = model.Adapt<Customer>();
			var jsonContent = JsonSerializer.Serialize(customer);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync("https://localhost:7092/api/account/register-customer", content);
				var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
				{
                
                    return RedirectToAction("Login");
				}
				else
				{
					ModelState.AddModelError(string.Empty, responseContent);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while creating the customer.");
			}

			return View(model);
		}

		// GET: AccountsController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AccountsController/Edit/5
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

		// GET: AccountsController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: AccountsController/Delete/5
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
    public class TokenResponse
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int Role { get; set; }
    }
}
