using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace BabybondPOSInventorySystem
{
    public partial class Home: Form
    {
        public Home()
        {
            InitializeComponent();
        }

        public Home (string log) : this()
        {
            lblUsername.Text = log;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void Releasecapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void OpenFormInPanel(object Dashboard)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            Form f = Dashboard as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(f);
            this.mainPanel.Tag = f;
            f.Show();
        }
        private void btnLogout_Click(object sender, System.EventArgs e)
        {
            Login lf = new Login();
            lf.Show();
            this.Hide();
        }
        private void btnMenu_Click(object sender, System.EventArgs e)
        {
            if (slideBar.Width == 300)
            {
                slideBar.Width = 70;
                smallLogo.Visible = true;
                bigLogo.Visible = false;
            }
            else
            {
                slideBar.Width = 300;
                smallLogo.Visible = false;
                bigLogo.Visible = true;
            }
        }

        private void btnRestore_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestore.Visible = false;
            btnMaximixe.Visible = true;
        }

        private void btnMinimize_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            btnRestore.Visible = false;
            btnMaximixe.Visible = true;
        }

        private void btnMaximixe_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnRestore.Visible = true;
            btnMaximixe.Visible = false;
        }

        private void Home_Load(object sender, System.EventArgs e)
        {
            btnDashboard_Click(null, e);
        }

        private void btnDashboard_Click(object sender, System.EventArgs e)
        {
            OpenFormInPanel(new Dashboard());
        }

        private void topBar_MouseDown(object sender, MouseEventArgs e)
        {
            Releasecapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void slideBar_Paint(object sender, PaintEventArgs e)
        {
            if (UserLog.type == "A")
            {
                btnDashboard.Visible = true;
                btnPos.Visible = true;
                btnStock.Visible = true;
                btnPackage.Visible = true;
                btnAccounts.Visible = true;
                btnReports.Visible = true;
                btnHistory.Visible = true;
            }
            else if(UserLog.type == "E")
            {
                btnDashboard.Visible = true;
                btnPos.Visible = true;
                btnStock.Visible = true;
                btnPackage.Visible = false;
                btnAccounts.Visible = false;
                btnReports.Visible = false;
                btnHistory.Visible = false;
            }
        }

        private void btnPos_Click(object sender, System.EventArgs e)
        {
            POS pf = new POS();
            pf.Show();
        }

        private void btnStock_Click(object sender, System.EventArgs e)
        {
            OpenFormInPanel(new Stocks());
        }

        private void btnPackage_Click(object sender, System.EventArgs e)
        {
            OpenFormInPanel(new Packages());
        }

        private void btnAccounts_Click(object sender, System.EventArgs e)
        {
            OpenFormInPanel(new Accounts());
        }

        private void btnReports_Click(object sender, System.EventArgs e)
        {
            OpenFormInPanel(new Reports());
        }

        private void btnHistory_Click(object sender, System.EventArgs e)
        {
            OpenFormInPanel(new History());
        }
    }
}
