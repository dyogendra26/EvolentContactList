using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Models.Dtos
{
    public class ContactUpdateDto
    {
        public int Contactid { get; set; }
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] Picture { get; set; }
        public int Groupid { get; set; }
        public ContactGroupsDto ContactGroup { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
