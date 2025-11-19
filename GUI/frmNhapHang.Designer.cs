namespace BTL_LTTQ.GUI
{
    partial class frmNhapHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabNhapHang = new System.Windows.Forms.TabPage();
            this.panelInput = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.numGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvChiTietNhap = new System.Windows.Forms.DataGridView();
            this.tabBaoCao = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvCanhBao = new System.Windows.Forms.DataGridView();

            // --- Styling Variables ---
            System.Drawing.Color backColor = System.Drawing.Color.FromArgb(22, 24, 45);
            System.Drawing.Color panelColor = System.Drawing.Color.FromArgb(43, 46, 75);
            System.Drawing.Color btnColor = System.Drawing.Color.FromArgb(232, 90, 79);
            System.Drawing.Color textColor = System.Drawing.Color.Gainsboro;

            // --- TabControl ---
            this.tabControl1.Controls.Add(this.tabNhapHang);
            this.tabControl1.Controls.Add(this.tabBaoCao);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1000, 600);
            this.tabControl1.TabIndex = 0;

            // --- Tab Nhap Hang ---
            this.tabNhapHang.BackColor = backColor;
            this.tabNhapHang.Controls.Add(this.dgvChiTietNhap);
            this.tabNhapHang.Controls.Add(this.panelInput);
            this.tabNhapHang.Text = "Nhập Hàng Mới";
            this.tabNhapHang.Location = new System.Drawing.Point(4, 30);

            // --- Panel Input (Left) ---
            this.panelInput.BackColor = panelColor;
            this.panelInput.Controls.Add(this.btnLuu);
            this.panelInput.Controls.Add(this.lblTongTien);
            this.panelInput.Controls.Add(this.label6);
            this.panelInput.Controls.Add(this.btnThem);
            this.panelInput.Controls.Add(this.numGiaNhap);
            this.panelInput.Controls.Add(this.label4);
            this.panelInput.Controls.Add(this.numSoLuong);
            this.panelInput.Controls.Add(this.label3);
            this.panelInput.Controls.Add(this.cboSanPham);
            this.panelInput.Controls.Add(this.label2);
            this.panelInput.Controls.Add(this.cboNhaCungCap);
            this.panelInput.Controls.Add(this.label1);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInput.ForeColor = textColor;
            this.panelInput.Size = new System.Drawing.Size(320, 566);

            // Controls setup
            this.label1.AutoSize = true; this.label1.Location = new System.Drawing.Point(20, 20); this.label1.Text = "Nhà cung cấp:";
            this.cboNhaCungCap.Location = new System.Drawing.Point(24, 45); this.cboNhaCungCap.Size = new System.Drawing.Size(270, 28);

            this.label2.AutoSize = true; this.label2.Location = new System.Drawing.Point(20, 90); this.label2.Text = "Chọn Sản phẩm:";
            this.cboSanPham.Location = new System.Drawing.Point(24, 115); this.cboSanPham.Size = new System.Drawing.Size(270, 28);
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.label3.AutoSize = true; this.label3.Location = new System.Drawing.Point(20, 160); this.label3.Text = "Số lượng nhập:";
            this.numSoLuong.Location = new System.Drawing.Point(24, 185); this.numSoLuong.Size = new System.Drawing.Size(270, 28);

            this.label4.AutoSize = true; this.label4.Location = new System.Drawing.Point(20, 230); this.label4.Text = "Giá nhập:";
            this.numGiaNhap.Location = new System.Drawing.Point(24, 255); this.numGiaNhap.Size = new System.Drawing.Size(270, 28); this.numGiaNhap.Maximum = 100000000;

            this.btnThem.BackColor = System.Drawing.Color.Teal;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Location = new System.Drawing.Point(24, 310);
            this.btnThem.Size = new System.Drawing.Size(270, 40);
            this.btnThem.Text = "+ Thêm vào danh sách";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.label6.AutoSize = true; this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(20, 380); this.label6.Text = "Tổng tiền nhập:";

            this.lblTongTien.AutoSize = true; this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Yellow;
            this.lblTongTien.Location = new System.Drawing.Point(20, 410); this.lblTongTien.Text = "0 VNĐ";

            this.btnLuu.BackColor = btnColor;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Location = new System.Drawing.Point(24, 470);
            this.btnLuu.Size = new System.Drawing.Size(270, 50);
            this.btnLuu.Text = "LƯU PHIẾU NHẬP";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            this.dgvChiTietNhap.BackgroundColor = backColor;
            this.dgvChiTietNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietNhap.Location = new System.Drawing.Point(320, 0);
            this.dgvChiTietNhap.Size = new System.Drawing.Size(680, 566);
            this.dgvChiTietNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // --- Tab Bao Cao ---
            this.tabBaoCao.BackColor = backColor;
            this.tabBaoCao.Controls.Add(this.dgvLichSu);
            this.tabBaoCao.Controls.Add(this.label7);
            this.tabBaoCao.Controls.Add(this.dgvCanhBao);
            this.tabBaoCao.Controls.Add(this.label5);
            this.tabBaoCao.ForeColor = textColor;
            this.tabBaoCao.Text = "Lịch sử & Cảnh báo";

            this.label5.AutoSize = true; this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(20, 20); this.label5.Text = "CẢNH BÁO TỒN KHO THẤP (< 5)";

            this.dgvCanhBao.BackgroundColor = panelColor;
            this.dgvCanhBao.Location = new System.Drawing.Point(24, 50);
            this.dgvCanhBao.Size = new System.Drawing.Size(940, 200);
            this.dgvCanhBao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.label7.AutoSize = true; this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Cyan;
            this.label7.Location = new System.Drawing.Point(20, 270); this.label7.Text = "LỊCH SỬ NHẬP HÀNG GẦN ĐÂY";

            this.dgvLichSu.BackgroundColor = panelColor;
            this.dgvLichSu.Location = new System.Drawing.Point(24, 300);
            this.dgvLichSu.Size = new System.Drawing.Size(940, 250);
            this.dgvLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // --- Form Main ---
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Nhập hàng - ABDDT Store";
            this.Load += new System.EventHandler(this.frmNhapHang_Load);

            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.tabNhapHang.ResumeLayout(false);
            this.tabBaoCao.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNhapHang;
        private System.Windows.Forms.TabPage tabBaoCao;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.ComboBox cboNhaCungCap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numGiaNhap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.DataGridView dgvChiTietNhap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvCanhBao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvLichSu;
    }
}