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
    public class PersonsController : Controller
    {
        private readonly IAppBLL _bll;

        public PersonsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Persons
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Persons.AllAsync());
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FindAsync(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Id")] Person person)
        {
            if (ModelState.IsValid)
            {
                await _bll.Persons.AddAsync(person);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Id")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.Persons.Update(person);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _bll.Persons.FindAsync(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _bll.Persons.Remove(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}