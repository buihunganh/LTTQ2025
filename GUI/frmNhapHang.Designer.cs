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
            this.dgvChiTietNhap = new System.Windows.Forms.DataGridView();
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
            this.tabBaoCao = new System.Windows.Forms.TabPage();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvCanhBao = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.tabTonKho = new System.Windows.Forms.TabPage();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabNhapHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).BeginInit();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.tabBaoCao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).BeginInit();
            this.tabTonKho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.SuspendLayout();
           
            this.tabControl1.Controls.Add(this.tabNhapHang);
            this.tabControl1.Controls.Add(this.tabBaoCao);
            this.tabControl1.Controls.Add(this.tabTonKho);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(750, 488);
            this.tabControl1.TabIndex = 0;
           
            this.tabNhapHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.tabNhapHang.Controls.Add(this.dgvChiTietNhap);
            this.tabNhapHang.Controls.Add(this.panelInput);
            this.tabNhapHang.Location = new System.Drawing.Point(4, 26);
            this.tabNhapHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabNhapHang.Name = "tabNhapHang";
            this.tabNhapHang.Size = new System.Drawing.Size(742, 458);
            this.tabNhapHang.TabIndex = 0;
            this.tabNhapHang.Text = "Nhập Hàng Mới";
            
            this.dgvChiTietNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietNhap.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.dgvChiTietNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietNhap.Location = new System.Drawing.Point(240, 0);
            this.dgvChiTietNhap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvChiTietNhap.Name = "dgvChiTietNhap";
            this.dgvChiTietNhap.RowHeadersWidth = 51;
            this.dgvChiTietNhap.Size = new System.Drawing.Size(502, 458);
            this.dgvChiTietNhap.TabIndex = 0;
            this.dgvChiTietNhap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietNhap_CellContentClick);
                        this.panelInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(75)))));
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
            this.panelInput.ForeColor = System.Drawing.Color.Gainsboro;
            this.panelInput.Location = new System.Drawing.Point(0, 0);
            this.panelInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(240, 458);
            this.panelInput.TabIndex = 1;
            
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(18, 382);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(202, 41);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "LƯU PHIẾU NHẬP";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Yellow;
            this.lblTongTien.Location = new System.Drawing.Point(15, 333);
            this.lblTongTien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(80, 30);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "0 VNĐ";
             
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(15, 309);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tổng tiền nhập:";
           
            this.btnThem.BackColor = System.Drawing.Color.Teal;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(18, 252);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(202, 32);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "+ Thêm vào danh sách";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
           
            this.numGiaNhap.Location = new System.Drawing.Point(18, 207);
            this.numGiaNhap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numGiaNhap.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaNhap.Name = "numGiaNhap";
            this.numGiaNhap.Size = new System.Drawing.Size(202, 25);
            this.numGiaNhap.TabIndex = 4;
            
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 187);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Giá nhập:";
            
            this.numSoLuong.Location = new System.Drawing.Point(18, 150);
            this.numSoLuong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(202, 25);
            this.numSoLuong.TabIndex = 6;
            
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Số lượng nhập:";
             
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.Location = new System.Drawing.Point(18, 93);
            this.cboSanPham.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(204, 25);
            this.cboSanPham.TabIndex = 8;
             
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Chọn Sản phẩm:";
             
            this.cboNhaCungCap.Location = new System.Drawing.Point(18, 37);
            this.cboNhaCungCap.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(204, 25);
            this.cboNhaCungCap.TabIndex = 10;
            
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nhà cung cấp:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            
            this.tabBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.tabBaoCao.Controls.Add(this.dgvLichSu);
            this.tabBaoCao.Controls.Add(this.label7);
            this.tabBaoCao.Controls.Add(this.dgvCanhBao);
            this.tabBaoCao.Controls.Add(this.label5);
            this.tabBaoCao.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabBaoCao.Location = new System.Drawing.Point(4, 26);
            this.tabBaoCao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabBaoCao.Name = "tabBaoCao";
            this.tabBaoCao.Size = new System.Drawing.Size(742, 458);
            this.tabBaoCao.TabIndex = 1;
            this.tabBaoCao.Text = "Lịch sử & Cảnh báo";
             
            this.dgvLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(75)))));
            this.dgvLichSu.ColumnHeadersHeight = 29;
            this.dgvLichSu.Location = new System.Drawing.Point(18, 244);
            this.dgvLichSu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.RowHeadersWidth = 51;
            this.dgvLichSu.Size = new System.Drawing.Size(705, 203);
            this.dgvLichSu.TabIndex = 0;
           
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Cyan;
            this.label7.Location = new System.Drawing.Point(15, 219);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(247, 21);
            this.label7.TabIndex = 1;
         
            this.dgvCanhBao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanhBao.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(75)))));
            this.dgvCanhBao.ColumnHeadersHeight = 29;
            this.dgvCanhBao.Location = new System.Drawing.Point(18, 41);
            this.dgvCanhBao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvCanhBao.Name = "dgvCanhBao";
            this.dgvCanhBao.RowHeadersWidth = 51;
            this.dgvCanhBao.Size = new System.Drawing.Size(705, 162);
            this.dgvCanhBao.TabIndex = 2;
           
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(15, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(255, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "CẢNH BÁO TỒN KHO THẤP (< 5)";
          
            this.tabTonKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(24)))), ((int)(((byte)(45)))));
            this.tabTonKho.Controls.Add(this.dgvTonKho);
            this.tabTonKho.Controls.Add(this.label8);
            this.tabTonKho.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabTonKho.Location = new System.Drawing.Point(4, 26);
            this.tabTonKho.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabTonKho.Name = "tabTonKho";
            this.tabTonKho.Size = new System.Drawing.Size(742, 458);
            this.tabTonKho.TabIndex = 2;
            this.tabTonKho.Text = "Tồn Kho";
           
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(46)))), ((int)(((byte)(75)))));
            this.dgvTonKho.ColumnHeadersHeight = 29;
            this.dgvTonKho.Location = new System.Drawing.Point(18, 41);
            this.dgvTonKho.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.ReadOnly = true;
            this.dgvTonKho.RowHeadersWidth = 51;
            this.dgvTonKho.Size = new System.Drawing.Size(705, 406);
            this.dgvTonKho.TabIndex = 0;
            
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(15, 16);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "DANH SÁCH TỒN KHO";
           
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 488);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Nhập hàng - ABDDT Store";
            this.Load += new System.EventHandler(this.frmNhapHang_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabNhapHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietNhap)).EndInit();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.tabBaoCao.ResumeLayout(false);
            this.tabBaoCao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).EndInit();
            this.tabTonKho.ResumeLayout(false);
            this.tabTonKho.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
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
        private System.Windows.Forms.TabPage tabTonKho;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Label label8;
    }
}