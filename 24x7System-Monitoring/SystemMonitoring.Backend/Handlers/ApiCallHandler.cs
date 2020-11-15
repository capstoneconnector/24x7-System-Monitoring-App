using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SystemMonitoring.Backend.Handlers
{
    class ApiCallHandler
    {
        public static async System.Threading.Tasks.Task<string> ApiCallAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://apidojo-yahoo-finance-v1.p.rapidapi.com/auto-complete?q=tesla&region=US"),
                Headers =
    {
        { "x-rapidapi-key", "d69aa8f42bmsh02802141d683248p166b67jsn04426870fe95" },
        { "x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}
