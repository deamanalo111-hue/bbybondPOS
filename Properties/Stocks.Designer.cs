namespace BabybondPOSInventorySystem
{
    partial class Stocks
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stocks));
            this.label4 = new System.Windows.Forms.Label();
            this.grdProdList = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.prodDetails = new System.Windows.Forms.Panel();
            this.txtId = new System.Windows.Forms.TextBox();
            this.prodNo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnASave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnESave = new System.Windows.Forms.Button();
            this.txtAProdName = new System.Windows.Forms.TextBox();
            this.txtADescription = new System.Windows.Forms.TextBox();
            this.txtEProdName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdProdList)).BeginInit();
            this.prodDetails.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Cooper Black", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(456, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 32);
            this.label4.TabIndex = 19;
            this.label4.Text = "Stocks";
            // 
            // grdProdList
            // 
            this.grdProdList.AllowUserToAddRows = false;
            this.grdProdList.AllowUserToDeleteRows = false;
            this.grdProdList.AllowUserToResizeRows = false;
            this.grdProdList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grdProdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdProdList.BackgroundColor = System.Drawing.Color.White;
            this.grdProdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdProdList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdProdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdProdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdProdList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.grdProdList.EnableHeadersVisualStyles = false;
            this.grdProdList.Location = new System.Drawing.Point(89, 117);
            this.grdProdList.Name = "grdProdList";
            this.grdProdList.ReadOnly = true;
            this.grdProdList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdProdList.RowHeadersVisible = false;
            this.grdProdList.RowHeadersWidth = 51;
            this.grdProdList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdProdList.RowTemplate.Height = 24;
            this.grdProdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdProdList.Size = new System.Drawing.Size(910, 374);
            this.grdProdList.TabIndex = 39;
            this.grdProdList.SelectionChanged += new System.EventHandler(this.grdProdList_SelectionChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(621, 528);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 35);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(956, 84);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(43, 27);
            this.btnSearch.TabIndex = 44;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtSearch.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(706, 84);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(249, 27);
            this.txtSearch.TabIndex = 43;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(485, 528);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 35);
            this.btnEdit.TabIndex = 45;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(433, 528);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(232, 35);
            this.btnInventory.TabIndex = 47;
            this.btnInventory.Text = "Input Inventory";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Visible = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(349, 528);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 35);
            this.btnAdd.TabIndex = 48;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // prodDetails
            // 
            this.prodDetails.Controls.Add(this.txtId);
            this.prodDetails.Controls.Add(this.prodNo);
            this.prodDetails.Controls.Add(this.panel3);
            this.prodDetails.Controls.Add(this.txtAProdName);
            this.prodDetails.Controls.Add(this.txtADescription);
            this.prodDetails.Controls.Add(this.txtEProdName);
            this.prodDetails.Controls.Add(this.label2);
            this.prodDetails.Controls.Add(this.txtEDescription);
            this.prodDetails.Controls.Add(this.label1);
            this.prodDetails.Controls.Add(this.panel2);
            this.prodDetails.Location = new System.Drawing.Point(281, 144);
            this.prodDetails.MaximumSize = new System.Drawing.Size(527, 315);
            this.prodDetails.MinimumSize = new System.Drawing.Size(527, 315);
            this.prodDetails.Name = "prodDetails";
            this.prodDetails.Size = new System.Drawing.Size(527, 315);
            this.prodDetails.TabIndex = 49;
            this.prodDetails.Visible = false;
            // 
            // txtId
            // 
            this.txtId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtId.BackColor = System.Drawing.Color.White;
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtId.Enabled = false;
            this.txtId.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(169, 81);
            this.txtId.Multiline = true;
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(312, 24);
            this.txtId.TabIndex = 60;
            this.txtId.Visible = false;
            // 
            // prodNo
            // 
            this.prodNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.prodNo.AutoSize = true;
            this.prodNo.BackColor = System.Drawing.Color.Transparent;
            this.prodNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prodNo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodNo.ForeColor = System.Drawing.Color.Black;
            this.prodNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.prodNo.Location = new System.Drawing.Point(46, 83);
            this.prodNo.Name = "prodNo";
            this.prodNo.Size = new System.Drawing.Size(93, 18);
            this.prodNo.TabIndex = 59;
            this.prodNo.Text = "Product No:";
            this.prodNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.prodNo.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(218)))), ((int)(((byte)(227)))));
            this.panel3.Controls.Add(this.btnASave);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnESave);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 265);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(527, 50);
            this.panel3.TabIndex = 58;
            // 
            // btnASave
            // 
            this.btnASave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnASave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnASave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnASave.FlatAppearance.BorderSize = 0;
            this.btnASave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnASave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnASave.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnASave.ForeColor = System.Drawing.Color.White;
            this.btnASave.Location = new System.Drawing.Point(390, 7);
            this.btnASave.Name = "btnASave";
            this.btnASave.Size = new System.Drawing.Size(130, 35);
            this.btnASave.TabIndex = 52;
            this.btnASave.Text = "Save";
            this.btnASave.UseVisualStyleBackColor = false;
            this.btnASave.Visible = false;
            this.btnASave.Click += new System.EventHandler(this.btnASave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(254, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(130, 35);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnESave
            // 
            this.btnESave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnESave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(195)))), ((int)(((byte)(246)))));
            this.btnESave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnESave.FlatAppearance.BorderSize = 0;
            this.btnESave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(161)))), ((int)(((byte)(235)))));
            this.btnESave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnESave.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESave.ForeColor = System.Drawing.Color.White;
            this.btnESave.Location = new System.Drawing.Point(390, 7);
            this.btnESave.Name = "btnESave";
            this.btnESave.Size = new System.Drawing.Size(130, 35);
            this.btnESave.TabIndex = 47;
            this.btnESave.Text = "Save";
            this.btnESave.UseVisualStyleBackColor = false;
            this.btnESave.Click += new System.EventHandler(this.btnESave_Click);
            // 
            // txtAProdName
            // 
            this.txtAProdName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAProdName.BackColor = System.Drawing.Color.White;
            this.txtAProdName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAProdName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtAProdName.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAProdName.Location = new System.Drawing.Point(169, 123);
            this.txtAProdName.Multiline = true;
            this.txtAProdName.Name = "txtAProdName";
            this.txtAProdName.Size = new System.Drawing.Size(312, 24);
            this.txtAProdName.TabIndex = 57;
            this.txtAProdName.Visible = false;
            // 
            // txtADescription
            // 
            this.txtADescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtADescription.BackColor = System.Drawing.Color.White;
            this.txtADescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtADescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtADescription.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtADescription.Location = new System.Drawing.Point(169, 166);
            this.txtADescription.Multiline = true;
            this.txtADescription.Name = "txtADescription";
            this.txtADescription.Size = new System.Drawing.Size(312, 24);
            this.txtADescription.TabIndex = 56;
            this.txtADescription.Visible = false;
            // 
            // txtEProdName
            // 
            this.txtEProdName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEProdName.BackColor = System.Drawing.Color.White;
            this.txtEProdName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEProdName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtEProdName.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEProdName.Location = new System.Drawing.Point(169, 125);
            this.txtEProdName.Multiline = true;
            this.txtEProdName.Name = "txtEProdName";
            this.txtEProdName.Size = new System.Drawing.Size(312, 24);
            this.txtEProdName.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(46, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 54;
            this.label2.Text = "Description:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEDescription
            // 
            this.txtEDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEDescription.BackColor = System.Drawing.Color.White;
            this.txtEDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtEDescription.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEDescription.Location = new System.Drawing.Point(169, 166);
            this.txtEDescription.Multiline = true;
            this.txtEDescription.Name = "txtEDescription";
            this.txtEDescription.Size = new System.Drawing.Size(312, 24);
            this.txtEDescription.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(46, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 18);
            this.label1.TabIndex = 52;
            this.label1.Text = "Product Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(238)))), ((int)(((byte)(235)))));
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 50);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Cooper Black", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(138)))), ((int)(((byte)(170)))));
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "Product Details";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Stocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(210)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1100, 620);
            this.Controls.Add(this.prodDetails);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grdProdList);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Stocks";
            this.Text = "Stocks";
            ((System.ComponentModel.ISupportInitialize)(this.grdProdList)).EndInit();
            this.prodDetails.ResumeLayout(false);
            this.prodDetails.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdProdList;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel prodDetails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAProdName;
        private System.Windows.Forms.TextBox txtADescription;
        private System.Windows.Forms.TextBox txtEProdName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnASave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnESave;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label prodNo;
    }
}