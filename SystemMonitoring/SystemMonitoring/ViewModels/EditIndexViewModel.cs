using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.ViewModels
{
    public class EditIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string CronString { get; set; }
        public string PriorityField { get; set; }
        public string Conditional { get; set; }
        public string Value { get; set; }
        public int ContactGroupId { get; set; }

    }
}
