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
    public partial class History: Form
    {
        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        public History()
        {
            InitializeComponent();
            RefreshHistory();
        }
        private void LoadHistory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT a.Username, h.HistoryRecord, h.Activities
            FROM tblHistory h
            JOIN tblAccounts a ON h.AccountId = a.Id
            ORDER BY h.HistoryRecord DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                grdHistory.DataSource = table;
                grdHistory.Columns["HistoryRecord"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }

          

        }
       
        public void RefreshHistory()
        {
            using (SqlConnection con = new SqlConnection("Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True"))
            {
                string query = @"SELECT u.Username, h.HistoryRecord, h.Activities " +
                    "FROM tblHistory h JOIN tblAccounts u ON h.AccountId = u.Id " +
                    "ORDER BY h.Activities DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grdHistory.DataSource = dt;
            }
            if (grdHistory.Columns.Contains("Activities"))
            {
                grdHistory.Columns["Activities"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            }
        }


        private void History_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }
    }
}
