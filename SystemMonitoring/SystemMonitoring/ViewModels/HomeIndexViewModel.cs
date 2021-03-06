using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CurrentJobResult> CurrentJobs { get; set; }
        public IEnumerable<ReoccurringJob> ReoccurringJobs { get; set; }
    }
}
