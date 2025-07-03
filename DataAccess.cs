//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace ProductSalesReport
//{
//    public class DataAccess
//    {
//        public AutoCompleteStringCollection GetAllProductNames()
//        {
//            var customSource = new AutoCompleteStringCollection();
//            string connectionString = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
//            string sql = "SELECT DISTINCT PRODUCTNAME FROM PRODUCTSALES";

//            using (var connection = new SqlConnection(connectionString))
//            {
//                using (var command = new SqlCommand(sql, connection))
//                {
//                    try
//                    {
//                        connection.Open();
//                        using (var reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                customSource.Add(reader["PRODUCTNAME"].ToString());
//                            }
//                        }
//                    }
//                    catch (SqlException) { /* Optional: log errors here too */ }
//                }
//            }
//            return customSource;
//        }

//        public List<saleDto> GetSalesData(DateTime startDate, DateTime endDate, string productNameFilter)
//        {
//            var salesData = new List<saleDto>();
//            string connectionString = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;

//            // Using >= and <= is often more reliable for date ranges than BETWEEN
//            string sql = @"SELECT PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE 
//                   FROM PRODUCTSALES 
//                   WHERE SALEDATE >= @StartDate AND SALEDATE <= @EndDate 
//                   AND (PRODUCTNAME LIKE @ProductNameFilter OR @ProductNameFilter IS NULL)";

//            using (var connection = new SqlConnection(connectionString))
//            {
//                using (var command = new SqlCommand(sql, connection))
//                {
//                    // The FIX: Use .Date to send only the date part, stripping any time information.
//                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
//                    command.Parameters.AddWithValue("@EndDate", endDate.Date);

//                    if (!string.IsNullOrEmpty(productNameFilter))
//                    {
//                        command.Parameters.AddWithValue("@ProductNameFilter", "%" + productNameFilter + "%");
//                    }
//                    else
//                    {
//                        command.Parameters.AddWithValue("@ProductNameFilter", DBNull.Value);
//                    }

//                    try
//                    {
//                        connection.Open();
//                        using (var reader = command.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                var sale = new saleDto
//                                {
//                                    ProductCode = reader["PRODUCTCODE"].ToString(),
//                                    ProductName = reader["PRODUCTNAME"].ToString(),
//                                    Quantity = (int)reader["QUANTITY"],
//                                    UnitPrice = (decimal)reader["UNITPRICE"],
//                                    SaleDate = (DateTime)reader["SALEDATE"]
//                                };
//                                sale.Total = sale.Quantity * sale.UnitPrice;
//                                salesData.Add(sale);
//                            }
//                        }
//                    }
//                    catch (SqlException ex)
//                    {
//                        LogError(ex.Message);
//                        return null;
//                    }
//                }
//            }
//            return salesData;
//        }

//        private void LogError(string message)
//        {
//            string logDirectory = "logs";
//            string logFilePath = Path.Combine(logDirectory, "errors.txt");
//            Directory.CreateDirectory(logDirectory);
//            string errorMessage = $"{DateTime.Now}: SQL Error: {message}{Environment.NewLine}";
//            File.AppendAllText(logFilePath, errorMessage);
//        }


//    }
//}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ProductSalesReport
{
    public class DataAccess
    {
        public List<saleDto> GetSalesData(DateTime startDate, DateTime endDate, string productNameFilter)
        {
            var salesData = new List<saleDto>();
            string connectionString = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;

            string sql = @"SELECT PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE 
                           FROM PRODUCTSALES 
                           WHERE SALEDATE >= @StartDate AND SALEDATE <= @EndDate 
                           AND (PRODUCTNAME LIKE @ProductNameFilter OR @ProductNameFilter IS NULL)";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);

                    if (!string.IsNullOrEmpty(productNameFilter))
                    {
                        command.Parameters.AddWithValue("@ProductNameFilter", "%" + productNameFilter + "%");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ProductNameFilter", DBNull.Value);
                    }

                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var sale = new saleDto
                                {
                                    ProductCode = reader["PRODUCTCODE"].ToString(),
                                    ProductName = reader["PRODUCTNAME"].ToString(),
                                    Quantity = (int)reader["QUANTITY"],
                                    UnitPrice = (decimal)reader["UNITPRICE"],
                                    SaleDate = (DateTime)reader["SALEDATE"]
                                };
                                sale.Total = sale.Quantity * sale.UnitPrice;
                                salesData.Add(sale);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        LogError("GetSalesData failed: " + ex.Message);
                        return null;
                    }
                }
            }
            return salesData;
        }

        public AutoCompleteStringCollection GetAllProductNames()
        {
            var customSource = new AutoCompleteStringCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString;
            string sql = "SELECT DISTINCT PRODUCTNAME FROM PRODUCTSALES";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customSource.Add(reader["PRODUCTNAME"].ToString());
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        LogError("GetAllProductNames failed: " + ex.Message);
                    }
                }
            }
            return customSource;
        }

        private void LogError(string message)
        {
            try
            {
                string logDirectory = "logs";
                string logFilePath = Path.Combine(logDirectory, "errors.txt");
                Directory.CreateDirectory(logDirectory);
                string errorMessage = $"{DateTime.Now}: {message}{Environment.NewLine}";
                File.AppendAllText(logFilePath, errorMessage);
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
