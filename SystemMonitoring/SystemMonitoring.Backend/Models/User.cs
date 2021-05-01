using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class User
    {

        public User()
        {
            this.Contacts = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int ContactInfoId { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
