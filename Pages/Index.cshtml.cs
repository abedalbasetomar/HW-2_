using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient; // Include this namespace for SqlConnection, SqlCommand, etc.

namespace HW_2_.Pages
{
    public class IndexModel : PageModel
    {
        private const string V = "Data Source=localdb\\MSSQLLocalDB;Initial Catalog=HWDB;Integrated Security=True";
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            // Retrieve form data
            string firstName = Request.Form["FirstName"];
            string lastName = Request.Form["LastName"];
            string address = Request.Form["Address"];

            // Connection string to your SQL Server database
            string connectionString = V;

            // SQL query to insert data into the PERSON table
            string sqlQuery = "INSERT INTO PERSON (FirstName, LastName, Address) VALUES (@FirstName, @LastName, @Address)";

            try
            {
                // Establish a connection to the database
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Open the connection
                    con.Open();

                    // Create a SQL command object with parameters to prevent SQL injection
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Address", address);

                        // Execute the SQL command
                        cmd.ExecuteNonQuery();
                    }
                }

                // Data successfully inserted
                Console.WriteLine("Data inserted successfully.");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during database interaction
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
