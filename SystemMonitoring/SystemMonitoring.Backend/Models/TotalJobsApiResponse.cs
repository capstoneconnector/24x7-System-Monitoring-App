using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class TotalJobsApiResponse
    {
        public DateTime Date { get; set; }
        public int TotalJobs { get; set; }
        public int TotalSuccessfulJobs { get; set; }
        public int TotalFailedJobs { get; set; }
    }
}
