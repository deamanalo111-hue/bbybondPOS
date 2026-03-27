namespace BabybondPOSInventorySystem
{
    partial class POSPackage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pckgPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.picPckgImage = new System.Windows.Forms.PictureBox();
            this.pckgName = new System.Windows.Forms.Label();
            this.pckgPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPckgImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pckgPanel
            // 
            this.pckgPanel.BackColor = System.Drawing.Color.White;
            this.pckgPanel.Controls.Add(this.picPckgImage);
            this.pckgPanel.Controls.Add(this.pckgName);
            this.pckgPanel.Location = new System.Drawing.Point(0, 0);
            this.pckgPanel.Name = "pckgPanel";
            this.pckgPanel.Size = new System.Drawing.Size(152, 142);
            this.pckgPanel.TabIndex = 0;
            // 
            // picPckgImage
            // 
            this.picPckgImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picPckgImage.Location = new System.Drawing.Point(3, 3);
            this.picPckgImage.Name = "picPckgImage";
            this.picPckgImage.Size = new System.Drawing.Size(150, 100);
            this.picPckgImage.TabIndex = 0;
            this.picPckgImage.TabStop = false;
            this.picPckgImage.Click += new System.EventHandler(this.picPckgImage_Click);
            // 
            // pckgName
            // 
            this.pckgName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pckgName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pckgName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pckgName.Location = new System.Drawing.Point(3, 106);
            this.pckgName.Name = "pckgName";
            this.pckgName.Size = new System.Drawing.Size(110, 29);
            this.pckgName.TabIndex = 1;
            this.pckgName.Text = "Package Name";
            this.pckgName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // POSPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pckgPanel);
            this.Name = "POSPackage";
            this.Size = new System.Drawing.Size(152, 142);
            this.pckgPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPckgImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pckgPanel;
        private System.Windows.Forms.PictureBox picPckgImage;
        private System.Windows.Forms.Label pckgName;
    }
}
