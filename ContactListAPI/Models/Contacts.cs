using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactListAPI.Models
{
    public class Contacts
    {
        [Key]
        public int Contactid { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] Picture { get; set; }
        public int Groupid { get; set; }

        [ForeignKey("Groupid")]
        public ContactGroups ContactGroup { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
