using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoring.Backend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int ContactGroup_Id { get; set; }
        public string ContactMethod { get; set; }

    }
}
