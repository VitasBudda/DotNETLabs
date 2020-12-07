using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ConsoleTables;
using FSharp.Data.Sql.Providers;
using MySql.Data.MySqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";  
                builder.UserID = "root";             
                builder.Password = "";    
                builder.InitialCatalog = "bank_deposits";

                string queryString = 
                    "SELECT p.last_name, p.first_name, p.surname, v.date, v.sum, t.name " + 
                    "FROM persons p " +
                    "INNER JOIN visits v ON v.person_id = p.id " +
                    "INNER JOIN types t ON v.type_id = t.id " +
                    "ORDER BY p.last_name, p.first_name, p.surname, v.date;";
                string queryString1 = 
                    "SELECT p.last_name, p.first_name, p.surname, " +
                        "count(v.person_id) as number_of_visits " + 
                    "FROM persons p " +
                    "INNER JOIN visits v ON v.person_id = p.id " +
                    "INNER JOIN types t ON v.type_id = t.id " +
                    "GROUP BY p.last_name, p.first_name, p.surname;";
                string queryString2 = 
                    "SELECT p.last_name, p.first_name, p.surname, a.city, a.street, " +
                        "a.house_number, a.apartment_number, ps.seria, ps.number " + 
                    "FROM addresses a " +
                    "INNER JOIN persons p ON p.address_id = a.id " +
                    "INNER JOIN passports ps ON p.passport_id = ps.id " +
                    "ORDER BY p.last_name, p.first_name, p.surname;";
                    
                
                using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
                {
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    MySqlCommand command1 = new MySqlCommand(queryString1, connection);
                    MySqlCommand command2 = new MySqlCommand(queryString2, connection);
                    connection.Open();

                    Console.WriteLine("Bank operations:");
                    var table = new ConsoleTable("Last Name", "First Name", 
                        "Surname", "Date", "Sum", "Type");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        table.AddRow(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                    }
                    table.Write();
                    reader.Close();
                    
                    Console.WriteLine("Visits:");
                    table = new ConsoleTable("Last Name", "First Name", 
                        "Surname", "Number of visits");
                    reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        table.AddRow(reader[0], reader[1], reader[2], reader[3]);
                    }
                    table.Write();
                    reader.Close();
                    
                    Console.WriteLine("Persons data:");
                    table = new ConsoleTable("Last Name", "First Name", 
                        "Surname", "City", "Street", "House number", "Apartment", "Passport seria", 
                        "Passport number");
                    reader = command2.ExecuteReader();
                    while (reader.Read())
                    {
                        table.AddRow(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], 
                            reader[7], reader[8]);
                    }
                    table.Write();
                    reader.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}