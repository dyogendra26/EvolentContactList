using System;
using System.Collections.Generic;
using AutoMapper;
using ContactListAPI.Models;
using ContactListAPI.Models.Dtos;
using ContactListAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers
{
    [Route("api/v{version:apiVersion}/ContactGroup")]
    [ApiVersion("2.0")]
    [ApiController]
    public class ContactGroupV2Controller : Controller
    {
        private readonly IContactGroupRepository _contactGroupRepository;
        private readonly IMapper _mapper;

        public ContactGroupV2Controller(IContactGroupRepository contactGroupRepository, IMapper mapper)
        {
            _contactGroupRepository = contactGroupRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of Contact Groups.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ContactGroupsDto>))]
        public IActionResult GetContactGroups()
        {
            var objList = _contactGroupRepository.GetContactGroups();
            // Create DTO object
            var objDto = new List<ContactGroupsDto>();
            foreach (var obj in objList)
            {
                //Map DTO
                objDto.Add(_mapper.Map<ContactGroupsDto>(obj));
            }
            //return DTO object
            return Ok(objDto);
        }


    }
}