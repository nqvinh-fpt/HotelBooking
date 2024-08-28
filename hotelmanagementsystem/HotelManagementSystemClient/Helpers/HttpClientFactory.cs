using System.Net.Http.Headers;

namespace StoreManagementClient.Helpers
{
    public class HttpClientFactory
    {
        private static string _jwtToken = "";
        public static void SetToken(string token)
        {
            _jwtToken = token;
        }
        public static HttpClient Create()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            return httpClient;
        }
    }
}