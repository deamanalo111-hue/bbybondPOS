using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace BabybondPOSInventorySystem
{
    public partial class Reports: Form
    {
        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        public Reports()
        {
            InitializeComponent();   
        }

        private void LoadReportData()

        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();




                //running --------------------------------------------------------------------------------------------
                string query = @"
                    SELECT 
                        p.Id AS ProductNo,
                        p.Product,
                        p.Description,
                        ISNULL(l.Quantity, 0) AS OpeningInventory,
                        ISNULL(r.StockIn, 0) AS StockIn,
                        ISNULL(l.Quantity, 0) + ISNULL(r.StockIn, 0) AS CompTotal,
                        ISNULL(r.Used, 0) AS Used,
                        ISNULL(r.Wasted, 0) AS Wasted,
                        (ISNULL(l.Quantity, 0) + ISNULL(r.StockIn, 0)) - ISNULL(r.Used, 0) - ISNULL(r.Wasted, 0) AS CompClosing,
                        ISNULL(r.ActualClosingInventory, 0) AS ActualClosingInventory
                    FROM 
                        Products p
                    LEFT JOIN 
                        ListQuantity l ON p.Id = l.ProductID
                    LEFT JOIN 
                        InventoryReports r ON p.Id = r.ReportID";
                //  runing---------------------------------------------------------------- -
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    grdReport.DataSource = dt;


                    // Make the DataGridView read-only
                    grdReport.ReadOnly = true;
                    grdReport.Columns["ProductNo"].Visible = false;

                   

                    //grdReport.Columns["ProductNo"].HeaderText = "Product No.";
                    grdReport.Columns["Product"].HeaderText = "Product Name";
                    grdReport.Columns["Description"].HeaderText = "Desc";
                    grdReport.Columns["OpeningInventory"].HeaderText = "Opening Inventory";
                    grdReport.Columns["StockIn"].HeaderText = "Stock In";
                    grdReport.Columns["CompTotal"].HeaderText = "Comp Total";
                    grdReport.Columns["Used"].HeaderText = "Used";
                    grdReport.Columns["Wasted"].HeaderText = "Wasted";
                    grdReport.Columns["CompClosing"].HeaderText = "Comp Closing";
                    grdReport.Columns["ActualClosingInventory"].HeaderText = "Actual Closing Inventory";
                }
            }
        }
        //running -------------------------------------------------------------------------------------------
        private void btnCalendar_Click(object sender, EventArgs e)   // remove the datetimepicker if its not necessary
        {
          //  LoadReportByDate(dtpReportDate.Value.Date);
        }
        //private void LoadReportByDate(DateTime selectedDate)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string query = @"
        //            SELECT 
        //                p.Id AS [Product No.],
        //                p.ProductName AS [Product Name],
        //                l.Quantity AS [Opening Inventory],
        //                ir.StockIn,
        //                ir.CompTotal,
        //                ir.Used,
        //                ir.Wasted, 
        //                ir.CompClosing,
        //                ir.ActualClosingInventory
        //            FROM InventoryReports ir
        //            INNER JOIN Products p ON ir.ReportID = p.Id
        //            LEFT JOIN ListQuantity l ON p.Id = l.ProductID
        //            WHERE CAST(ir.ReportDate AS DATE) = @SelectedDate";

        //        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        //        adapter.SelectCommand.Parameters.AddWithValue("@SelectedDate", selectedDate);

        //        DataTable dt = new DataTable();
        //        adapter.Fill(dt);
        //        grdReport.DataSource = dt;
        //    }
        //}

        private void Reports_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }


    }
}
