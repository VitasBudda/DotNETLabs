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
    public class CreateModel : PageModel
    {
        private readonly BankDepositContext _context;
        
        public CreateModel(BankDepositContext db)
        {
            _context = db;
        }

        public List<SelectListItem> Persons { get; set; }
        public List<SelectListItem> Types { get; set; }
        
        public void OnGet()
        {
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

                // return Page();
        }

        [BindProperty]
        public Visit Visit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _context.Visits.AddAsync(Visit);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}