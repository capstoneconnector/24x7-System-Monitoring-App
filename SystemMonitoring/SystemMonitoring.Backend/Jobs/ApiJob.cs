using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SystemMonitoring.Backend.ApiResponses;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Interfaces;

namespace SystemMonitoring.Backend.Jobs
{
    public class ApiJob : IApiJob
    {
        private string _endpoint;
        private DataContext _dataContext;
 
        public ApiJob(DataContext dataContext, string endpoint)
        {
            _endpoint = endpoint;
            _dataContext = dataContext;
        }
        
        public async Task Run()
        {
            //Client is item that connects to the API, this happens in the GetAsync method
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync(_endpoint);

            if (result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                var messageAsObject = JsonSerializer.Deserialize<TotalJobsResponse>(message);
                
                _dataContext.JobResults.Add(new Models.JobResult
                {
                    Date = messageAsObject.Date,
                    TotalJobs = messageAsObject.TotalJobs,
                    TotalSuccessfulJobs = messageAsObject.TotalSuccessfulJobs,
                    TotalFailedJobs = messageAsObject.TotalFailedJobs
                });

                //dataEntry.SaveChanges();
                _dataContext.SaveChanges();
            }

        }


    }
}
