﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3
{
    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string AccountNumber { get; set; }

        public int PassportId { get; set; }
        public Passport Passport { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
