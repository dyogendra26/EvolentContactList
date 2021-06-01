using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWeb.Models
{
    public class Contacts
    {
        public int Contactid { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] Picture { get; set; }
        public int Groupid { get; set; }
        public ContactGroups ContactGroup { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
