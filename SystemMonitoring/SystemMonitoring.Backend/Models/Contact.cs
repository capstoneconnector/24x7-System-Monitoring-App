using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContactGroupId { get; set; }
        public string ContactMethod { get; set; }

        public virtual User User { get; set; }
        public virtual ContactGroup ContactGroup { get; set; }
    }
}
