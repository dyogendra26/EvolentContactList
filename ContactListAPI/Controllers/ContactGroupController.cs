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
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ContactGroupController : ControllerBase
    {
        private readonly IContactGroupRepository _contactGroupRepository;
        private readonly IMapper _mapper;

        public ContactGroupController(IContactGroupRepository contactGroupRepository, IMapper mapper)
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

        /// <summary>
        /// Get individual national park
        /// </summary>
        /// <param name="ContactGroupsId"> The Id of the national Park </param>
        /// <returns></returns>
        [HttpGet("{ContactGroupsId:int}", Name = "GetContactGroups")]
        [ProducesResponseType(200, Type = typeof(ContactGroupsDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetContactGroups(int ContactGroupsId)
        {
            var obj = _contactGroupRepository.GetContactGroups(ContactGroupsId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ContactGroupsDto>(obj);
            //var objDto = new ContactGroupsDto()
            //{
            //    Created = obj.Created,
            //    Id = obj.Id,
            //    Name = obj.Name,
            //    State = obj.State,
            //};
            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ContactGroupsDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateContactGroups([FromBody] ContactGroupsDto ContactGroupsDto)
        {
            if (ContactGroupsDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_contactGroupRepository.ContactGroupsExists(ContactGroupsDto.GroupName))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);
            }
            var ContactGroupsObj = _mapper.Map<ContactGroups>(ContactGroupsDto);
            if (!_contactGroupRepository.CreateContactGroups(ContactGroupsObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {ContactGroupsObj.GroupName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetContactGroups", new { ContactGroupsId = ContactGroupsObj.Groupid }, ContactGroupsObj);
        }

        [HttpPatch("{ContactGroupsId:int}", Name = "UpdateContactGroups")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateContactGroups(int ContactGroupsId, [FromBody] ContactGroupsDto ContactGroupsDto)
        {
            if (ContactGroupsDto == null || ContactGroupsId != ContactGroupsDto.Groupid)
            {
                return BadRequest(ModelState);
            }

            var ContactGroupsObj = _mapper.Map<ContactGroups>(ContactGroupsDto);
            if (!_contactGroupRepository.UpdateContactGroups(ContactGroupsObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {ContactGroupsObj.GroupName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{ContactGroupsId:int}", Name = "DeleteContactGroups")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteContactGroups(int ContactGroupsId)
        {
            if (!_contactGroupRepository.ContactGroupsExists(ContactGroupsId))
            {
                return NotFound();
            }

            var ContactGroupsObj = _contactGroupRepository.GetContactGroups(ContactGroupsId);
            if (!_contactGroupRepository.DeleteContactGroups(ContactGroupsObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {ContactGroupsObj.GroupName}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}