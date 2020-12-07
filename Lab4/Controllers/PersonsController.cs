using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lab4.Data;
using Lab4.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Lab4.Controllers
{
    public class PersonsController : Controller
    {
        private readonly BankDepositContext _context;

        public PersonsController(BankDepositContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Person> persons = _context.Persons
                .Include(a => a.Address)
                .Include(p => p.Passport)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.Surname)
                .ToList();

            return View(persons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            await _context.Addresses.AddAsync(person.Address);
            await _context.Passports.AddAsync(person.Passport);
            await _context.Persons.AddAsync(person);
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(a => a.Address)
                .Include(p => p.Passport)
                .FirstOrDefaultAsync(i => i.Id == id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _context.Persons
                .Include(a => a.Address)
                .Include(p => p.Passport)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            if (id != person.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                await TryUpdateModelAsync(
                    person,
                    "",
                    p => p.FirstName, p => p.LastName, 
                    p => p.Surname, p => p.Passport, p => p.Address);
                    
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .Include(a => a.Address)
                .Include(p => p.Passport)
                .FirstOrDefaultAsync(i => i.Id == id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons
                .Include(a => a.Address)
                .Include(p => p.Passport)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            _context.Persons.Remove(person);
            _context.Addresses.Remove(person.Address);
            _context.Passports.Remove(person.Passport);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
    }
}