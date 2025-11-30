namespace BTL_LTTQ.GUI
{
    partial class frnNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelRoot = new System.Windows.Forms.Panel();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.lblLoc = new System.Windows.Forms.Label();
            this.cmbLocTrangThai = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.panelProductInfo = new System.Windows.Forms.Panel();
            this.chkIsAdmin = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();
            this.dtpNgayVaoLam = new System.Windows.Forms.DateTimePicker();
            this.lblNgayVaoLam = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelProductInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.AutoScroll = true;
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.panelRoot.Controls.Add(this.dgvNhanVien);
            this.panelRoot.Controls.Add(this.grpFilter);
            this.panelRoot.Controls.Add(this.panelButtons);
            this.panelRoot.Controls.Add(this.panelProductInfo);
            this.panelRoot.Controls.Add(this.lblTitle);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Margin = new System.Windows.Forms.Padding(2);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Padding = new System.Windows.Forms.Padding(18, 20, 18, 20);
            this.panelRoot.Size = new System.Drawing.Size(984, 585);
            this.panelRoot.TabIndex = 0;
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Location = new System.Drawing.Point(9, 334);
            this.dgvNhanVien.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.RowTemplate.Height = 28;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(964, 240);
            this.dgvNhanVien.TabIndex = 4;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);
            // 
            // grpFilter
            // 
            this.grpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFilter.BackColor = System.Drawing.Color.Transparent;
            this.grpFilter.Controls.Add(this.lblLoc);
            this.grpFilter.Controls.Add(this.cmbLocTrangThai);
            this.grpFilter.Controls.Add(this.txtSearch);
            this.grpFilter.Controls.Add(this.lblSearch);
            this.grpFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpFilter.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpFilter.Location = new System.Drawing.Point(9, 251);
            this.grpFilter.Margin = new System.Windows.Forms.Padding(2);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Padding = new System.Windows.Forms.Padding(2);
            this.grpFilter.Size = new System.Drawing.Size(802, 79);
            this.grpFilter.TabIndex = 3;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Lọc / Tìm kiếm";
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Location = new System.Drawing.Point(380, 22);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(63, 15);
            this.lblLoc.TabIndex = 3;
            this.lblLoc.Text = "Trạng thái:";
            // 
            // cmbLocTrangThai
            // 
            this.cmbLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocTrangThai.FormattingEnabled = true;
            this.cmbLocTrangThai.Location = new System.Drawing.Point(380, 42);
            this.cmbLocTrangThai.Name = "cmbLocTrangThai";
            this.cmbLocTrangThai.Size = new System.Drawing.Size(163, 23);
            this.cmbLocTrangThai.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(14, 42);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(306, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(14, 24);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(143, 15);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm (Tên hoặc SĐT):";
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnXuatFile);
            this.panelButtons.Controls.Add(this.btnLamMoi);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnLuu);
            this.panelButtons.Controls.Add(this.btnThem);
            this.panelButtons.Location = new System.Drawing.Point(816, 53);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(2);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(150, 495);
            this.panelButtons.TabIndex = 2;
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.btnXuatFile.FlatAppearance.BorderSize = 0;
            this.btnXuatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatFile.ForeColor = System.Drawing.Color.White;
            this.btnXuatFile.Location = new System.Drawing.Point(8, 211);
            this.btnXuatFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(134, 40);
            this.btnXuatFile.TabIndex = 4;
            this.btnXuatFile.Text = "⬇ Xuất Excel";
            this.btnXuatFile.UseVisualStyleBackColor = false;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(8, 168);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(134, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "⟳ Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(8, 125);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(134, 40);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "🗑 Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(8, 82);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(134, 40);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(8, 39);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(134, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // panelProductInfo
            // 
            this.panelProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProductInfo.BackColor = System.Drawing.Color.Transparent;
            this.panelProductInfo.Controls.Add(this.chkIsAdmin);
            this.panelProductInfo.Controls.Add(this.chkTrangThai);
            this.panelProductInfo.Controls.Add(this.dtpNgayVaoLam);
            this.panelProductInfo.Controls.Add(this.lblNgayVaoLam);
            this.panelProductInfo.Controls.Add(this.txtDiaChi);
            this.panelProductInfo.Controls.Add(this.lblDiaChi);
            this.panelProductInfo.Controls.Add(this.txtEmail);
            this.panelProductInfo.Controls.Add(this.lblEmail);
            this.panelProductInfo.Controls.Add(this.txtSDT);
            this.panelProductInfo.Controls.Add(this.lblSDT);
            this.panelProductInfo.Controls.Add(this.txtMatKhau);
            this.panelProductInfo.Controls.Add(this.lblMatKhau);
            this.panelProductInfo.Controls.Add(this.txtTaiKhoan);
            this.panelProductInfo.Controls.Add(this.lblTaiKhoan);
            this.panelProductInfo.Controls.Add(this.txtHoTen);
            this.panelProductInfo.Controls.Add(this.lblHoTen);
            this.panelProductInfo.Controls.Add(this.txtMaNV);
            this.panelProductInfo.Location = new System.Drawing.Point(18, 53);
            this.panelProductInfo.Margin = new System.Windows.Forms.Padding(2);
            this.panelProductInfo.Name = "panelProductInfo";
            this.panelProductInfo.Size = new System.Drawing.Size(780, 205);
            this.panelProductInfo.TabIndex = 1;
            // 
            // chkIsAdmin
            // 
            this.chkIsAdmin.AutoSize = true;
            this.chkIsAdmin.Location = new System.Drawing.Point(322, 179);
            this.chkIsAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.chkIsAdmin.Name = "chkIsAdmin";
            this.chkIsAdmin.Size = new System.Drawing.Size(116, 17);
            this.chkIsAdmin.TabIndex = 17;
            this.chkIsAdmin.Text = "Là Admin (Quản trị)";
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Location = new System.Drawing.Point(322, 146);
            this.chkTrangThai.Margin = new System.Windows.Forms.Padding(2);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(104, 17);
            this.chkTrangThai.TabIndex = 16;
            this.chkTrangThai.Text = "Đang hoạt động";
            // 
            // dtpNgayVaoLam
            // 
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(322, 68);
            this.dtpNgayVaoLam.Margin = new System.Windows.Forms.Padding(2);
            this.dtpNgayVaoLam.Name = "dtpNgayVaoLam";
            this.dtpNgayVaoLam.Size = new System.Drawing.Size(211, 20);
            this.dtpNgayVaoLam.TabIndex = 15;
            // 
            // lblNgayVaoLam
            // 
            this.lblNgayVaoLam.AutoSize = true;
            this.lblNgayVaoLam.Location = new System.Drawing.Point(322, 51);
            this.lblNgayVaoLam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNgayVaoLam.Name = "lblNgayVaoLam";
            this.lblNgayVaoLam.Size = new System.Drawing.Size(75, 13);
            this.lblNgayVaoLam.TabIndex = 14;
            this.lblNgayVaoLam.Text = "Ngày vào làm:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(322, 21);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(2);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(211, 20);
            this.txtDiaChi.TabIndex = 13;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(322, 4);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(43, 13);
            this.lblDiaChi.TabIndex = 12;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(15, 155);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(211, 20);
            this.txtEmail.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(15, 139);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(15, 110);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(2);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(211, 20);
            this.txtSDT.TabIndex = 7;
            this.txtSDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSDT_KeyPress);
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(15, 94);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(73, 13);
            this.lblSDT.TabIndex = 6;
            this.lblSDT.Text = "Số điện thoại:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(15, 68);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(2);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(211, 20);
            this.txtMatKhau.TabIndex = 5;
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(15, 51);
            this.lblMatKhau.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(55, 13);
            this.lblMatKhau.TabIndex = 4;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Location = new System.Drawing.Point(15, 21);
            this.txtTaiKhoan.Margin = new System.Windows.Forms.Padding(2);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(211, 20);
            this.txtTaiKhoan.TabIndex = 3;
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Location = new System.Drawing.Point(15, 4);
            this.lblTaiKhoan.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(58, 13);
            this.lblTaiKhoan.TabIndex = 2;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(322, 110);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(2);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(211, 20);
            this.txtHoTen.TabIndex = 21;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(322, 94);
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(57, 13);
            this.lblHoTen.TabIndex = 20;
            this.lblHoTen.Text = "Họ và tên:";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaNV.Location = new System.Drawing.Point(488, -4);
            this.txtMaNV.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(46, 20);
            this.txtMaNV.TabIndex = 99;
            this.txtMaNV.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 20);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 13);
            this.lblTitle.Size = new System.Drawing.Size(245, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý nhân viên";
            // 
            // frnNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(984, 585);
            this.Controls.Add(this.panelRoot);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frnNhanVien";
            this.Text = "Quản lý nhân viên";
            this.Load += new System.EventHandler(this.frnNhanVien_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelProductInfo.ResumeLayout(false);
            this.panelProductInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelProductInfo;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvNhanVien;

        // ĐẦY ĐỦ CÁC TRƯỜNG NHÂN VIÊN
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblNgayVaoLam;
        private System.Windows.Forms.DateTimePicker dtpNgayVaoLam;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.CheckBox chkIsAdmin;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.ComboBox cmbLocTrangThai;
    }
}