using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using static WebApplication.Person;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BankContext _context;
        
        public List<Visit> Operations { get; set; }
        public List<Type> Types { get; set; }
        public List<Visit> Visits { get; set; }
        public List<Person> Persons { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Passport> Passports { get; set; }

        public IndexModel(BankContext db)
        {
            _context = db;
        }
        
        public void OnGet()
        {
            Persons = _context.Persons.AsNoTracking().ToList();
            Types = _context.Types.AsNoTracking().ToList();
            Operations = _context.Visits
                .Include(t => t.Type)
                .Include(p => p.Person)
                .OrderBy(v => v.Date)
                .ThenBy(p => p.Person.LastName)
                .ThenBy(p => p.Person.FirstName)
                .ThenBy(p => p.Person.Surname)
                .ThenBy(v => v.Sum)
                .AsNoTracking()
                .ToList();
            
            
            Visits = _context.Visits.AsNoTracking().ToList();

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