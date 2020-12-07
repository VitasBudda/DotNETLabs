using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Lab4.Data;
using Lab4.Models;

namespace Lab4.Controllers
{
    public class NumberOfVisitsController : Controller
    {
        private readonly BankDepositContext _context;

        public NumberOfVisitsController(BankDepositContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
           List<Visit> visits = _context.Visits
               .Include(t => t.Type)
               .Include(p => p.Person)
               .ToList();
            
            return View(visits);
        }
    }
}