using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3
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
