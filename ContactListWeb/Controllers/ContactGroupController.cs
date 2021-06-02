using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactListWeb.Models;
using ContactListWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactListWeb.Controllers
{
    public class ContactGroupController : Controller
    {

        private readonly IContactGroups _npRepo;

        public ContactGroupController(IContactGroups npRepo)
        {
            _npRepo = npRepo;
        }

        public IActionResult Index()
        {
            return View(new ContactGroups() { });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            ContactGroups obj = new ContactGroups();

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await _npRepo.GetAsync(SD.ContactGroupAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ContactGroups obj)
        {
            if (ModelState.IsValid)
            {
               
                if (obj.Groupid == 0)
                {
                    await _npRepo.CreateAsync(SD.ContactGroupAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _npRepo.UpdateAsync(SD.ContactGroupAPIPath + obj.Groupid, obj, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllGroups()
        {
            return Json(new { data = await _npRepo.GetAllAsync(SD.ContactGroupAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _npRepo.DeleteAsync(SD.ContactGroupAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }

    }
}