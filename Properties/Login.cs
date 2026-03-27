using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BabybondPOSInventorySystem
{
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void Releasecapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private string conString = @"Server=LAPTOP-NOGPA80R;Database=BabybondAccounts;Trusted_Connection=True";
        string log;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            if (!ValidateCredentials(txtUsername.Text.Trim(), txtPassword.Text))
            {
                return;
            }
        }       
    
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty.");
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Contains(" "))
            {
                MessageBox.Show("Username cannot contain spaces.");
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password cannot be empty.");
                txtPassword.Focus();
                return false;
            }

            return true;
        }
        private bool ValidateCredentials(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    conn.Open();

                    string checkUserQuery = "SELECT Password, Role FROM tblAccounts " +
                        "WHERE Username = @Username COLLATE SQL_Latin1_General_CP1_CS_AS";
                    using (SqlCommand cmd = new SqlCommand(checkUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (!reader.Read())
                        {
                            MessageBox.Show("Username does not exist.");
                            txtUsername.Focus();
                            return false;
                        }

                        string storedPassword = reader["Password"].ToString();
                        string role = reader["Role"].ToString();

                        if (!string.Equals(storedPassword , password))
                        {
                            MessageBox.Show("Incorrect password.");
                            txtPassword.Focus();
                            return false;
                        }

                        UserLog.type = role == "Admin" ? "A" : "E";
                        log = txtUsername.Text;
                        this.Hide();
                        Home fh = new Home(log);
                        fh.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating credentials: " + ex.Message);
                return false;
            }

            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cbShowPass.Checked ? '\0' : '*';
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            Releasecapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
