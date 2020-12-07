using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Type = Lab4.Models.Type;

namespace Lab4.Controllers
{
    public class VisitsController : Controller
    {
        private readonly BankDepositContext _context;

        public VisitsController(BankDepositContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Visit> visits = _context.Visits
                .Include(t => t.Type)
                .Include(p => p.Person)
                .OrderBy(v => v.Date)
                .ThenBy(p => p.Person.LastName)
                .ThenBy(p => p.Person.FirstName)
                .ThenBy(v => v.Sum)
                .ToList();
            
            return View(visits);
        }
        
        public IActionResult Create()
        {
            PopulateTypesDropDownList();
            PopulatePersonsDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visit visit)
        {
            await _context.Visits.AddAsync(visit);
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                    .Include(t => t.Type)
                    .Include(p => p.Person)
                    .FirstOrDefaultAsync(i => i.Id == id.Value);
            if (visit == null)
            {
                return NotFound();
            }
            PopulateTypesDropDownList(visit.TypeId);
            PopulatePersonsDropDownList(visit.PersonId);

            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var visit = await _context.Visits
                .Include(t => t.Type)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            if (id != visit.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                await TryUpdateModelAsync(
                    visit,
                    "",
                    v => v.Date, v => v.Sum, 
                    v => v.TypeId, v => v.PersonId);
                    
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(visit);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(t => t.Type)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(i => i.Id == id.Value);
            if (visit == null)
            {
                return NotFound();
            }
            PopulateTypesDropDownList(visit.TypeId);
            PopulatePersonsDropDownList(visit.PersonId);

            return View(visit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visits
                .Include(t => t.Type)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(i => i.Id == id);
            
            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
        
        private void PopulateTypesDropDownList(object selectedType = null)
        {
            var typesQuery = from t in _context.Types
                orderby t.Name
                select t;
            ViewBag.TypeId = new SelectList(typesQuery, "Id", "Name", selectedType);
        }
        
        private void PopulatePersonsDropDownList(object selectedPerson = null)
        {
            var persons = from p in _context.Persons
                orderby p.LastName, p.FirstName, p.Surname
                select p;
            ViewBag.PersonId = new SelectList(persons, "Id", "FullName", selectedPerson);
        }
    }
}