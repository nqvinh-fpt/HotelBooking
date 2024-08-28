using Microsoft.AspNetCore.Mvc;
using StoreManagementClient.Helpers;

namespace StoreManagementClient.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient client = HttpClientFactory.Create();

        protected string ApiUrl;
    }
}