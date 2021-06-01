using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactListAPI.Models.Dtos
{
    public class ContactDto
    {
      
        public int Contactid { get; set; }
        [Required]
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
