using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class JobHistory
    {
        public int Id { get; set; }
        public int ReoccurringJobId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }

    }
}
