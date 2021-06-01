using ContactListAPI.Data;
using ContactListAPI.Models;
using ContactListAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _db;

        public ContactRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public bool CreateContact(Contacts contacts)
        {
            _db.Contacts.Add(contacts);
            return Save();
        }

        public bool DeleteContact(Contacts contacts)
        {
            _db.Contacts.Remove(contacts);
            return Save();
        }

        public Contacts GetContact(int Contactid)
        {
            return _db.Contacts.Include(c => c.ContactGroup).FirstOrDefault(a => a.Contactid == Contactid );
        }

        public ICollection<Contacts> GetContactGroups()
        {
            return _db.Contacts.Include(c => c.ContactGroup).OrderBy(a => a.FirstName).ToList();
        }

        public bool ContactExists(string name)
        {
            bool value = _db.Contacts.Any(a => a.FirstName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ContactExists(int id)
        {
            return _db.Contacts.Any(a => a.Contactid == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateContact(Contacts contacts)
        {
            _db.Contacts.Update(contacts);
            return Save();
        }

        public ICollection<Contacts> GetContactInGroup(int gpId)
        {
            return _db.Contacts.Include(c => c.ContactGroup).Where(c => c.Groupid == gpId).ToList();
        }
       
    }
}
