using ContactListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Repository.IRepository
{
   public interface IContactRepository
    {
        ICollection<Contacts> GetContactGroups();
        ICollection<Contacts> GetContactInGroup(int gpId);
        Contacts GetContact(int Contactid);
        bool ContactExists(string Firstname);
        bool ContactExists(int Contactid);
        bool CreateContact(Contacts Contacts);
        bool UpdateContact(Contacts Contacts);
        bool DeleteContact(Contacts Contacts);
        bool Save();
    }
}
