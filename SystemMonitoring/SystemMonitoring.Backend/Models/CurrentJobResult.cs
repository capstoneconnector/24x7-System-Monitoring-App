using System;
using System.Collections.Generic;
using System.Text;
using SystemMonitoring.Backend.Enumeration;

namespace SystemMonitoring.Backend.Models
{
    public class CurrentJobResult
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Enqueued { get; set; }
        public int Scheduled { get; set; }
        public int Processing { get; set; }
        public int Succeeded { get; set; }
        public int Failed { get; set; }
        public int Deleted { get; set; }
        public int Awaiting { get; set; }
        public JobType JobType { get; set; }

        public CurrentJobResult()
        {
                
        }
    }
}
