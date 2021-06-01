using ContactListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Repository.IRepository
{
    public interface IContactGroupRepository
    {
        ICollection<ContactGroups> GetContactGroups();
        ContactGroups GetContactGroups(int groupid);
        bool ContactGroupsExists(string name);
        bool ContactGroupsExists(int id);
        bool CreateContactGroups(ContactGroups ContactGroups);
        bool UpdateContactGroups(ContactGroups ContactGroups);
        bool DeleteContactGroups(ContactGroups ContactGroups);
        bool Save();
    }
}
