using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabybondPOSInventorySystem
{
    public partial class InventoryLog: Form
    {
        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        
        public InventoryLog()
        {
            InitializeComponent();
            LoadProductsToGrid();
            
        }
       
       

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void Releasecapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void LoadProductsToGrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = @"
                                SELECT p.ProductId, p.ProductName, p.Description, ISNULL(l.quantity, 0) AS Quantity
                                FROM tblProduct p
                                LEFT JOIN tblQuantity l ON p.ProductId = l.QuantityId";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                

                foreach (DataRow row in dt.Rows)
                {
                    row["quantity"] = 0;         
                    
                }
                grdProdList.DataSource = dt;

               
                grdProdList.Columns["ProductName"].ReadOnly = true;
                grdProdList.Columns["ProductId"].Visible = false;

                grdProdList.Columns["ProductName"].HeaderText = "Product";
                grdProdList.Columns["Description"].HeaderText = "Description";
                grdProdList.Columns["Quantity"].HeaderText = "Add Quantity";


                grdProdList.Columns["Quantity"].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                grdProdList.EditingControlShowing += grdProdList_EditingControlShowing;
            }
        }
        private void NumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void grdProdList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (grdProdList.CurrentCell.ColumnIndex == 1)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(NumbersOnly_KeyPress);
                    tb.KeyPress += new KeyPressEventHandler(NumbersOnly_KeyPress);
                }
            }
        }
        
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True"))
            {
                con.Open();
                bool UpdatedAny = false;
                
                

                foreach (DataGridViewRow row in grdProdList.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                    string quantityStr = Convert.ToString(row.Cells["Quantity"].Value);

                    // Skip if empty
                    if (string.IsNullOrWhiteSpace(quantityStr)) continue;

                    
                    if (!int.TryParse(quantityStr, out int quantity) || quantity < 0 || quantity > 200)
                    {
                        MessageBox.Show("Invalid quantity. Please enter a number between 0 and 200.");
                        return;
                    }

                    // Update in ListQuantity
                    SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM tblQuantity WHERE ProductId = @ProductId", con);
                    cmdCheck.Parameters.AddWithValue("@ProductId", productId);
                    int exists = (int)cmdCheck.ExecuteScalar();

                    SqlCommand cmd;
                    if (exists > 0)
                    {
                        cmd = new SqlCommand("UPDATE tblQuantity SET Quantity = Quantity + @Quantity WHERE ProductId = @ProductId", con);
                    }
                    else
                    {
                        cmd = new SqlCommand("INSERT INTO tblQuantity (ProductId, Quantity) VALUES (@ProductId, @quantity)", con);
                    }

                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();      // recheck

                    ActivityLogger.LogActivity(Session.AccountId, $"added new stock");

                    UpdatedAny = true;
                }

                if (UpdatedAny)
                {
                    MessageBox.Show("Stocks updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is History history)
                        {
                            history.RefreshHistory();
                            break;
                        }
                    }
                    LoadProductsToGrid();
                }
                con.Close();

               
            }


           
            this.Close();


        }

        public static class Session
        {
            public static int AccountId { get; set; }
            public static string Username { get; set; }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InventoryLog_MouseDown(object sender, MouseEventArgs e)
        {
            Releasecapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
