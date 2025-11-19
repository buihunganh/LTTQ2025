namespace BTL_LTTQ.GUI
{
    partial class frmBanHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.panelPayment = new System.Windows.Forms.Panel();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.lblTienThua = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTienKhach = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblThanhToan = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numGiamGia = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.panelPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGia)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.btnAdd);
            this.panelLeft.Controls.Add(this.numSoLuong);
            this.panelLeft.Controls.Add(this.label3);
            this.panelLeft.Controls.Add(this.cboSanPham);
            this.panelLeft.Controls.Add(this.label2);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(20);
            this.panelLeft.Size = new System.Drawing.Size(350, 600);
            this.panelLeft.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 170);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(300, 50);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "THÊM VÀO GIỎ HÀNG";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(20, 115);
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(300, 25);
            this.numSoLuong.TabIndex = 1;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Số lượng mua:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSanPham.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSanPham.Location = new System.Drawing.Point(20, 45);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(300, 25);
            this.cboSanPham.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tìm & Chọn Giày:";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.dgvGioHang);
            this.panelRight.Controls.Add(this.panelPayment);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(350, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(650, 600);
            this.panelRight.TabIndex = 0;
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.Location = new System.Drawing.Point(0, 0);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.Size = new System.Drawing.Size(650, 280);
            this.dgvGioHang.TabIndex = 0;
            // 
            // panelPayment
            // 
            this.panelPayment.Controls.Add(this.btnThanhToan);
            this.panelPayment.Controls.Add(this.lblTienThua);
            this.panelPayment.Controls.Add(this.label9);
            this.panelPayment.Controls.Add(this.txtTienKhach);
            this.panelPayment.Controls.Add(this.label8);
            this.panelPayment.Controls.Add(this.lblThanhToan);
            this.panelPayment.Controls.Add(this.label7);
            this.panelPayment.Controls.Add(this.numGiamGia);
            this.panelPayment.Controls.Add(this.label6);
            this.panelPayment.Controls.Add(this.lblTongTien);
            this.panelPayment.Controls.Add(this.label5);
            this.panelPayment.Controls.Add(this.cboKhachHang);
            this.panelPayment.Controls.Add(this.label4);
            this.panelPayment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPayment.Location = new System.Drawing.Point(0, 280);
            this.panelPayment.Name = "panelPayment";
            this.panelPayment.Padding = new System.Windows.Forms.Padding(20);
            this.panelPayment.Size = new System.Drawing.Size(650, 320);
            this.panelPayment.TabIndex = 1;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(380, 180);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(200, 80);
            this.btnThanhToan.TabIndex = 0;
            this.btnThanhToan.Text = "THANH TOÁN & IN HÓA ĐƠN";
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // lblTienThua
            // 
            this.lblTienThua.AutoSize = true;
            this.lblTienThua.ForeColor = System.Drawing.Color.Lime;
            this.lblTienThua.Location = new System.Drawing.Point(150, 230);
            this.lblTienThua.Name = "lblTienThua";
            this.lblTienThua.Size = new System.Drawing.Size(50, 19);
            this.lblTienThua.TabIndex = 1;
            this.lblTienThua.Text = "0 VNĐ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 230);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 19);
            this.label9.TabIndex = 2;
            this.label9.Text = "Tiền thừa:";
            // 
            // txtTienKhach
            // 
            this.txtTienKhach.Location = new System.Drawing.Point(150, 185);
            this.txtTienKhach.Name = "txtTienKhach";
            this.txtTienKhach.Size = new System.Drawing.Size(200, 25);
            this.txtTienKhach.TabIndex = 3;
            this.txtTienKhach.TextChanged += new System.EventHandler(this.txtTienKhach_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 19);
            this.label8.TabIndex = 4;
            this.label8.Text = "Tiền khách đưa:";
            // 
            // lblThanhToan
            // 
            this.lblThanhToan.AutoSize = true;
            this.lblThanhToan.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblThanhToan.ForeColor = System.Drawing.Color.Yellow;
            this.lblThanhToan.Location = new System.Drawing.Point(180, 140);
            this.lblThanhToan.Name = "lblThanhToan";
            this.lblThanhToan.Size = new System.Drawing.Size(80, 30);
            this.lblThanhToan.TabIndex = 5;
            this.lblThanhToan.Text = "0 VNĐ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(20, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "KHÁCH CẦN TRẢ:";
            // 
            // numGiamGia
            // 
            this.numGiamGia.Location = new System.Drawing.Point(130, 95);
            this.numGiamGia.Name = "numGiamGia";
            this.numGiamGia.Size = new System.Drawing.Size(100, 25);
            this.numGiamGia.TabIndex = 7;
            this.numGiamGia.ValueChanged += new System.EventHandler(this.SumTotal_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Giảm giá (%):";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Location = new System.Drawing.Point(180, 60);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(59, 21);
            this.lblTongTien.TabIndex = 9;
            this.lblTongTien.Text = "0 VNĐ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tổng tiền hàng:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.Location = new System.Drawing.Point(130, 15);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(250, 25);
            this.cboKhachHang.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "Khách hàng:";
            // 
            // frmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "frmBanHang";
            this.Text = "Bán Hàng (POS)";
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.panelPayment.ResumeLayout(false);
            this.panelPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiamGia)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Panel panelPayment;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Label lblTienThua;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTienKhach;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblThanhToan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numGiamGia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label label4;
    }
}