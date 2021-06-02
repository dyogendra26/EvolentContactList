using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactListAPI.Models;
using ContactListAPI.Models.Dtos;
using ContactListAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Contact")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _ContactRepo;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepo, IMapper mapper)
        {
            _ContactRepo = contactRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of Contacts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ContactDto>))]
        public IActionResult GetContacts()
        {
            var objList = _ContactRepo.GetContactGroups();
            var objDto = new List<ContactDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ContactDto>(obj));
            }
            return Ok(objDto);
        }


        /// <summary>
        /// Get individual Contact
        /// </summary>
        /// <param name="Contactid"> The id of the Contact </param>
        /// <returns></returns>
        [HttpGet("{Contactid:int}", Name = "GetContact")]
        [ProducesResponseType(200, Type = typeof(ContactDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public IActionResult GetContact(int Contactid)
        {
            var obj = _ContactRepo.GetContact(Contactid);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ContactDto>(obj);

            return Ok(objDto);

        }

        [HttpGet("[action]/{Groupid:int}")]
        [ProducesResponseType(200, Type = typeof(ContactDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetContactByGroup(int Groupid)
        {
            var objList = _ContactRepo.GetContactInGroup(Groupid);
            if (objList == null)
            {
                return NotFound();
            }
            var objDto = new List<ContactDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ContactDto>(obj));
            }


            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ContactDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateContact([FromBody] ContactCreateDto ContactDto)
        {
            if (ContactDto == null)
            {
                return BadRequest(ModelState);
            }
           
            var contactObj = _mapper.Map<Contacts>(ContactDto);
            if (!_ContactRepo.CreateContact(contactObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {contactObj.FirstName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetContact", new { Contactid = contactObj.Contactid }, contactObj);
           
        }

        [HttpPatch("{Contactid:int}", Name = "UpdateContact")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateContact(int contactid, [FromBody] ContactUpdateDto ContactDto)
        {
            if (ContactDto == null || contactid != ContactDto.Contactid)
            {
                return BadRequest(ModelState);
            }

            var contactObj = _mapper.Map<Contacts>(ContactDto);
            if (!_ContactRepo.UpdateContact(contactObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {contactObj.FirstName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{Contact:int}", Name = "DeleteContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteContact(int contactid)
        {
            if (!_ContactRepo.ContactExists(contactid))
            {
                return NotFound();
            }

            var contactObj = _ContactRepo.GetContact(contactid);
            if (!_ContactRepo.DeleteContact(contactObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {contactObj.FirstName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


    }
}