using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Data;
using SystemMonitoring.Backend.Interfaces;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.Backend.Jobs
{
    public class CurrentApiJob : IApiJob
    {
        private string _endpoint;
        private DataContext _dataContext;

        public CurrentApiJob(DataContext dataContext, string endpoint)
        {
            _endpoint = endpoint;
            _dataContext = dataContext;
        }
        public async Task Run()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.GetAsync(_endpoint);

            if (result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                var messageAsObject = JsonSerializer.Deserialize<CurrentJobResult>(message);

                _dataContext.CurrentJobResults.Add(new CurrentJobResult
                {
                    Date = messageAsObject.Date,
                    Enqueued = messageAsObject.Enqueued,
                    Scheduled = messageAsObject.Scheduled,
                    Processing = messageAsObject.Processing,
                    Succeeded = messageAsObject.Succeeded,
                    Failed = messageAsObject.Failed,
                    Deleted = messageAsObject.Deleted,
                    Awaiting = messageAsObject.Awaiting
                });

                //dataEntry.SaveChanges();
                _dataContext.SaveChanges();

            }
        }
    }
}
