using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContactListWeb.Models;
using ContactListWeb.Models.ViewModel;
using ContactListWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContactListWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContacts _contactRepo;
        private readonly IContactGroups _contactGroups;

        public ContactController(IContacts contactrepo, IContactGroups contactGroups )
        {
            _contactRepo = contactrepo;
            _contactGroups = contactGroups;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            //ContactVM obj = new ContactVM();
            IEnumerable<ContactGroups> npList = await _contactGroups.GetAllAsync(SD.ContactGroupAPIPath, HttpContext.Session.GetString("JWToken"));

            ContactVM obj = new ContactVM()
            {
                ContactGroup = npList.Select(i => new SelectListItem
                {
                    Text = i.GroupName,
                    Value = i.Groupid.ToString()
                }),
                Contacts = new Contacts()
            };

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj.Contacts = await _contactRepo.GetAsync(SD.ContactAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ContactVM obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Contacts.Picture = p1;
                }
                else
                {
                    var objFromDb = await _contactRepo.GetAsync(SD.ContactAPIPath, obj.Contacts.Contactid, HttpContext.Session.GetString("JWToken"));
                    obj.Contacts.Picture = objFromDb.Picture;
                }
                if (obj.Contacts.Contactid == 0)
                {
                    await _contactRepo.CreateAsync(SD.ContactAPIPath, obj.Contacts, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _contactRepo.UpdateAsync(SD.ContactAPIPath + obj.Contacts.Contactid, obj.Contacts, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }


        public IActionResult Index()
        {
            return View( new Contacts() { });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllContacts()
        {
            return Json(new { data = await _contactRepo.GetAllAsync(SD.ContactAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _contactRepo.DeleteAsync(SD.ContactAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}