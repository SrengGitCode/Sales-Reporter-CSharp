using DevExpress.XtraPrinting.Native.WebClientUIControl;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductSalesReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseHelper.TestConnection();

            // Configure the textbox for AutoComplete
            DataAccess dataAccess = new DataAccess();
            txtProductName.AutoCompleteCustomSource = dataAccess.GetAllProductNames();
            txtProductName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }
        private List<saleDto> FetchAndValidateData()
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;
            string productNameFilter = txtProductName.Text;

            DataAccess dataAccess = new DataAccess();
            var salesData = dataAccess.GetSalesData(startDate, endDate, productNameFilter);

            if (salesData == null || salesData.Count == 0)
            {
                MessageBox.Show("No results found for the selected criteria.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return salesData;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            var salesData = FetchAndValidateData();
            if (salesData == null) return;

            XtraReport1 report = new XtraReport1();
            report.Parameters["paramStartDate"].Value = dtpStartDate.Value;
            report.Parameters["paramEndDate"].Value = dtpEndDate.Value;
            report.DataSource = salesData;
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();

        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            var salesData = FetchAndValidateData();
            if (salesData == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Save the Report as PDF",
                FileName = $"Product_Sales_Report_{DateTime.Now:yyyy-MM-dd_HHmmss}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XtraReport1 report = new XtraReport1();
                report.Parameters["paramStartDate"].Value = dtpStartDate.Value;
                report.Parameters["paramEndDate"].Value = dtpEndDate.Value;
                report.DataSource = salesData;
                report.ExportToPdf(saveFileDialog.FileName);

                // Open PDF with default viewer
                Process.Start(new ProcessStartInfo
                {
                    FileName = saveFileDialog.FileName,
                    UseShellExecute = true
                });
            }


        }
    }
    }

