using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.IO;

namespace BabybondPOSInventorySystem
{
    public partial class POS: Form
    {
        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        public POS()
        {
            InitializeComponent();
            LoadPackage();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void Releasecapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnMinimize_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            btnRestore.Visible = false;
            btnMaximixe.Visible = true;
        }

        private void btnRestore_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestore.Visible = false;
            btnMaximixe.Visible = true;
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void btnMaximixe_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnRestore.Visible = true;
            btnMaximixe.Visible = false;
        }

        private void topBar_MouseDown(object sender, MouseEventArgs e)
        {
            Releasecapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public void pckgItem(int Id, string PackageName, string PackageStatus, Image image)
        {
            try
            {
                var pckgItem = new POSPackage()
                {
                    Id = Id,
                    PackageName = PackageName,
                    PackageImage = image,
                    PackageStatus = PackageStatus
                };
                //pckgItem.selectPckg += (sender, e) =>
                //{
                //    ShowDetails(Id);
                //};
                pckgPanel.Controls.Add(pckgItem);
                Console.WriteLine($"Adding Package: {Id} - {PackageName}");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error adding package item: {ex.Message}");
            }
        }
        public void LoadPackage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string selectData = "SELECT * FROM tblPackage ";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable tbl = new DataTable();
                        sda.Fill(tbl);
                        pckgPanel.Controls.Clear(); 

                        //MessageBox.Show($"Packages found: {tbl.Rows.Count}")
                        //    ;
                        foreach(DataRow row in tbl.Rows)
                        {
                            int Id = row["PackageId"] != DBNull.Value ? (int)row["PackageId"]: 0;
                            string PackageName = row["PackageName"] != DBNull.Value ? row["PackageName"].ToString() : " ";
                            string PackageStatus = row["PackageStatus"] != DBNull.Value ? row["PackageStatus"].ToString() : "0";
                            Image image = null;
                            if (row["PackageImage"] != DBNull.Value)
                            {
                                string imagePath = row["PackageImage"].ToString();
                                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                                {
                                    try
                                    {
                                        image = Image.FromFile(imagePath);
                                        Console.WriteLine($"Load Image from: {Id} - {imagePath}");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error loading image: {ex.Message}");
                                        image = null;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Image path is invalid: {image}");
                            }
                         pckgItem(Id, PackageName, PackageStatus, image);  
                        }
                    }
                        
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Error:{ex}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //private void ShowDetails(int PackageId)
        //{
        //    grdAccounts.Columns.Clear();
        //    grdAccounts.DataSource = null;

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        string query = @"
        //            SELECT 
        //                p.ProductName,
        //                p.Description,
        //                q.Quantity
        //            FROM 
        //                tblPackageItem pi
        //            JOIN 
        //                tblProduct p ON pi.ProductId = p.ProductId
        //            JOIN 
        //                tblQuantity q ON pi.QuantityId = q.QuantityId
        //            WHERE 
        //                pi.PackageId = @PackageId";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@PackageId", PackageId);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                DataTable table = new DataTable();
        //                table.Load(reader);
        //                grdAccounts.DataSource = table;
        //            }
        //        }
        //    }
        //}

        private void UpdateInventory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Loop through the grid and update the inventory
                foreach (DataGridViewRow row in grdAccounts.Rows)
                {
                    if (row.Cells["Quantity"].Value != null)
                    {
                        int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                        string updateQuery = "UPDATE tblQuantity SET Quantity = Quantity - @Quantity WHERE ProductId = @ProductId";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@ProductId", productId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            MessageBox.Show("Inventory updated successfully.");
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            UpdateInventory();
        }
    }
}