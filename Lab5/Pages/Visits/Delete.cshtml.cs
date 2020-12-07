using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lab5.Data;
using Lab5.Models;

namespace Lab5.Pages.Visits
{
    public class DeleteModel : PageModel
    {
        private readonly BankDepositContext _context;
        
        public DeleteModel(BankDepositContext db)
        {
            _context = db;
        }

        public List<SelectListItem> Persons { get; set; }
        public List<SelectListItem> Types { get; set; }
        
        [BindProperty]
        public Visit Visit { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Visit = await _context.Visits.FirstOrDefaultAsync(v => v.Id == id);

            if (Visit == null)
            {
                return NotFound();
            }
            
            Persons = _context.Persons.Select(a => 
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text =  a.FullName
                }).ToList();
            
            Types = _context.Types.Select(t =>
                new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Visits.Remove(Visit);
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(Visit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}