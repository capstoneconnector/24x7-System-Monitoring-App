using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class ReoccurringJob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string CronString { get; set; }
        public string PriorityField { get; set; }
        public string[] ConditionalExpression { get; set; }
        public bool AlertSent { get; set; }
        public int ContactGroup_Id { get; set; }

    }
}
