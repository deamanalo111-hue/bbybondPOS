using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BabybondPOSInventorySystem
{
    public partial class Accounts: Form
    {
        public Accounts()
        {
            InitializeComponent();
                        
            grdAccounts.SelectionChanged += GrdAccounts_SelectionChanged;
            LoadUsers();
        }

        private string connectionString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        private void btnNew_Click(object sender, EventArgs e)
        {
            userDeatails.Visible = true;
            userInfo.Visible = false;
          
        }
        private void btnSave_Click(object sender, EventArgs e)
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
                    string checkUser = "SELECT COUNT(*) FROM tblAccounts WHERE Username=@Username";
                    using (SqlCommand cmdCheck = new SqlCommand(checkUser, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@Username", txtUname.Text);
                        int userExists = (int)cmdCheck.ExecuteScalar();
                        if (userExists == 0)
                        {
                            string insertQuery = "INSERT INTO tblAccounts (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@Username", txtUname.Text);
                                cmdInsert.Parameters.AddWithValue("@Password", txtUpass.Text);
                                cmdInsert.Parameters.AddWithValue("@Role", cmbUrole.SelectedItem);
                                cmdInsert.ExecuteNonQuery();
                            }
                            MessageBox.Show("Registration successful!");
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("Username already exists.");
                            txtUsername.Focus();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during registration: " + ex.Message);
            }
            userDeatails.Visible = false;
            userInfo.Visible = true;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUname.Text))
            {
                MessageBox.Show("Username cannot be empty.");
                txtUname.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUpass.Text))
            {
                MessageBox.Show("Password cannot be empty.");
                txtPassword.Focus();
                return false;
            }
            if (txtUpass.Text.Length < 7)
            {
                MessageBox.Show("Password must be at least 7 characters long.");
                txtUpass.Focus(); 
                return false;
            }
            if (txtUname.Text.Length > 10)
            {
                MessageBox.Show("Username must not exceed 10 characters.");
                txtUname.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtUname.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Username must contain only letters (A-Z or a-z) and numbers (0-9).");
                txtUname.Focus();
                return false;
            }
            if (!Regex.IsMatch(txtUpass.Text, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Password must contain only letters (A-Z or a-z) and numbers (0-9).");
                txtUpass.Focus();
                return false;
            }

            return true;
        }
        private void GrdAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (grdAccounts.SelectedRows.Count > 0)
            {
                var selectedRow = grdAccounts.SelectedRows[0];
                txtId.Text = selectedRow.Cells["Id"].Value?.ToString();
                txtUsername.Text = selectedRow.Cells["Username"].Value?.ToString();
                txtPassword.Text = selectedRow.Cells["Password"].Value?.ToString();
                cmbRole.SelectedItem = selectedRow.Cells["Role"].Value?.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdAccounts.SelectedRows.Count > 0)
            {
                string username = grdAccounts.SelectedRows[0].Cells["Id"].Value.ToString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = "UPDATE tblAccounts SET Username=@Username, Password=@Password, Role=@Role WHERE Id=@Id";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Id", txtId.Text);
                        updateCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        updateCmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        updateCmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem.ToString());
                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Update successful.");
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user account to edit.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (grdAccounts.SelectedRows.Count > 0)
            {
                string username = grdAccounts.SelectedRows[0].Cells["Username"].Value.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tblAccounts WHERE Username=@Username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User  account deleted successfully.");
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("User  not found.");
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a user account to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LoadUsers()
        {
            string query = "SELECT Id, Username, Password, Role FROM tblAccounts";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                grdAccounts.DataSource = dt;
                conn.Close();
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

            GrdAccounts_SelectionChanged(sender, e);
            userDeatails.Visible = false;

        }

        private void btnUcancel_Click(object sender, EventArgs e)
        {
            txtUname.Clear();
            txtUpass.Clear();
            cmbUrole.SelectedItem = null;
            userDeatails.Visible = false;
            userDeatails.Visible = false;
            userInfo.Visible = true;

        }
    }
}
