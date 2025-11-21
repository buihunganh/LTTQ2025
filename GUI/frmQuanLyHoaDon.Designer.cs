namespace BTL_LTTQ.GUI
{
    partial class frmQuanLyHoaDon
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
            System.Windows.Forms.Label lblFrom;
            System.Windows.Forms.Label lblTo;
            System.Windows.Forms.Label lblNV;
            System.Windows.Forms.Label lblKH;
            System.Windows.Forms.Label lblMaHD;
            System.Windows.Forms.GroupBox grpFilter;
            System.Windows.Forms.TableLayoutPanel tlp;
            
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.cboMaHD = new System.Windows.Forms.ComboBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            grpFilter = new System.Windows.Forms.GroupBox();
            tlp = new System.Windows.Forms.TableLayoutPanel();
            lblFrom = new System.Windows.Forms.Label();
            lblTo = new System.Windows.Forms.Label();
            lblNV = new System.Windows.Forms.Label();
            lblKH = new System.Windows.Forms.Label();
            lblMaHD = new System.Windows.Forms.Label();
            grpFilter.SuspendLayout();
            tlp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFrom
            // 
            lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            lblFrom.ForeColor = System.Drawing.Color.Gainsboro;
            lblFrom.Location = new System.Drawing.Point(3, 0);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new System.Drawing.Size(94, 35);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "Từ ngày:";
            lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTo
            // 
            lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            lblTo.ForeColor = System.Drawing.Color.Gainsboro;
            lblTo.Location = new System.Drawing.Point(543, 0);
            lblTo.Name = "lblTo";
            lblTo.Size = new System.Drawing.Size(94, 35);
            lblTo.TabIndex = 2;
            lblTo.Text = "Đến ngày:";
            lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNV
            // 
            lblNV.Dock = System.Windows.Forms.DockStyle.Fill;
            lblNV.ForeColor = System.Drawing.Color.Gainsboro;
            lblNV.Location = new System.Drawing.Point(3, 35);
            lblNV.Name = "lblNV";
            lblNV.Size = new System.Drawing.Size(94, 35);
            lblNV.TabIndex = 4;
            lblNV.Text = "Nhân viên:";
            lblNV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKH
            // 
            lblKH.Dock = System.Windows.Forms.DockStyle.Fill;
            lblKH.ForeColor = System.Drawing.Color.Gainsboro;
            lblKH.Location = new System.Drawing.Point(543, 35);
            lblKH.Name = "lblKH";
            lblKH.Size = new System.Drawing.Size(94, 35);
            lblKH.TabIndex = 6;
            lblKH.Text = "Khách hàng:";
            lblKH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMaHD
            // 
            lblMaHD.Dock = System.Windows.Forms.DockStyle.Fill;
            lblMaHD.ForeColor = System.Drawing.Color.Gainsboro;
            lblMaHD.Location = new System.Drawing.Point(3, 70);
            lblMaHD.Name = "lblMaHD";
            lblMaHD.Size = new System.Drawing.Size(94, 35);
            lblMaHD.TabIndex = 8;
            lblMaHD.Text = "Mã hóa đơn:";
            lblMaHD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(103, 3);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(434, 22);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.Value = System.DateTime.Now.AddDays(-30);
            // 
            // dtpTo
            // 
            this.dtpTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(643, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(434, 22);
            this.dtpTo.TabIndex = 3;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenNV.Location = new System.Drawing.Point(103, 38);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(434, 22);
            this.txtTenNV.TabIndex = 5;
            // 
            // txtTenKH
            // 
            this.txtTenKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenKH.Location = new System.Drawing.Point(643, 38);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(434, 22);
            this.txtTenKH.TabIndex = 7;
            // 
            // cboMaHD
            // 
            this.cboMaHD.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboMaHD.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboMaHD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMaHD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboMaHD.FormattingEnabled = true;
            this.cboMaHD.Location = new System.Drawing.Point(103, 73);
            this.cboMaHD.Name = "cboMaHD";
            this.cboMaHD.Size = new System.Drawing.Size(974, 24);
            this.cboMaHD.TabIndex = 9;
            // 
            // btnTim
            // 
            this.btnTim.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnTim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTim.FlatAppearance.BorderSize = 0;
            this.btnTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTim.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTim.ForeColor = System.Drawing.Color.White;
            this.btnTim.Location = new System.Drawing.Point(415, 108);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(250, 40);
            this.btnTim.TabIndex = 10;
            this.btnTim.Text = "🔍 TÌM KIẾM HÓA ĐƠN";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.BtnTim_Click);
            // 
            // tlp
            // 
            tlp.ColumnCount = 4;
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp.Controls.Add(lblFrom, 0, 0);
            tlp.Controls.Add(this.dtpFrom, 1, 0);
            tlp.Controls.Add(lblTo, 2, 0);
            tlp.Controls.Add(this.dtpTo, 3, 0);
            tlp.Controls.Add(lblNV, 0, 1);
            tlp.Controls.Add(this.txtTenNV, 1, 1);
            tlp.Controls.Add(lblKH, 2, 1);
            tlp.Controls.Add(this.txtTenKH, 3, 1);
            tlp.Controls.Add(lblMaHD, 0, 2);
            tlp.Controls.Add(this.cboMaHD, 1, 2);
            tlp.Controls.Add(this.btnTim, 0, 3);
            tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp.Location = new System.Drawing.Point(10, 25);
            tlp.Name = "tlp";
            tlp.RowCount = 4;
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tlp.SetColumnSpan(this.cboMaHD, 3);
            tlp.SetColumnSpan(this.btnTim, 4);
            tlp.Size = new System.Drawing.Size(1080, 155);
            tlp.TabIndex = 0;
            // 
            // grpFilter
            // 
            grpFilter.Controls.Add(tlp);
            grpFilter.Dock = System.Windows.Forms.DockStyle.Top;
            grpFilter.ForeColor = System.Drawing.Color.Gainsboro;
            grpFilter.Location = new System.Drawing.Point(0, 0);
            grpFilter.Name = "grpFilter";
            grpFilter.Padding = new System.Windows.Forms.Padding(10);
            grpFilter.Size = new System.Drawing.Size(1100, 170);
            grpFilter.TabIndex = 0;
            grpFilter.TabStop = false;
            grpFilter.Text = "Bộ lọc tìm kiếm";
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHoaDon.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.dgvHoaDon.DefaultCellStyle.ForeColor = System.Drawing.Color.Gainsboro;
            this.dgvHoaDon.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(82)))), ((int)(((byte)(110)))));
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.EnableHeadersVisualStyles = false;
            this.dgvHoaDon.Location = new System.Drawing.Point(0, 170);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowTemplate.Height = 35;
            this.dgvHoaDon.Size = new System.Drawing.Size(1100, 530);
            this.dgvHoaDon.TabIndex = 1;
            this.dgvHoaDon.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvHoaDon_CellDoubleClick);
            // 
            // frmQuanLyHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(grpFilter);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "frmQuanLyHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn";
            this.Load += new System.EventHandler(this.frmQuanLyHoaDon_Load);
            grpFilter.ResumeLayout(false);
            tlp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.ComboBox cboMaHD;
        private System.Windows.Forms.Button btnTim;
    }
}