using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoring.ViewModels
{
    public class NewUserViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactMethod { get; set; }
        public int ContactGroupId { get; set; }

    }
}
