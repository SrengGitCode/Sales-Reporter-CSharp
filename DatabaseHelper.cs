using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductSalesReport
{
    internal class DatabaseHelper
    {
        public static void TestConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    string errorMessage = "Failed to connect to the database. Please check your App.config connection string and ensure the SQL Server is running.\n\nError: " + ex.Message;
                    MessageBox.Show(errorMessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
