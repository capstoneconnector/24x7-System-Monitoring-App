using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<TotalJobResult> TotalJobResults { get; set; } 
        public IEnumerable<CurrentJobResult> CurrentJobResult { get; set; }
    }
}
