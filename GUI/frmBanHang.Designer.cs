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
            System.Windows.Forms.Panel pnlLeft;
            System.Windows.Forms.Label lblSanPham;
            System.Windows.Forms.Label lblSoLuong;
            System.Windows.Forms.Label lblGiamGia;
            System.Windows.Forms.Label lblHint;
            
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.numGiamGiaSP = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnGoToInvoice = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            pnlLeft = new System.Windows.Forms.Panel();
            lblSanPham = new System.Windows.Forms.Label();
            lblSoLuong = new System.Windows.Forms.Label();
            lblGiamGia = new System.Windows.Forms.Label();
            lblHint = new System.Windows.Forms.Label();
            pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGiaSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            pnlLeft.Controls.Add(lblHint);
            pnlLeft.Controls.Add(this.btnGoToInvoice);
            pnlLeft.Controls.Add(this.btnAdd);
            pnlLeft.Controls.Add(this.numGiamGiaSP);
            pnlLeft.Controls.Add(lblGiamGia);
            pnlLeft.Controls.Add(this.numSoLuong);
            pnlLeft.Controls.Add(lblSoLuong);
            pnlLeft.Controls.Add(this.cboSanPham);
            pnlLeft.Controls.Add(lblSanPham);
            pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            pnlLeft.Location = new System.Drawing.Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new System.Windows.Forms.Padding(20);
            pnlLeft.Size = new System.Drawing.Size(350, 600);
            pnlLeft.TabIndex = 0;
            // 
            // lblSanPham
            // 
            lblSanPham.AutoSize = true;
            lblSanPham.ForeColor = System.Drawing.Color.Gainsboro;
            lblSanPham.Location = new System.Drawing.Point(20, 20);
            lblSanPham.Name = "lblSanPham";
            lblSanPham.Size = new System.Drawing.Size(70, 16);
            lblSanPham.TabIndex = 0;
            lblSanPham.Text = "Chọn giày:";
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
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.ForeColor = System.Drawing.Color.Gainsboro;
            lblSoLuong.Location = new System.Drawing.Point(20, 85);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new System.Drawing.Size(67, 16);
            lblSoLuong.TabIndex = 2;
            lblSoLuong.Text = "Số lượng:";
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
            // lblGiamGia
            // 
            lblGiamGia.AutoSize = true;
            lblGiamGia.ForeColor = System.Drawing.Color.Gainsboro;
            lblGiamGia.Location = new System.Drawing.Point(20, 150);
            lblGiamGia.Name = "lblGiamGia";
            lblGiamGia.Size = new System.Drawing.Size(91, 16);
            lblGiamGia.TabIndex = 4;
            lblGiamGia.Text = "Giảm giá (%):";
            // 
            // numGiamGiaSP
            // 
            this.numGiamGiaSP.Location = new System.Drawing.Point(20, 175);
            this.numGiamGiaSP.Name = "numGiamGiaSP";
            this.numGiamGiaSP.Size = new System.Drawing.Size(300, 22);
            this.numGiamGiaSP.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(85)))), ((int)(((byte)(75)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(104)))), ((int)(((byte)(93)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
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
            // btnGoToInvoice
            // 
            this.btnGoToInvoice.BackColor = System.Drawing.Color.ForestGreen;
            this.btnGoToInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToInvoice.FlatAppearance.BorderSize = 0;
            this.btnGoToInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(133)))), ((int)(((byte)(33)))));
            this.btnGoToInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(168)))), ((int)(((byte)(51)))));
            this.btnGoToInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoToInvoice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
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
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.ForeColor = System.Drawing.Color.Yellow;
            lblHint.Location = new System.Drawing.Point(20, 380);
            lblHint.Name = "lblHint";
            lblHint.Size = new System.Drawing.Size(171, 16);
            lblHint.TabIndex = 8;
            lblHint.Text = "* Kích đúp dòng để xóa";
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
            this.Controls.Add(pnlLeft);
            this.Name = "frmBanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bán hàng (POS)";
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGiaSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.NumericUpDown numGiamGiaSP;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnGoToInvoice;
        private System.Windows.Forms.DataGridView dgvGioHang;
    }
}