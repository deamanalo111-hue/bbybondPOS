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
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace BabybondPOSInventorySystem
{
    public partial class Stocks: Form
    {
        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        public Stocks()
        {
            InitializeComponent();
            OpenInPanel();
        }

        public void LoadProductsUser()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                                 SELECT 
                                   p.ProductId, 
                                   p.ProductName, 
                                   p.Description, 
                                   ISNULL(l.quantity, 0) AS total_quantity
                                 FROM tblProduct p
                                 LEFT JOIN tblQuantity l ON p.ProductId = l.QuantityId";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Open();
                grdProdList.DataSource = dt;


                grdProdList.Columns["ProductName"].ReadOnly = true;
                grdProdList.Columns["ProductId"].Visible = true;


                grdProdList.Columns["ProductName"].HeaderText = "ProductName";
                grdProdList.Columns["Description"].HeaderText = "Description";
                grdProdList.Columns["total_quantity"].HeaderText = "total_quantity";


                grdProdList.Columns["total_quantity"].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            }
        }
        public void LoadProductsAdmin()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                                 SELECT 
                                   p.ProductId, 
                                   p.ProductName, 
                                   p.Description, 
                                   ISNULL(l.quantity, 0) AS total_quantity
                                 FROM tblProduct p
                                 LEFT JOIN tblQuantity l ON p.ProductId = l.QuantityId"; // get the infos
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Open();
                grdProdList.DataSource = dt;


                grdProdList.Columns["ProductName"].ReadOnly = true;
                grdProdList.Columns["ProductId"].Visible = true;

                grdProdList.Columns["ProductName"].HeaderText = "Product";
                grdProdList.Columns["Description"].HeaderText = "Description";
                grdProdList.Columns["total_quantity"].HeaderText = "total_quantity";


            }
        }
        private void OpenInPanel()
        {
            if (UserLog.type == "A")
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnAdd.Visible = true;
                btnInventory.Visible = false;
                LoadProductsAdmin();
            }
            else if (UserLog.type == "E")
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnAdd.Visible = false;
                btnInventory.Visible = true;
                LoadProductsUser();
            }
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            prodDetails.Visible = true;
            txtEDescription.Visible = true;
            txtEProdName.Visible = true;
            btnESave.Visible = true;
            txtId.Visible = true;
            prodNo.Visible = true;
            txtAProdName.Visible = false;
            txtADescription.Visible = false;
            btnASave.Visible = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            prodDetails.Visible = true;
            txtEDescription.Visible = false;
            txtEProdName.Visible = false;
            btnESave.Visible = false;
            txtId.Visible = false;
            prodNo.Visible = false;
            txtAProdName.Visible = true;
            txtADescription.Visible = true;
            btnASave.Visible = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdProdList.SelectedRows.Count > 0)
            {
                string prodName = grdProdList.SelectedRows[0].Cells["ProductName"].Value.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tblProduct WHERE ProductName=@ProductName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductName", prodName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted successfully.");
                        LoadProductsAdmin();                    }
                    else
                    {
                        MessageBox.Show("Product not found.");
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SearchProductName(string name)
        {
            string query = "SELECT ProductId, ProductName, Description FROM tblProduct WHERE ProductName LIKE @Name";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
                    cmd.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
                    DataTable dt = new DataTable();
                    cmd.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        grdProdList.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No Product found with that name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grdProdList.DataSource = null;
                        LoadProductsUser();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearch.Text;
            if (string.IsNullOrEmpty(searchName))
            {
                LoadProductsUser();  // show the products in table 
                MessageBox.Show("Enter product name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SearchProductName(searchName);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            InventoryLog ilf = new InventoryLog();
            ilf.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            prodDetails.Visible = false;
        }

        private void btnASave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkProduct = "SELECT COUNT(*) FROM tblProduct WHERE Description=@Description";
                    using (SqlCommand cmdCheck = new SqlCommand(checkProduct, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@Description", txtADescription.Text); // desc dapat ang i check
                        int prodExists = (int)cmdCheck.ExecuteScalar();
                        if (prodExists == 0)
                        {
                            string insertQuery = "INSERT INTO tblProduct (ProductName, Description) VALUES (@ProductName, @Description)";
                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@ProductName", txtAProdName.Text);
                                cmdInsert.Parameters.AddWithValue("@Description", txtADescription.Text);
                                cmdInsert.ExecuteNonQuery();
                            }
                            MessageBox.Show("Product has been successfully added!");
                            LoadProductsAdmin();
                            prodDetails.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Product already exists.");
                            txtEProdName.Focus();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during the process of adding a product: " + ex.Message);
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtAProdName.Text))
            {
                MessageBox.Show("Product name cannot be empty.");
                txtAProdName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtADescription.Text))
            {
                MessageBox.Show("Product description cannot be empty.");
                txtADescription.Focus();
                return false;
            }
            if (txtADescription.Text.Length > 15)
            {
                MessageBox.Show("Description must not exceed 15 characters.");
                txtADescription.Focus();
                return false;
            }
            if (txtAProdName.Text.Length > 15)
            {
                MessageBox.Show("Product name must not exceed 15 characters.");
                txtADescription.Focus();
                return false;
            }
           

            return true;
        }

        private void grdProdList_SelectionChanged(object sender, EventArgs e)
        {
            if (grdProdList.SelectedRows.Count > 0)
            {
                var selectedRow = grdProdList.SelectedRows[0];
                txtId.Text = selectedRow.Cells["ProductId"].Value?.ToString();
                txtEProdName.Text = selectedRow.Cells["ProductName"].Value?.ToString();
                txtEDescription.Text = selectedRow.Cells["Description"].Value?.ToString();
            }
        }
        private void btnESave_Click(object sender, EventArgs e)
        {
            if (grdProdList.SelectedRows.Count > 0)
            {
                string eprodName = grdProdList.SelectedRows[0].Cells["ProductId"].Value.ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE tblProduct SET ProductName=@ProductName, Description=@Description WHERE ProductId=@ProductId";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@ProductId", txtId.Text);
                        updateCmd.Parameters.AddWithValue("@ProductName", txtEProdName.Text);
                        updateCmd.Parameters.AddWithValue("@Description", txtEDescription.Text);
                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Update successful.");
                            LoadProductsAdmin();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.");
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to edit.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
