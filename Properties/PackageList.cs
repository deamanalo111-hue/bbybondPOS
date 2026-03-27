//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace BabybondPOSInventorySystem
//{
//    class PackageList
//    {
//        //public int PackageId { get; set; }
//        //public string PackageName { get; set; }
//        //public string PackageImageRelativePath { get; set; } // Store relative path
//        //public string PackageStatus { get; set; }
//        //public List<PackageItem> Items { get; set; }

//        //public Package()
//        //{
//        //    Items = new List<PackageItem>();
//        //}
//    }
//}


//        private void LoadPromo()
//        {
//            string query = "SELECT PackageId, PackageName, PackageImage, PackageStatus FROM tblPackage";
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
//                DataTable dt = new DataTable();
//                conn.Open();
//                adapter.Fill(dt);
//                grdPckgList.DataSource = dt;
//            }
//        }

//        private void LoadProducts(int? packageId = null)
//        {
//            // Now joining tblProduct with tblQuantity to include quantity info
//            string query = @"SELECT pr.ProductId, pr.ProductName, pr.Description, q.Quantity
//                             FROM tblProduct pr
//                             LEFT JOIN tblQuantity q ON pr.ProductId = q.ProductId";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
//                DataTable dt = new DataTable();
//                adapter.Fill(dt);
//                grdProdList.DataSource = dt;

//                // Insert checkbox column if not exists
//                if (!grdProdList.Columns.Contains("Select"))
//                {
//                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
//                    {
//                        HeaderText = "Select",
//                        Name = "Select",
//                        ReadOnly = false
//                    };
//                    grdProdList.Columns.Insert(0, chk);
//                    grdProdList.ReadOnly = false;
//                }

//                // If packageId is specified, mark selected products linked to package
//                if (packageId.HasValue)
//                {
//                    try
//                    {
//                        HashSet<int> selectedProductIds = new HashSet<int>();
//                        conn.Open();
//                        using (SqlCommand cmd = new SqlCommand("SELECT ProductId FROM PackageItem WHERE PackageId=@PackageId", conn))
//                        {
//                            cmd.Parameters.AddWithValue("@PackageId", packageId.Value);
//                            using (SqlDataReader reader = cmd.ExecuteReader())
//                            {
//                                while (reader.Read())
//                                {
//                                    selectedProductIds.Add(reader.GetInt32(0));
//                                }
//                            }
//                        }

//                        foreach (DataGridViewRow row in grdProdList.Rows)
//                        {
//                            if (row.Cells["ProductId"].Value != null)
//                            {
//                                int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
//                                row.Cells["Select"].Value = selectedProductIds.Contains(productId);
//                            }
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show("Error loading selected products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }
//                    finally
//                    {
//                        if (conn.State == System.Data.ConnectionState.Open)
//                            conn.Close();
//                    }
//                }
//            }
//        }

//        private void SearchPckgName(string name)
//        {
//            string query = "SELECT PackageId, PackageName FROM tblPackage WHERE PackageName LIKE @Name";
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
//                    cmd.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
//                    DataTable dt = new DataTable();
//                    cmd.Fill(dt);

//                    if (dt.Rows.Count > 0)
//                    {
//                        grdPckgList.DataSource = dt;
//                    }
//                    else
//                    {
//                        MessageBox.Show("No promo found with that name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        LoadPromo();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            string searchName = txtSearch.Text.Trim();
//            if (string.IsNullOrEmpty(searchName))
//            {
//                LoadPromo();
//                MessageBox.Show("Enter package name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }
//            SearchPckgName(searchName);
//        }

//        private void btnBrowse_Click(object sender, EventArgs e)
//        {
//            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
//            if (openFile.ShowDialog() == DialogResult.OK)
//            {
//                pckgPic.ImageLocation = openFile.FileName;
//                pckgPic.SizeMode = PictureBoxSizeMode.StretchImage;
//            }
//        }

//        private void grdPckgList_SelectionChanged(object sender, EventArgs e)
//        {
//            if (grdPckgList.SelectedRows.Count > 0)
//            {
//                var selectedRow = grdPckgList.SelectedRows[0];
//                txtId.Text = selectedRow.Cells["PackageId"].Value?.ToString();
//                txtPckg.Text = selectedRow.Cells["PackageName"].Value?.ToString();
//                cmbStatus.SelectedItem = selectedRow.Cells["PackageStatus"].Value?.ToString();

//                string imagePath = selectedRow.Cells["PackageImage"].Value?.ToString();
//                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
//                {
//                    pckgPic.ImageLocation = imagePath;
//                }

//                // Load assigned products with quantities for selected package
//                int packageId = Convert.ToInt32(txtId.Text);
//                LoadAssignedProducts(packageId);

//                // Update product selection checkboxes accordingly
//                LoadProducts(packageId);
//            }
//        }

//        private void btnNew_Click(object sender, EventArgs e)
//        {
//            panelNew.Visible = true;
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (grdPckgList.SelectedRows.Count > 0)
//            {
//                string packageId = grdPckgList.SelectedRows[0].Cells["PackageId"].Value.ToString();
//                using (SqlConnection conn = new SqlConnection(connectionString))
//                {
//                    conn.Open();
//                    string deletePackageItems = "DELETE FROM PackageItem WHERE PackageId=@PackageId";
//                    using (SqlCommand cmdDelItems = new SqlCommand(deletePackageItems, conn))
//                    {
//                        cmdDelItems.Parameters.AddWithValue("@PackageId", packageId);
//                        cmdDelItems.ExecuteNonQuery();
//                    }

//                    string query = "DELETE FROM tblPackage WHERE PackageId=@PackageId";
//                    SqlCommand cmd = new SqlCommand(query, conn);
//                    cmd.Parameters.AddWithValue("@PackageId", packageId);

//                    int rowsAffected = cmd.ExecuteNonQuery();
//                    if (rowsAffected > 0)
//                    {
//                        MessageBox.Show("Package deleted successfully.");
//                        LoadPromo();
//                    }
//                    else
//                    {
//                        MessageBox.Show("Package not found.");
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please select a package to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void btnClear_Click(object sender, EventArgs e)
//        {
//            txtPckg.Clear();
//            pckgPic.Image = null;
//            cmbStatus.SelectedItem = null;
//        }

//        private void btnUpdate_Click(object sender, EventArgs e)
//        {
//            if (!ValidateInputs())
//                return;

//            if (grdPckgList.SelectedRows.Count > 0)
//            {
//                string packageId = grdPckgList.SelectedRows[0].Cells["PackageId"].Value.ToString();
//                using (SqlConnection conn = new SqlConnection(connectionString))
//                {
//                    conn.Open();
//                    string updateQuery = "UPDATE tblPackage SET PackageName=@PackageName, PackageImage=@PackageImage, PackageStatus=@PackageStatus WHERE PackageId=@PackageId";

//                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
//                    string fileName = Path.GetFileName(pckgPic.ImageLocation);
//                    string relativePath = Path.Combine("package_images", fileName);
//                    string path = Path.Combine(baseDirectory, relativePath);

//                    string directory = Path.GetDirectoryName(path);
//                    if (!Directory.Exists(directory))
//                    {
//                        Directory.CreateDirectory(directory);
//                    }

//                    File.Copy(pckgPic.ImageLocation, path, true);



//                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
//                    {
//                        updateCmd.Parameters.AddWithValue("@PackageId", packageId);
//                        updateCmd.Parameters.AddWithValue("@PackageName", txtPckg.Text.Trim());
//                        updateCmd.Parameters.AddWithValue("@PackageImage", path);
//                        updateCmd.Parameters.AddWithValue("@PackageStatus", cmbStatus.SelectedItem);
//                        int rowsAffected = updateCmd.ExecuteNonQuery();
//                        if (rowsAffected > 0)
//                        {
//                            MessageBox.Show("Update successful.");
//                            LoadPromo();
//                            LoadProducts(Convert.ToInt32(packageId));
//                            LoadAssignedProducts(Convert.ToInt32(packageId));
//                        }
//                        else
//                        {
//                            MessageBox.Show("No changes were made.");
//                            txtPckg.Focus();
//                        }
//                    }
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please select a package to edit.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void LoadAssignedProducts(int packageId)
//        {
//            grdInProd.Columns.Clear();
//            grdInProd.Columns.Add("ProductId", "Product ID");
//            grdInProd.Columns["ProductId"].Visible = false;
//            grdInProd.Columns.Add("ProductName", "Product Name");
//            grdInProd.Columns.Add("Quantity", "Quantity");
//            grdInProd.Columns["Quantity"].ValueType = typeof(int);
//            //using (SqlConnection con = new SqlConnection(connectionString))
//            //{
//            //    con.Open();
//            //    string query = @"
//            //        SELECT pr.ProductId, pr.ProductName, q.Quantity
//            //        FROM tblPackageItem pi
//            //        JOIN tblProduct pr ON pi.PackageId = pr.ProductId
//            //        LEFT JOIN tblQuantity q ON pr.ProductId = q.QuantityId
//            //        WHERE pi.PackageId = @PackageId";

//            //    using (SqlCommand cmd = new SqlCommand(query, con))
//            //    {
//            //        cmd.Parameters.AddWithValue("@PackageId", packageId);

//            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
//            //        DataTable dt = new DataTable();
//            //        da.Fill(dt);
//            //        grdInProd.DataSource = dt; // included (assigned) products table
//            //    }
//            //}
//        }

//        private void grdProdList_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0) // To avoid header row issues
//            {
//                if (grdPckgList.SelectedRows.Count == 0)
//                {
//                    MessageBox.Show("Please select a package first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                int packageId = Convert.ToInt32(grdPckgList.SelectedRows[0].Cells["PackageId"].Value);
//                int productId = Convert.ToInt32(grdProdList.Rows[e.RowIndex].Cells["ProductId"].Value);

//                using (SqlConnection conn = new SqlConnection(connectionString))
//                {
//                    conn.Open();

//                    // Check if already assigned
//                    string checkQuery = "SELECT COUNT(*) FROM PackageItem WHERE PackageId = @pkg AND ProductId = @prd";
//                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
//                    {
//                        checkCmd.Parameters.AddWithValue("@pkg", packageId);
//                        checkCmd.Parameters.AddWithValue("@prd", productId);
//                        int exists = (int)checkCmd.ExecuteScalar();

//                        if (exists > 0)
//                        {
//                            MessageBox.Show("This product is already assigned to this package.");
//                            return;
//                        }
//                    }

//                    // Insert
//                    string insertQuery = "INSERT INTO PackageItem (PackageId, ProductId) VALUES (@pkg, @prd)";
//                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
//                    {
//                        insertCmd.Parameters.AddWithValue("@pkg", packageId);
//                        insertCmd.Parameters.AddWithValue("@prd", productId);
//                        insertCmd.ExecuteNonQuery();
//                    }

//                    MessageBox.Show("Product added to package successfully.");
//                    LoadAssignedProducts(packageId);
//                }
//            }
//        }

//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            a



//            //grdInProd.Columns["Quantity"].ReadOnly = false;
//            //int selectedProductId = (int)grdProdList.SelectedRows[0].Cells["ProductId"].Value;
//            //string ProductName = grdProdList.SelectedRows[0].Cells["ProductName"].Value.ToString();

//            //DataGridViewRow row = new DataGridViewRow();
//            //row.Cells.Add(new DataGridViewTextBoxCell { Value = selectedProductId });
//            //row.Cells.Add(new DataGridViewTextBoxCell { Value = ProductName });
//            //row.Cells.Add(new DataGridViewTextBoxCell { Value = 0 });
//            //grdInProd.Rows.Add(row);

//            grdInProd.DataSource = null;

//            // Ensure columns exist (only once)
//            if (grdInProd.Columns.Count == 0)
//            {
//                grdInProd.Columns.Add("ProductId", "Product ID");
//                grdInProd.Columns.Add("ProductName", "Product Name");
//                grdInProd.Columns.Add("Quantity", "Quantity");
//            }

//            grdInProd.Rows.Clear(); // now it's safe

//            foreach (DataGridViewRow row in grdProdList.Rows)
//            {
//                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);
//                if (isChecked)
//                {
//                    int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
//                    string productName = row.Cells["ProductName"].Value.ToString();
//                    int quantity = 0;
//                    if (row.Cells["Quantity"].Value != DBNull.Value && row.Cells["Quantity"].Value != null)
//                    {
//                        quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
//                    }

//                    grdInProd.Rows.Add(productId, productName, quantity);
//                }
//            }

//            if (grdInProd.Rows.Count == 0)
//            {
//                MessageBox.Show("Please select at least one product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (!NewValidateInputs())
//            {
//                return;
//            }

//            try
//            {
//                using (SqlConnection conn = new SqlConnection(connectionString))
//                {
//                    conn.Open();
//                    string checkUser = "SELECT COUNT(*) FROM tblPackage WHERE PackageName=@PackageName";
//                    using (SqlCommand cmdCheck = new SqlCommand(checkUser, conn))
//                    {
//                        cmdCheck.Parameters.AddWithValue("@PackageName", txtNPckg.Text);
//                        int pckgExists = (int)cmdCheck.ExecuteScalar();

//                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

//                        if (pckgExists == 0)
//                        {
//                            string insertQuery = "INSERT INTO tblPackage (PackageName, PackageImage, PackageStatus) " +
//                                "VALUES (@PackageName, @PackageImage, @PackageStatus)";
//                            string fileName = Path.GetFileName(pckgNImage.ImageLocation);
//                            string relativePath = Path.Combine("package_images", fileName);
//                            string path = Path.Combine(baseDirectory, relativePath);

//                            // Ensure directory exists
//                            string directory = Path.GetDirectoryName(path);
//                            if (!Directory.Exists(directory))
//                            {
//                                Directory.CreateDirectory(directory);
//                            }

//                            // Save the image
//                            File.Copy(pckgNImage.ImageLocation, path, true);
//                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
//                            {
//                                cmdInsert.Parameters.AddWithValue("@PackageName", txtNPckg.Text);
//                                cmdInsert.Parameters.AddWithValue("@PackageImage", path);
//                                cmdInsert.Parameters.AddWithValue("@PackageStatus", cmbNStatus.SelectedItem.ToString());
//                                cmdInsert.ExecuteNonQuery();
//                                int PackageId = Convert.ToInt32(new SqlCommand("SELECT SCOPE_IDENTITY()", conn).ExecuteScalar());
//                                foreach (DataGridViewRow row in grdInProd.Rows)
//                                {
//                                    if (row.Cells["ProductId"].Value != null && row.Cells["Quantity"].Value != null)
//                                    {
//                                        int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
//                                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

//                                        string insertQuantityQuery = "INSERT INTO tblQuantity (ProductID, Quantity) " +
//                                                                     "VALUES ( @ProductID, @Quantity)";
//                                        SqlCommand quantityCmd = new SqlCommand(insertQuantityQuery, conn);

//                                        quantityCmd.Parameters.AddWithValue("@ProductID", productId);
//                                        quantityCmd.Parameters.AddWithValue("@Quantity", quantity);

//                                        quantityCmd.ExecuteNonQuery();
//                                    }
//                                }
//                                MessageBox.Show("Package added successfully!");
//                                LoadPromo();
//                            }
//                        }
//                        else
//                        {
//                            MessageBox.Show("Package already exists.");
//                            txtNPckg.Focus();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error during the process of adding package: " + ex.Message);
//            }
//            panelNew.Visible = false;
//        }

//        private bool NewValidateInputs()
//        {
//            if (string.IsNullOrWhiteSpace(txtNPckg.Text))
//            {
//                MessageBox.Show("Package name cannot be empty.");
//                txtNPckg.Focus();
//                return false;
//            }
//            if (!Regex.IsMatch(txtNPckg.Text, @"^[a-zA-Z0-9]+$"))
//            {
//                MessageBox.Show("Package name must contain only letters (A-Z or a-z) and numbers (0-9).");
//                txtNPckg.Focus();
//                return false;
//            }
//            if (txtNPckg.Text.Length > 15)
//            {
//                MessageBox.Show("Package name must not exceed 15 characters.");
//                txtNPckg.Focus();
//                return false;
//            }
//            return true;
//        }

//        private bool ValidateInputs()
//        {
//            if (string.IsNullOrWhiteSpace(txtPckg.Text))
//            {
//                MessageBox.Show("Package name cannot be empty.");
//                txtPckg.Focus();
//                return false;
//            }
//            if (!Regex.IsMatch(txtPckg.Text, @"^[a-zA-Z0-9]+$"))
//            {
//                MessageBox.Show("Package name must contain only letters (A-Z or a-z) and numbers (0-9).");
//                txtPckg.Focus();
//                return false;
//            }
//            if (txtPckg.Text.Length > 15)
//            {
//                MessageBox.Show("Package name must not exceed 15 characters.");
//                txtPckg.Focus();
//                return false;
//            }
//            return true;
//        }

//        private void btnNBrowse_Click(object sender, EventArgs e)
//        {
//            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
//            if (openFile.ShowDialog() == DialogResult.OK)
//            {
//                pckgNImage.ImageLocation = openFile.FileName;
//                pckgNImage.SizeMode = PictureBoxSizeMode.StretchImage;
//            }
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            txtNPckg.Clear();
//            cmbNStatus.SelectedItem = null;
//            pckgNImage.Image = null;
//            panelNew.Visible = false;
//        }

//        private void label6_Click(object sender, EventArgs e)
//        {
//            // You may implement this event handler if needed.
//        }
//    }
//}






////using System;
////using System.CodeDom.Compiler;
////using System.Collections;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Data.SqlClient;
////using System.Drawing;
////using System.IO;
////using System.Linq;
////using System.Text;
////using System.Text.RegularExpressions;
////using System.Threading.Tasks;
////using System.Windows.Forms;

////namespace BabybondPOSInventorySystem
////{
////    public partial class Packages: Form
////    {
////        public Packages()
////        {
////            InitializeComponent();
////            LoadPromo();
////            LoadProducts();
////            grdPckgList.SelectionChanged += grdPckgList_SelectionChanged;
////            //LoadAssignedProducts();

////        }
////        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
////        private void LoadPromo()
////        {
////            string query = "SELECT PackageId, PackageName, PackageImage, PackageStatus FROM tblPackage";
////            using (SqlConnection conn = new SqlConnection(connectionString))
////            {
////                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
////                DataTable dt = new DataTable();
////                conn.Open();
////                adapter.Fill(dt);
////                grdPckgList.DataSource = dt;
////                conn.Close();


////            }

////        }
////        private void LoadProducts(int? packageId = null)
////        {
////            string query = "SELECT ProductId , ProductName, Description FROM tblProduct";
////            using (SqlConnection conn = new SqlConnection(connectionString))
////            {
////                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
////                DataTable dt = new DataTable();

////                adapter.Fill(dt);
////                grdProdList.DataSource = dt;
////                if (!grdProdList.Columns.Contains("Select"))
////                {
////                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
////                    chk.HeaderText = "Select";
////                    chk.Name = "Select";
////                    chk.ReadOnly = false;
////                    grdProdList.Columns.Insert(0, chk);
////                    grdProdList.ReadOnly = false;

////                }

////                if (packageId.HasValue)
////                {
////                    SqlCommand cmd = new SqlCommand("SELECT ProductId FROM PackageItem WHERE PackageId=@PackageId", conn);
////                    cmd.Parameters.AddWithValue("@PackageId", packageId);
////                    conn.Open();
////                    SqlDataReader sdr = cmd.ExecuteReader();
////                    HashSet<int> selectedProductIds = new HashSet<int>();
////                    while (sdr.Read())
////                        selectedProductIds.Add(sdr.GetInt32(0));
////                    conn.Close();

////                    foreach (DataGridViewRow row in grdProdList.Rows)
////                    {
////                        int ProductId = Convert.ToInt32(row.Cells["ProductId"].Value);
////                        row.Cells["SELECT"].Value = selectedProductIds.Contains(ProductId);
////                    }
////                }

////            }

////        }

////        private void SearchPckgName(string name)
////        {
////            string query = "SELECT PackageId, PackageName FROM tblPackage WHERE PackageName LIKE @Name";
////            using (SqlConnection conn = new SqlConnection(connectionString))
////            {
////                try
////                {
////                    SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
////                    cmd.SelectCommand.Parameters.AddWithValue("@Name", "%" + name + "%");
////                    DataTable dt = new DataTable();
////                    cmd.Fill(dt);

////                    if (dt.Rows.Count > 0)
////                    {
////                        grdPckgList.DataSource = dt;
////                    }
////                    else
////                    {
////                        MessageBox.Show("No promo found with that name.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                        grdPckgList.DataSource = null;
////                        LoadPromo();
////                    }
////                }
////                catch (Exception ex)
////                {
////                    MessageBox.Show("Error occurred while searching: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////                conn.Close();
////            }
////        }
////        private void btnSearch_Click(object sender, EventArgs e)
////        {
////            string searchName = txtSearch.Text;
////            if (string.IsNullOrEmpty(searchName))
////            {
////                LoadPromo();  // show the products in table 
////                MessageBox.Show("Enter product name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                return;
////            }
////            SearchPckgName(searchName);
////        }

////        private void btnBrowse_Click(object sender, EventArgs e)
////        {
////            openFile.Filter = "Image Files(|*.jpg;*.jpeg;*.png;";
////            if (openFile.ShowDialog() == DialogResult.OK)
////            {
////                pckgPic.ImageLocation = openFile.FileName;
////                pckgPic.SizeMode = PictureBoxSizeMode.StretchImage;
////            }
////        }

////        private void grdPckgList_SelectionChanged(object sender, EventArgs e)
////        {
////            if (grdPckgList.SelectedRows.Count > 0)
////            {
////                var selectedRow = grdPckgList.SelectedRows[0];
////                txtId.Text = selectedRow.Cells["PackageId"].Value?.ToString();
////                txtPckg.Text = selectedRow.Cells["PackageName"].Value?.ToString();
////                cmbStatus.SelectedItem = selectedRow.Cells["PackageStatus"].Value?.ToString();

////                string imagePath = selectedRow.Cells["PackageImage"].Value?.ToString();
////                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
////                {
////                    pckgPic.ImageLocation = imagePath;
////                }

////            }
////        }

////        private void btnNew_Click(object sender, EventArgs e)
////        {
////            panelNew.Visible = true;
////        }
////        private void btnDelete_Click(object sender, EventArgs e)
////        {
////            if (grdPckgList.SelectedRows.Count > 0)
////            {
////                string pckgName = grdPckgList.SelectedRows[0].Cells["PackageName"].Value.ToString();

////                using (SqlConnection conn = new SqlConnection(connectionString))
////                {
////                    conn.Open();
////                    string query = "DELETE FROM tblPackage WHERE PackageName=@PackageName";
////                    SqlCommand cmd = new SqlCommand(query, conn);
////                    cmd.Parameters.AddWithValue("@PackageName", pckgName);

////                    int rowsAffected = cmd.ExecuteNonQuery();
////                    if (rowsAffected > 0)
////                    {
////                        MessageBox.Show("Packages deleted successfully.");
////                        LoadPromo();
////                    }
////                    else
////                    {
////                        MessageBox.Show("Package not found.");
////                    }
////                    conn.Close();
////                }
////            }
////            else
////            {
////                MessageBox.Show("Please select a package to delete.", "Validation",
////                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
////            }
////        }

////        private void btnClear_Click(object sender, EventArgs e)
////        {
////            txtPckg.Clear();
////        }

////        private void btnUpdate_Click(object sender, EventArgs e)
////        {
////            if (grdPckgList.SelectedRows.Count > 0)
////            {
////                string pckgName = grdPckgList.SelectedRows[0].Cells["PackageId"].Value.ToString();
////                using (SqlConnection conn = new SqlConnection(connectionString))
////                {
////                    conn.Open();
////                    string updateQuery = "UPDATE tblPackage SET PackageName=@PackageName, PackageImage=@PackageImage, PackageStatus=@PackageStatus WHERE PackageId=@PackageId";


////                    // Save the image to a directory
////                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
////                   // string relativePath = Path.Combine("package_images", pckgPic.Image + ".jpg");
////                    string fileName = Path.GetFileName(pckgPic.ImageLocation);
////                    string relativePath = Path.Combine("package_images", fileName);

////                    string path = Path.Combine(baseDirectory, relativePath);

////                    // Ensure directory exists
////                    string directory = Path.GetDirectoryName(path);
////                    if (!Directory.Exists(directory))
////                    {
////                        Directory.CreateDirectory(directory);
////                    }

////                    // Save the image
////                    File.Copy(pckgPic.ImageLocation, path, true);
////                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
////                    {
////                        updateCmd.Parameters.AddWithValue("@PackageId", txtId.Text.Trim());
////                        updateCmd.Parameters.AddWithValue("@PackageName", txtPckg.Text.Trim());
////                        updateCmd.Parameters.AddWithValue("@PackageImage", path);
////                        updateCmd.Parameters.AddWithValue("@PackageStatus", cmbStatus.SelectedItem);
////                        int rowsAffected = updateCmd.ExecuteNonQuery();
////                        if (rowsAffected > 0)
////                        {
////                            MessageBox.Show("Update successful.");
////                            LoadPromo();
////                        }
////                        else
////                        {
////                            MessageBox.Show("No changes were made.");
////                            txtPckg.Focus();
////                        }
////                    }
////                    conn.Close();
////                }
////            }
////            else
////            {
////                MessageBox.Show("Please select a package to edit.", "Validation",
////                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
////            }
////        }

////        private void LoadAssignedProducts(int packageId)
////        {
////            using (SqlConnection con = new SqlConnection(connectionString))
////            {
////                con.Open();

////                string query = @"
////                    SELECT p.ProdctId AS ProductID, p.Product
////                    FROM CombinePackageItem cpi
////                    JOIN Products p ON cpi.ProductID = p.Id
////                    WHERE cpi.PackageID = @pkgId";

////                SqlCommand cmd = new SqlCommand(query, con);
////                cmd.Parameters.AddWithValue("@pkgId", packageId);

////                SqlDataAdapter da = new SqlDataAdapter(cmd);
////                DataTable dt = new DataTable();
////                da.Fill(dt);
////                grdInProd.DataSource = dt; // included (assigned) products table
////            }
////        }
////        private void grdProdList_CellClick(object sender, DataGridViewCellEventArgs e)
////        {
////            if (e.RowIndex >= 0) // To avoid header row issues
////            {
////                if (grdPckgList.SelectedRows.Count == 0)
////                {
////                    MessageBox.Show("Please select a package first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                    return;
////                }

////                int packageId = Convert.ToInt32(grdPckgList.SelectedRows[0].Cells["Id"].Value);
////                int productId = Convert.ToInt32(grdProdList.Rows[e.RowIndex].Cells["ProductID"].Value);

////                using (SqlConnection conn = new SqlConnection(connectionString))
////                {
////                    conn.Open();

////                    // Check if already assigned
////                    string checkQuery = "SELECT COUNT(*) FROM CombinePackageItem WHERE Id = @pkg AND ProductID = @prd";
////                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
////                    {
////                        checkCmd.Parameters.AddWithValue("@pkg", packageId);
////                        checkCmd.Parameters.AddWithValue("@prd", productId);
////                        int exists = (int)checkCmd.ExecuteScalar();

////                        if (exists > 0)
////                        {
////                            MessageBox.Show("This product is already assigned to this package.");
////                            return;
////                        }
////                    }

////                    // Insert
////                    string insertQuery = "INSERT INTO CombinePackageItem (PackageID, ProductID) VALUES (@pkg, @prd)";
////                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
////                    {
////                        insertCmd.Parameters.AddWithValue("@pkg", packageId);
////                        insertCmd.Parameters.AddWithValue("@prd", productId);
////                        insertCmd.ExecuteNonQuery();
////                    }

////                    MessageBox.Show("Product added to package successfully.");
////                    LoadAssignedProducts(packageId);
////                }
////            }
////        }
////        private void btnAdd_Click(object sender, EventArgs e)
////        {
////            grdInProd.DataSource = null;

////            // Ensure columns exist (only once)
////            if (grdInProd.Columns.Count == 0)
////            {
////                grdInProd.Columns.Add("ProductID", "Product ID");
////                grdInProd.Columns.Add("Product", "Product Name");
////            }

////            grdInProd.Rows.Clear(); // now it's safe

////            foreach (DataGridViewRow row in grdProdList.Rows)
////            {
////                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);
////                if (isChecked)
////                {
////                    int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
////                    string productName = row.Cells["Product"].Value.ToString();

////                    grdInProd.Rows.Add(productId, productName);
////                }
////            }

////            if (grdInProd.Rows.Count == 0)
////            {
////                MessageBox.Show("Please select at least one product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////            }
////        }

////        private void btnSave_Click(object sender, EventArgs e)
////        {
////            if (!NewValidateInputs())
////            {
////                return;
////            }

////            try
////            {

////                using (SqlConnection conn = new SqlConnection(connectionString))
////                {
////                    conn.Open();
////                    string checkUser = "SELECT COUNT(*) FROM tblPackage WHERE PackageName=@PackageName";
////                    using (SqlCommand cmdCheck = new SqlCommand(checkUser, conn))
////                    {
////                        cmdCheck.Parameters.AddWithValue("@PackageName", txtNPckg.Text);
////                        int pckgExists = (int)cmdCheck.ExecuteScalar();

////                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

////                        if (pckgExists == 0)
////                        {
////                            string insertQuery = "INSERT INTO tblPackage (PackageName, PackageImage, PackageStatus) VALUES (@PackageName, @PackageImage, @PackageStatus)";
////                            string fileName = Path.GetFileName(pckgNImage.ImageLocation);
////                            string relativePath = Path.Combine("package_images", fileName);

////                          //  string relativePath = Path.Combine("package_images", pckgNImage.Image + ".jpg");
////                            string path = Path.Combine(baseDirectory, relativePath);

////                            // Ensure directory exists
////                            string directory = Path.GetDirectoryName(path);
////                            if (!Directory.Exists(directory))
////                            {
////                                Directory.CreateDirectory(directory);
////                            }

////                            // Save the image
////                            File.Copy(pckgNImage.ImageLocation, path, true);
////                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
////                            {
////                                cmdInsert.Parameters.AddWithValue("@PackageName", txtNPckg.Text);
////                                cmdInsert.Parameters.AddWithValue("@PackageImage", path);
////                                cmdInsert.Parameters.AddWithValue("@PackageStatus", cmbNStatus.SelectedItem);
////                                cmdInsert.ExecuteNonQuery();
////                            }
////                            MessageBox.Show("Package Added successful!");
////                            LoadPromo();
////                            ;
////                        }
////                        else
////                        {
////                            MessageBox.Show("Package already exists.");
////                            txtPckg.Focus();
////                        }
////                    }
////                    conn.Close();
////                }
////            }
////            catch (Exception ex)
////            {
////                MessageBox.Show("Error during the process of adding package: " + ex.Message);
////            }
////            panelNew.Visible = false;
////        }

////        private bool NewValidateInputs()
////        {
////            LoadProducts();
////            if (string.IsNullOrWhiteSpace(txtNPckg.Text))
////            {
////                MessageBox.Show("Package name cannot be empty.");
////                txtPckg.Focus();
////                return false;
////            }
////            if (!Regex.IsMatch(txtNPckg.Text, @"^[a-zA-Z0-9]+$"))
////            {
////                MessageBox.Show("Package name must contain only letters (A-Z or a-z) and numbers (0-9).");
////                txtPckg.Focus();
////                return false;
////            }
////            if (txtNPckg.Text.Length > 15)
////            {
////                MessageBox.Show("Package name must not exceed 15 characters.");
////                txtPckg.Focus();
////                return false;
////            }
////            return true;
////        }
////        private bool ValidateInputs()
////        {
////            LoadProducts();
////            if (string.IsNullOrWhiteSpace(txtPckg.Text))
////            {
////                MessageBox.Show("Package name cannot be empty.");
////                txtPckg.Focus();
////                return false;
////            }
////            if (!Regex.IsMatch(txtPckg.Text, @"^[a-zA-Z0-9]+$"))
////            {
////                MessageBox.Show("Package name must contain only letters (A-Z or a-z) and numbers (0-9).");
////                txtPckg.Focus();
////                return false;
////            }
////            if (txtPckg.Text.Length > 15)
////            {
////                MessageBox.Show("Package name must not exceed 15 characters.");
////                txtPckg.Focus();
////                return false;
////            }
////            return true;
////        }

////        private void btnNBrowse_Click(object sender, EventArgs e)
////        {
////                openFile.Filter = "Image Files(|*.jpg;*.jpeg;*.png;";
////                if (openFile.ShowDialog() == DialogResult.OK)
////                {
////                    pckgNImage.ImageLocation = openFile.FileName;
////                    pckgNImage.SizeMode = PictureBoxSizeMode.StretchImage;
////                }
////        }

////        private void btnCancel_Click(object sender, EventArgs e)
////        {
////            txtNPckg.Clear();
////            cmbNStatus.SelectedItem = null;
////            panelNew.Visible = false;
////        }

////        private void label6_Click(object sender, EventArgs e)
////        {

////        }


////    }

////}

