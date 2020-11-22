using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SystemMonitoring.Backend.Handlers
{
    public class ApiCallHandler
    {
        public static async System.Threading.Tasks.Task<string> ApiCallAsync()
        {
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("x-rapidapi-key", "d69aa8f42bmsh02802141d683248p166b67jsn04426870fe95");
            //client.DefaultRequestHeaders.Add("x-rapidapi-host", "apidojo-yahoo-finance-v1.p.rapidapi.com");

            using (var response = await client.GetAsync("https://google.com"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    return body;
                }
                return null;
                
            }
        }
    }
}
