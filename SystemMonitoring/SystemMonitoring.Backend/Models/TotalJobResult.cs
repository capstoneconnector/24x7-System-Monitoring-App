using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Enumeration;

namespace SystemMonitoring.Backend.Models
{
    public class TotalJobResult
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TotalJobs { get; set; }
        public int TotalSuccessfulJobs { get; set; }
        public int TotalFailedJobs { get; set; }
        public JobType JobType { get; set; }
    }
}
