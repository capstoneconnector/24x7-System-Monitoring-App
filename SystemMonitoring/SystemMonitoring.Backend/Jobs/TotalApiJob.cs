using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Enumeration;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Jobs
{
    public class TotalApiJob : IApiJob
    {
        private string _endpoint;
        private DataContext _dataContext;
 
        public TotalApiJob(DataContext dataContext, string endpoint)
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
                var messageAsObject = JsonSerializer.Deserialize<TotalJobResult>(message);
                //Maybe update this for a Switch case statements when having more calls
                //Checking for the JobType and comparing it to see where it will be stored into what database
                _dataContext.TotalJobResults.Add(new TotalJobResult
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
