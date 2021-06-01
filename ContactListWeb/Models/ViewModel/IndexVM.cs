using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<ContactGroups> ContactGroups { get; set; }
        public IEnumerable<Contacts> Contacts { get; set; }
    }
}
