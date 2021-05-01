using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class ContactGroup
    {
        public ContactGroup()
        {
            this.Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
