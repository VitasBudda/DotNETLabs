using System;
using System.Linq;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace Lab2
{
    public class BankContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;UserId=root;Database=bank_deposit");
        }
    }

    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }

    public class Passport
    {
        public int Id { get; set; }
        public string Seria { get; set; }
        public int Number { get; set; }
    }

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

    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

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
    
    class Program
    {
        static void Main(string[] args)
        {
            using (BankContext db = new BankContext())
            {
                var operations = db.Visits
                    .Include(t => t.Type)
                    .Include(p => p.Person)
                    .OrderBy(v => v.Date)
                    .ThenBy(p => p.Person.LastName)
                    .ThenBy(p => p.Person.FirstName)
                    .ThenBy(p => p.Person.Surname)
                    .ThenBy(v => v.Sum);

                Console.WriteLine("Bank operations:");
                foreach (var operation in operations)
                {
                    Console.WriteLine($"{operation.Date}, {operation.Sum}, {operation.Type.Name}, " +
                                      $"{operation.Person.LastName} {operation.Person.FirstName} " +
                                      $"{operation.Person.Surname}");
                }

                var visits = db.Visits
                    .Include(p => p.Person)
                    .GroupBy(p => new 
                    {
                        FirstName = p.Person.FirstName,
                        LastName = p.Person.LastName,
                        Surname = p.Person.Surname
                    })
                    .Select(p => new
                    {
                        Person = p.Key,
                        Count = p.Count()
                    });

                Console.WriteLine("Visits:");
                foreach (var visit in visits)
                {
                    Console.WriteLine($"{visit.Person.LastName} {visit.Person.FirstName} " +
                                      $"{visit.Person.Surname}, {visit.Count} - number of visits");
                }

                var persons = db.Persons
                    .Include(a => a.Address)
                    .Include(p => p.Passport)
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ThenBy(p => p.Surname);

                Console.WriteLine("Persons data:");
                foreach (var person in persons)
                {
                    Console.WriteLine($"{person.LastName} {person.FirstName} " +
                                      $"{person.Surname}, {person.Address.City} " +
                                      $"{person.Address.Street} {person.Address.HouseNumber}/" +
                                      $"{person.Address.ApartmentNumber}, {person.Passport.Seria} " +
                                      $"{person.Passport.Number}");
                }
            }
        }
    }
}