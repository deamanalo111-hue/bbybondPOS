using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BabybondPOSInventorySystem
{
    public partial class Packages : Form
    {
        private string conString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";

        public Packages()
        {
            InitializeComponent();
            LoadPackageList();
            LoadPackage();
            LoadProducts();
        }
        private void LoadPackageList()
        {
            if (!grdPckgList.Columns.Contains("Select"))
            {
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Select";
                chk.Name = "Select";
                chk.ReadOnly = false;
                grdProdList.Columns.Insert(0, chk);
            }
        }
        private void LoadProducts(int? packageId = null)
        {
            string query = @"SELECT pr.ProductId, pr.ProductName, pr.Description
                     FROM tblProduct pr
                     LEFT JOIN tblQuantity q ON pr.ProductId = q.ProductId";

            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Temporarily remove checkbox column if it exists
                if (grdProdList.Columns.Contains("Select"))
                {
                    grdProdList.Columns.Remove("Select");
                }

                grdProdList.DataSource = dt;

                // Re-add checkbox column
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
                {
                    HeaderText = "Select",
                    Name = "Select",
                    ReadOnly = false
                };
                grdProdList.Columns.Insert(0, chk);

                // Make the grid editable (specifically checkbox column)
                grdProdList.ReadOnly = false;

                foreach (DataGridViewColumn col in grdProdList.Columns)
                {
                    col.ReadOnly = (col.Name != "Select"); // Only "Select" should be editable
                }

                // If packageId is specified, mark selected products linked to package
                if (packageId.HasValue)
                {
                    try
                    {
                        HashSet<int> selectedProductIds = new HashSet<int>();
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT ProductId " +
                            "FROM PackageItem WHERE PackageId=@PackageId", conn))
                        {
                            cmd.Parameters.AddWithValue("@PackageId", packageId.Value);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    selectedProductIds.Add(reader.GetInt32(0));
                                }
                            }
                        }

                        foreach (DataGridViewRow row in grdProdList.Rows)
                        {
                            if (row.Cells["ProductId"].Value != null)
                            {
                                int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                                row.Cells["Select"].Value = selectedProductIds.Contains(productId);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading selected products: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }
                }
            }
        }

        private void LoadPackage()
        {
            string query = "SELECT PackageId, PackageName, PackageImage, PackageStatus FROM tblPackage";

            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (grdPckgList.Columns.Contains("ImagePreview"))
                {
                    grdPckgList.Columns.Remove("ImagePreview");
                }

                grdPckgList.DataSource = dt;
            }
        }
        private void LoadIncludedProduct()
        {
            // Ensure the grid is enabled and editable
            grdInProd.Enabled = true;
            grdInProd.ReadOnly = false;
            grdInProd.EditMode = DataGridViewEditMode.EditOnEnter;

            // Clear any existing data
            grdInProd.DataSource = null;
            grdInProd.Columns.Clear();
            grdInProd.Rows.Clear();

            // Add columns
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                HeaderText = "Product Id",
                ReadOnly = true
            };
            grdInProd.Columns.Add(colId);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                ReadOnly = true
            };
            grdInProd.Columns.Add(colName);

            DataGridViewTextBoxColumn colQty = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                ReadOnly = false // Quantity must be editable
            };
            grdInProd.Columns.Add(colQty);

            // Populate with selected products
            foreach (DataGridViewRow row in grdProdList.Rows)
            {
                bool isSelected = row.Cells["Select"].Value != null && 
                    Convert.ToBoolean(row.Cells["Select"].Value);
                if (isSelected)
                {
                    int prodId = Convert.ToInt32(row.Cells["ProductId"].Value);
                    string prodName = Convert.ToString(row.Cells["ProductName"].Value);
                    grdInProd.Rows.Add(prodId, prodName, 0); // Default quantity = 0
                }
            }

            if (grdInProd.Rows.Count == 0)
            {
                MessageBox.Show("Please select at least one product.", "Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadIncludedProduct();
        }

        private void grdPckgList_SelectionChanged(object sender, EventArgs e)
        {
            if(grdPckgList.SelectedRows.Count > 0)
            {
                int pckgId = Convert.ToInt32(grdPckgList.SelectedRows[0].Cells["PackageId"].Value);
                LoadProd(pckgId);
            }
        }
        private void LoadProd(int pckgId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string query = @"
            SELECT 
                p.ProductId, 
                p.ProductName, 
                pi.Quantity
            FROM tblPackageItem pi
            JOIN tblProduct p ON pi.ProductId = p.ProductId
            WHERE pi.PackageId = @PackageId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PackageId", pckgId);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                // Reset the DataGridView before binding
                grdInProd.DataSource = null;
                grdInProd.Columns.Clear();
                grdInProd.Rows.Clear();

                // Bind new data
                grdInProd.DataSource = dt;

                // Make sure only Quantity is editable
                foreach (DataGridViewColumn col in grdInProd.Columns)
                {
                    if (col.Name == "Quantity")
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }

                grdInProd.EditMode = DataGridViewEditMode.EditOnEnter;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatePackageInput(out string validationError))
                {
                    MessageBox.Show(validationError, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] imageData = null;
                if (pckgPic.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pckgPic.Image.Save(ms, pckgPic.Image.RawFormat);
                        imageData = ms.ToArray();
                    }
                }

                string packageStatus = cmbStatus.Text;
                int newPackageId = 0;

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd;

                    if (grdPckgList.SelectedRows.Count > 0)
                    {
                        int packageId = Convert.ToInt32(grdPckgList.SelectedRows[0].Cells["PackageId"].Value);
                        string updateQuery = @"UPDATE tblPackage
                                       SET PackageName = @PackageName, PackageImage = @PackageImage, PackageStatus = @PackageStatus
                                       WHERE PackageId = @PackageId";
                        cmd = new SqlCommand(updateQuery, con);
                        cmd.Parameters.AddWithValue("@PackageId", packageId);
                        newPackageId = packageId;
                    }
                    else
                    {
                        string insertQuery = @"INSERT INTO tblPackage (PackageName, PackageImage, PackageStatus)
                                       OUTPUT INSERTED.PackageId
                                       VALUES (@PackageName, @PackageImage, @PackageStatus)";
                        cmd = new SqlCommand(insertQuery, con);
                    }

                    cmd.Parameters.AddWithValue("@PackageName", txtPckg.Text.Trim());
                    cmd.Parameters.AddWithValue("@PackageStatus", packageStatus);
                    cmd.Parameters.AddWithValue("@PackageImage", selectedImagePath ?? (object)DBNull.Value);

                    if (newPackageId == 0)
                    {
                        newPackageId = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Insert Package Items
                    foreach (DataGridViewRow row in grdInProd.Rows)
                    {
                        if (row.Cells["ProductId"].Value != null && row.Cells["Quantity"].Value != null)
                        {
                            int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                            int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                            SqlCommand itemCmd = new SqlCommand(
                                "INSERT INTO tblPackageItem (PackageId, ProductId, Quantity) VALUES (@PackageId, @ProductId, @Quantity)", con);
                            itemCmd.Parameters.AddWithValue("@PackageId", newPackageId);
                            itemCmd.Parameters.AddWithValue("@ProductId", productId);
                            itemCmd.Parameters.AddWithValue("@Quantity", quantity);
                            itemCmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Package saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPackage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving package: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdPckgList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a package to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation
            if (!ValidatePackageInput(out string validationError))
            {
                MessageBox.Show(validationError, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int pckgId = Convert.ToInt32(grdPckgList.SelectedRows[0].Cells["PackageId"].Value);

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Update tblPackage
                    string updatePckgQuery = @"UPDATE tblPackage
                                       SET PackageName = @PackageName, 
                                           PackageImage = @PackageImage, 
                                           PackageStatus = @PackageStatus
                                       WHERE PackageId = @PackageId";

                    SqlCommand updatePckgCmd = new SqlCommand(updatePckgQuery, con);
                    updatePckgCmd.Parameters.AddWithValue("@PackageId", pckgId);
                    updatePckgCmd.Parameters.AddWithValue("@PackageName", txtPckg.Text.Trim());
                    updatePckgCmd.Parameters.AddWithValue("@PackageStatus", cmbStatus.Text);
                    updatePckgCmd.Parameters.AddWithValue("@PackageImage", selectedImagePath ?? (object)DBNull.Value);
                    updatePckgCmd.ExecuteNonQuery();

                    // Clear existing products for this package
                    string deleteQuery = "DELETE FROM tblPackageItem WHERE PackageId = @PackageId";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                    deleteCmd.Parameters.AddWithValue("@PackageId", pckgId);
                    deleteCmd.ExecuteNonQuery();

                    // Insert updated product list
                    foreach (DataGridViewRow row in grdInProd.Rows)
                    {
                        if (row.Cells["ProductId"].Value == null || row.Cells["Quantity"].Value == null)
                            continue;

                        int prodId = Convert.ToInt32(row.Cells["ProductId"].Value);
                        int quantity;

                        var qtyVal = row.Cells["Quantity"].Value;
                        if (qtyVal == null || !int.TryParse(qtyVal.ToString(), out quantity) || quantity < 0)
                        {
                            MessageBox.Show($"Invalid quantity for Product ID {prodId}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        string insertQuery = "INSERT INTO tblPackageItem (PackageId, ProductId, Quantity) VALUES (@PackageId, @ProductId, @Quantity)";
                        SqlCommand cmd = new SqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@PackageId", pckgId);
                        cmd.Parameters.AddWithValue("@ProductId", prodId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Package updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProd(pckgId);
                LoadPackage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating package: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            grdPckgList.ClearSelection();
            grdInProd.DataSource = null;
            grdInProd.Rows.Clear();
            grdInProd.Columns.Clear();


            foreach (DataGridViewRow row in grdProdList.Rows)
            {
                if (row.Cells["Select"] != null)
                {
                    row.Cells["Select"].Value = false;
                }
            }

            txtPckg.Text = "";
            pckgPic.Image = null;
            txtId = null;

            MessageBox.Show("Ready to create a new package.", "New Package", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private string selectedImagePath = null;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedImagePath = ofd.FileName;
                        pckgPic.Image = Image.FromFile(selectedImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to load image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdPckgList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a package to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int packageId = Convert.ToInt32(grdPckgList.SelectedRows[0].Cells["PackageId"].Value);

            var confirm = MessageBox.Show("Are you sure you want to delete the selected package? This will also delete all included products.",
                                          "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    // Delete related package items first (foreign key constraints)
                    string deleteItemsQuery = "DELETE FROM tblPackageItem WHERE PackageId = @PackageId";
                    SqlCommand cmdItems = new SqlCommand(deleteItemsQuery, con);
                    cmdItems.Parameters.AddWithValue("@PackageId", packageId);
                    cmdItems.ExecuteNonQuery();

                    // Delete the package itself
                    string deletePackageQuery = "DELETE FROM tblPackage WHERE PackageId = @PackageId";
                    SqlCommand cmdPackage = new SqlCommand(deletePackageQuery, con);
                    cmdPackage.Parameters.AddWithValue("@PackageId", packageId);
                    cmdPackage.ExecuteNonQuery();
                }

                MessageBox.Show("Package deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPackage();

                // Clear grids and inputs after delete
                grdInProd.DataSource = null;
                grdInProd.Rows.Clear();
                grdInProd.Columns.Clear();

                txtPckg.Clear();
                pckgPic.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting package: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadPackage(); // load all if empty search
                return;
            }

            string query = @"SELECT PackageId, PackageName, PackageImage, PackageStatus 
                     FROM tblPackage 
                     WHERE PackageName LIKE @SearchTerm";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                grdPckgList.DataSource = dt;
            }
        }

        private bool ValidatePackageInput(out string errorMsg)
        {
            errorMsg = "";

            if (string.IsNullOrWhiteSpace(txtPckg.Text))
            {
                errorMsg = "Package name is required.";
                return false;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                errorMsg = "Please select a package status.";
                return false;
            }

            bool atLeastOneSelected = false;
            foreach (DataGridViewRow row in grdInProd.Rows)
            {
                if (row.Cells["ProductId"].Value != null)
                {
                    atLeastOneSelected = true;

                    var qtyVal = row.Cells["Quantity"].Value;
                    if (qtyVal == null || !int.TryParse(qtyVal.ToString(), out int qty) || qty < 0)
                    {
                        errorMsg = $"Invalid quantity for Product ID {row.Cells["ProductId"].Value}.";
                        return false;
                    }
                }
            }

            if (!atLeastOneSelected)
            {
                errorMsg = "At least one product must be selected with a valid quantity.";
                return false;
            }

            return true;
        }
    }
}
