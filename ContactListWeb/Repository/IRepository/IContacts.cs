using ContactListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWeb.Repository.IRepository
{
   public interface IContacts : IRepository<Contacts>
    {
    }
}
