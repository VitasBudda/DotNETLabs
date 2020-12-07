using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lab5.Data;
using Lab5.Models;

namespace Lab5.Pages.Persons
{
    public class IndexModel : PageModel
    {
        private readonly BankDepositContext _context;
        
        public IndexModel(BankDepositContext db)
        {
            _context = db;
        }
        public List<Address> Addresses { get; set; }
        public List<Person> Persons { get; set; }
        public List<Passport> Passports { get; set; }

        public void OnGet()
        {
            Addresses = _context.Addresses.AsNoTracking().ToList();
            Passports = _context.Passports.AsNoTracking().ToList();
            Persons = _context.Persons
                .Include(a => a.Address)
                .Include(p => p.Passport)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ThenBy(p => p.Surname)
                .AsNoTracking()
                .ToList();
        }
    }
}