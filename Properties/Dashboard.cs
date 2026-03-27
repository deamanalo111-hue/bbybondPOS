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

namespace BabybondPOSInventorySystem
{
    public partial class Dashboard: Form
    {
        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        public Dashboard()
        {
            InitializeComponent();
            LoadTotalPackages();
        }

        private void LoadTotalPackages()
        {
           

            // total number of packages
            string query = "SELECT COUNT(*) FROM tblPackage";  
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Execute the query and get the result
                        int totalPackages = (int)cmd.ExecuteScalar(); 

                        // show to label
                        lbTotalPckg.Text =  totalPackages.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);  
                }
            }
        }
    }
}
