using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab4.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Sum { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}