using Microsoft.EntityFrameworkCore;
using Lab4.Models;

namespace Lab4.Data
{
    public class BankDepositContext : DbContext
    {
        public BankDepositContext (DbContextOptions<BankDepositContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}