using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Lab5.Data;
using Lab5.Models;

namespace Lab5.Pages.Visits
{
    public class IndexModel : PageModel
    {
        private readonly BankDepositContext _context;
        
        public IndexModel(BankDepositContext db)
        {
            _context = db;
        }
        
        public List<Visit> Visits { get; set; }
        public List<Person> Persons { get; set; }
        public List<Type> Types { get; set; }

        public void OnGet()
        {
            Persons = _context.Persons.AsNoTracking().ToList();
            Types = _context.Types.AsNoTracking().ToList();
            Visits = _context.Visits
                .Include(t => t.Type)
                .Include(p => p.Person)
                .OrderBy(v => v.Date)
                .ThenBy(p => p.Person.LastName)
                .ThenBy(p => p.Person.FirstName)
                .ThenBy(p => p.Person.Surname)
                .ThenBy(v => v.Sum)
                .AsNoTracking()
                .ToList();
        }
    }
}