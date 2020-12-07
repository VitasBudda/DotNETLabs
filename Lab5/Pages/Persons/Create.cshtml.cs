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

namespace Lab5.Pages.Persons
{
    public class CreateModel : PageModel
    {
        private readonly BankDepositContext _context;
        
        public CreateModel(BankDepositContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
            //return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _context.Addresses.AddAsync(Person.Address);
            await _context.Passports.AddAsync(Person.Passport);
            await _context.Persons.AddAsync(Person);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}