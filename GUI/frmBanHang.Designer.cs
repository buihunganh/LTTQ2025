namespace BTL_LTTQ.GUI
{
    partial class frmBanHang
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnGoToInvoice = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.numGiamGiaSP = new System.Windows.Forms.NumericUpDown();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblChonGiay = new System.Windows.Forms.Label();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGiaSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.pnlLeft.Controls.Add(this.lblNote);
            this.pnlLeft.Controls.Add(this.btnGoToInvoice);
            this.pnlLeft.Controls.Add(this.btnAdd);
            this.pnlLeft.Controls.Add(this.numGiamGiaSP);
            this.pnlLeft.Controls.Add(this.lblGiamGia);
            this.pnlLeft.Controls.Add(this.numSoLuong);
            this.pnlLeft.Controls.Add(this.lblSoLuong);
            this.pnlLeft.Controls.Add(this.cboSanPham);
            this.pnlLeft.Controls.Add(this.lblChonGiay);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLeft.Size = new System.Drawing.Size(350, 600);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.ForeColor = System.Drawing.Color.Yellow;
            this.lblNote.Location = new System.Drawing.Point(20, 380);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(160, 16);
            this.lblNote.TabIndex = 8;
            this.lblNote.Text = "* Kích đúp dòng để xóa";
            // 
            // btnGoToInvoice
            // 
            this.btnGoToInvoice.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGoToInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToInvoice.FlatAppearance.BorderSize = 0;
            this.btnGoToInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(112)))), ((int)(((byte)(23)))));
            this.btnGoToInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(153)))), ((int)(((byte)(38)))));
            this.btnGoToInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToInvoice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToInvoice.ForeColor = System.Drawing.Color.White;
            this.btnGoToInvoice.Location = new System.Drawing.Point(20, 305);
            this.btnGoToInvoice.Name = "btnGoToInvoice";
            this.btnGoToInvoice.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnGoToInvoice.Size = new System.Drawing.Size(300, 65);
            this.btnGoToInvoice.TabIndex = 7;
            this.btnGoToInvoice.Text = "🧾  TẠO HÓA ĐƠN";
            this.btnGoToInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGoToInvoice.UseVisualStyleBackColor = false;
            this.btnGoToInvoice.Click += new System.EventHandler(this.btnGoToInvoice_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(86)))), ((int)(((byte)(75)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(125)))), ((int)(((byte)(116)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 230);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnAdd.Size = new System.Drawing.Size(300, 55);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "🛒  THÊM VÀO GIỎ";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // numGiamGiaSP
            // 
            this.numGiamGiaSP.Location = new System.Drawing.Point(20, 175);
            this.numGiamGiaSP.Name = "numGiamGiaSP";
            this.numGiamGiaSP.Size = new System.Drawing.Size(300, 22);
            this.numGiamGiaSP.TabIndex = 5;
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGiamGia.Location = new System.Drawing.Point(20, 150);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(95, 16);
            this.lblGiamGia.TabIndex = 4;
            this.lblGiamGia.Text = "Giảm giá (%):";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(20, 110);
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(300, 22);
            this.numSoLuong.TabIndex = 3;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSoLuong.Location = new System.Drawing.Point(20, 85);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(67, 16);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            this.cboSanPham.Location = new System.Drawing.Point(20, 45);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(300, 24);
            this.cboSanPham.TabIndex = 1;
            // 
            // lblChonGiay
            // 
            this.lblChonGiay.AutoSize = true;
            this.lblChonGiay.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblChonGiay.Location = new System.Drawing.Point(20, 20);
            this.lblChonGiay.Name = "lblChonGiay";
            this.lblChonGiay.Size = new System.Drawing.Size(72, 16);
            this.lblChonGiay.TabIndex = 0;
            this.lblChonGiay.Text = "Chọn giày:";
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioHang.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.dgvGioHang.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvGioHang.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvGioHang.DefaultCellStyle.ForeColor = System.Drawing.Color.Gainsboro;
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.Location = new System.Drawing.Point(350, 0);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersWidth = 51;
            this.dgvGioHang.RowTemplate.Height = 24;
            this.dgvGioHang.Size = new System.Drawing.Size(650, 600);
            this.dgvGioHang.TabIndex = 1;
            this.dgvGioHang.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGioHang_CellDoubleClick);
            // 
            // frmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvGioHang);
            this.Controls.Add(this.pnlLeft);
            this.Name = "frmBanHang";
            this.Text = "frmBanHang";
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGiaSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Button btnGoToInvoice;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numGiamGiaSP;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblChonGiay;
        private System.Windows.Forms.DataGridView dgvGioHang;
    }
}
