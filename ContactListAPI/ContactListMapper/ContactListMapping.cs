using ContactListAPI.Models.Dtos;
using ContactListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ContactListAPI.ContactListMapper
{
    public class ContactListMapping : Profile
    {
        public ContactListMapping()
        {
            // Create Mapping for ContactGroup and ContactGroupDto
            CreateMap<ContactGroups, ContactGroupsDto>().ReverseMap();
            CreateMap<Contacts, ContactDto>().ReverseMap();
            CreateMap<Contacts, ContactCreateDto>().ReverseMap();
            CreateMap<Contacts, ContactUpdateDto>().ReverseMap();
        }
    }
}
