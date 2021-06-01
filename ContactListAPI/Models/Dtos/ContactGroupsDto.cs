using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Models.Dtos
{
    public class ContactGroupsDto
    {
        [Key]
        public int Groupid { get; set; }

        [Required]
        public string GroupName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
