using ContactListWeb.Models;
using ContactListWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContactListWeb.Repository
{
    public class ContactGroupsRepository : Repository<ContactGroups>, IContactGroups
    {
        private readonly IHttpClientFactory _clientFactory;

        public ContactGroupsRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
