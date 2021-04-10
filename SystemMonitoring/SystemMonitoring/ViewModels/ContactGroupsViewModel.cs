using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoring.Backend.Models;

namespace SystemMonitoring.ViewModels
{
    public class ContactGroupsViewModel
    {
        public IEnumerable<ContactGroup> Groups { get; set; }
    }
}
