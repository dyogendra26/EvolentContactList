using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWeb.Models.ViewModel
{
    public class ContactVM
    {

        public IEnumerable<SelectListItem> ContactGroup { get; set; }
        public Contacts Contacts { get; set; }
    }
}
