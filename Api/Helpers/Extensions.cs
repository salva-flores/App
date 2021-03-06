using Microsoft.AspNetCore.Http;

namespace Api.Helpers
{
    public static class Extensions //static to avoid creating new instance
    {
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}