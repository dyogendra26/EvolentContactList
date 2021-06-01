using ContactListAPI.Data;
using ContactListAPI.Models;
using ContactListAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Repository
{
    public class ContactGroupRepository : IContactGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public ContactGroupRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool ContactGroupsExists(string name)
        {
            bool value = _db.ContactGroups.Any(a => a.GroupName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ContactGroupsExists(int id)
        {
            return _db.ContactGroups.Any(a => a.Groupid == id);
        }

        public bool CreateContactGroups(ContactGroups ContactGroups)
        {
            _db.ContactGroups.Add(ContactGroups);
            return Save();
        }

        public bool DeleteContactGroups(ContactGroups ContactGroups)
        {
            _db.ContactGroups.Remove(ContactGroups);
            return Save();
        }

        public ICollection<ContactGroups> GetContactGroups()
        {
            return _db.ContactGroups.OrderBy(a => a.GroupName).ToList();
        }

        public ContactGroups GetContactGroups(int groupid)
        {
            return _db.ContactGroups.FirstOrDefault(a => a.Groupid == groupid);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateContactGroups(ContactGroups ContactGroups)
        {
            _db.ContactGroups.Update(ContactGroups);
            return Save();
        }
    }
}
