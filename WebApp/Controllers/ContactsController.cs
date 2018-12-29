using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;

namespace WebApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IAppBLL _bll;

        public ContactsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var contacts = await _bll.Contacts.AllAsync();
            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _bll.Contacts.FindAsync(id.Value);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public async Task<IActionResult>Create()
        {
            ViewData["ContactTypeId"] = new SelectList(await _bll.ContactTypes.AllAsync(), "Id", "Value");
            ViewData["PersonId"] = new SelectList(await _bll.Persons.AllAsync(), "Id", "FirstLastName");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,PersonId,ContactTypeId,Id")]
            Contact contact)
        {
            if (ModelState.IsValid)
            {
                _bll.Contacts.Add(contact);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ContactTypeId"] =
                new SelectList(await _bll.ContactTypes.AllAsync(), "Id", "Value", contact.ContactTypeId);
            ViewData["PersonId"] =
                new SelectList(await _bll.Persons.AllAsync(), "Id", "FirstLastName", contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _bll.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewData["ContactTypeId"] =
                new SelectList(await _bll.ContactTypes.AllAsync(), "Id", "Value", contact.ContactTypeId);
            ViewData["PersonId"] =
                new SelectList(await _bll.Persons.AllAsync(), "Id", "FirstLastName", contact.PersonId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Value,PersonId,ContactTypeId,Id")]
            Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Contacts.Update(contact);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ContactTypeId"] =
                new SelectList(await _bll.ContactTypes.AllAsync(), "Id", "Value", contact.ContactTypeId);
            ViewData["PersonId"] =
                new SelectList(await _bll.Persons.AllAsync(), "Id", "FirstLastName", contact.PersonId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _bll.Contacts.FindAsync(id.Value);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.Contacts.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}