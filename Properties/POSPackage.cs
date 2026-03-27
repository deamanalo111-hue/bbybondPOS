using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabybondPOSInventorySystem
{
    public partial class POSPackage: UserControl
    {
        public POSPackage()
        {
            InitializeComponent();
            Panel();
        }
        private void Panel()
        {
            pckgPanel = new FlowLayoutPanel();
            pckgPanel.Dock = DockStyle.Fill;
            pckgPanel.AutoScroll = true;
        }
        public int Id {get; set;}
        public string PackageName
        {
            get => pckgName.Text;
            set => pckgName.Text = value;
        }
        
        public string PackageStatus {get; set;}
        public Image PackageImage
        {
            get => picPckgImage.Image;
            set => picPckgImage.Image = value;
        }
        public event EventHandler selectPckg = null;

        private void picPckgImage_Click(object sender, EventArgs e)
        {
            selectPckg?.Invoke(this, EventArgs.Empty);
        }
    }
}
