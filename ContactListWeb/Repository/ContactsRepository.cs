using ContactListWeb.Models;
using ContactListWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContactListWeb.Repository
{
    public class ContactsRepository : Repository<Contacts>, IContacts
    {
        private readonly IHttpClientFactory _clientFactory;

    public ContactsRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }
}
}
