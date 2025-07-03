# C# WinForms Sales Reporting Application

This is a WinForms application built with C# and .NET Framework to generate sales reports from a SQL Server database. The application allows users to filter sales data by date range and product name, view the report in a print preview window, and export it as a PDF.

## ✨ Features

- **Dynamic Reporting**: Generate sales reports based on a user-selected date range.
- **Data Grouping**: Reports are grouped by product code, with summaries for each product.
- **Grand Totals**: The report includes grand totals for quantity and revenue.
- **PDF Export**: Export the generated report directly to a PDF file and open it automatically.
- **Product Name Filter**: Filter the report by a specific product name with an autocomplete suggestion feature.
- **Database Error Logging**: SQL exceptions are logged to a local logs/errors.txt file.
- **Report Parameters**: The selected date range is displayed in the report's header for clarity.
- **Database Connection Test**: The application tests the database connection on startup for immediate user feedback.

## 🛠️ Setup Instructions

Follow these steps to set up and run the project on your local machine.

### Prerequisites

- **Visual Studio 2019 or later**: With the ".NET desktop development" workload installed.
- **SQL Server**: Any version of SQL Server (e.g., Express, Developer, or LocalDB).
- **DevExpress WinForms Components**: A trial or licensed version must be installed.

### 1. Database Setup

1. Open SQL Server Management Studio (SSMS).
2. Create a new, empty database named `SalesDB`.
3. Open the `SalesDB.sql` script (included in this repository) in a new query window.
4. Execute the script to create the `PRODUCTSALES` table and insert the sample data.
5. Ensure your Windows user account (e.g., `YOUR_PC_NAME\Your_User`) has `db_datareader` and `db_datawriter` permissions on the `SalesDB` database.

### 2. Application Configuration

1. Open the `ProductSalesReport.sln` file in Visual Studio.
2. Open the `App.config` file. Or copy from App.config.example but change to your connection string.
3. Update the connection string to point to your SQL Server instance. See the examples below.

### 3. Running the Application

1. In Visual Studio, Build the solution (Build > Build Solution).
2. Run the project by pressing F5 or clicking the "Start" button.

## 🔌 Connection String

You must update the `connectionString` in the `App.config` file to match your database server.

**For a named SQL Server instance (e.g., "SQLEXPRESS"):**

```xml
<add name="SalesDB"
     connectionString="Data Source=YOUR_PC_NAME\YOUR_INSTANCE_NAME;Database=SalesDB;Trusted_Connection=true;TrustServerCertificate=true;"
     providerName="System.Data.SqlClient"/>
```

**For SQL Server LocalDB (often installed with Visual Studio):**

```xml
<add name="SalesDB"
     connectionString="Server=(localdb)\mssqllocaldb;Database=SalesDB;Trusted_Connection=True;"
     providerName="System.Data.SqlClient"/>
```

## 🎯 Bonus Requirements

| Feature                     | Description                                            | Completion |
| --------------------------- | ------------------------------------------------------ | ---------- |
| Dynamic Export              | Add a button to export report as PDF                   | ✅         |
| Filter by ProductName       | Add a textbox to filter products by partial name match | ✅         |
| Use of Stored Procedure     | Replace raw SQL with a stored procedure                | ❌         |
| Report Parameters in Header | Display StartDate & EndDate on top of report           | ✅         |
| Khmer Font Support          | Use Unicode-friendly font (for multilingual reports)   | ❌         |

## 📸 Screenshots (Optional)

Below are screenshots of the application's user interface and a sample of the generated report.

**Application UI:**
![alt text](<Screenshots/Form UI.png>)
![alt text](<Screenshots/Export PDF.png>)

**Generated Report:**
![![Report Screenshot]](<Screenshots/PDF Report.png>)
![alt text](<Screenshots/XtraReport Preview.png>)

**Error Handling:**
![alt text](<Screenshots/Error Handling.png>)
![alt text](<Screenshots/SQL Error_Log.png>)
