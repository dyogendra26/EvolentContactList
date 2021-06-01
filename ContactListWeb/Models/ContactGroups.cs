using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWeb.Models
{
    public class ContactGroups
    {
       
        public int Groupid { get; set; }

        [Required]
        public string GroupName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
