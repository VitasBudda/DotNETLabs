using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Lab4.Models
{
    public class Passport
    {
        public int Id { get; set; }
        public string Seria { get; set; }
        public int Number { get; set; }
    }
}