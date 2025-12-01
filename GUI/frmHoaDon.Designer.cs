namespace BTL_LTTQ.GUI
{
    partial class frmHoaDon
    {
        
        private System.ComponentModel.IContainer components = null;

        
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

        
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.title = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblHangThanhVien = new System.Windows.Forms.Label();
            this.txtHangThanhVien = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.btnThemKhach = new System.Windows.Forms.Button();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.txtNhanVien = new System.Windows.Forms.TextBox();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblNgayBan = new System.Windows.Forms.Label();
            this.txtNgayBan = new System.Windows.Forms.TextBox();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnHuyHoaDon = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.lblTienKhachTra = new System.Windows.Forms.Label();
            this.txtTienKhachTra = new System.Windows.Forms.TextBox();
            this.lblTienThua = new System.Windows.Forms.Label();
            this.lblTienThuaValue = new System.Windows.Forms.Label();
            this.lblGiamGiaBill = new System.Windows.Forms.Label();
            this.txtGiamGiaBill = new System.Windows.Forms.TextBox();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
                        this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.OrangeRed;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(900, 60);
            this.title.TabIndex = 0;
            this.title.Text = "HÓA ĐƠN BÁN HÀNG";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblHangThanhVien);
            this.grpInfo.Controls.Add(this.txtHangThanhVien);
            this.grpInfo.Controls.Add(this.lblDiaChi);
            this.grpInfo.Controls.Add(this.txtDiaChi);
            this.grpInfo.Controls.Add(this.lblSDT);
            this.grpInfo.Controls.Add(this.txtSDT);
            this.grpInfo.Controls.Add(this.btnThemKhach);
            this.grpInfo.Controls.Add(this.lblKhachHang);
            this.grpInfo.Controls.Add(this.cboKhachHang);
            this.grpInfo.Controls.Add(this.lblMaKH);
            this.grpInfo.Controls.Add(this.txtMaKH);
            this.grpInfo.Controls.Add(this.lblNhanVien);
            this.grpInfo.Controls.Add(this.txtNhanVien);
            this.grpInfo.Controls.Add(this.lblMaNV);
            this.grpInfo.Controls.Add(this.txtMaNV);
            this.grpInfo.Controls.Add(this.lblNgayBan);
            this.grpInfo.Controls.Add(this.txtNgayBan);
            this.grpInfo.Controls.Add(this.lblMaHD);
            this.grpInfo.Controls.Add(this.txtMaHD);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInfo.Location = new System.Drawing.Point(20, 70);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(850, 245);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin chung";
            // 
            // lblHangThanhVien
            // 
            this.lblHangThanhVien.AutoSize = true;
            this.lblHangThanhVien.Location = new System.Drawing.Point(360, 175);
            this.lblHangThanhVien.Name = "lblHangThanhVien";
            this.lblHangThanhVien.Size = new System.Drawing.Size(55, 23);
            this.lblHangThanhVien.TabIndex = 17;
            this.lblHangThanhVien.Text = "Hạng:";
            // 
            // txtHangThanhVien
            // 
            this.txtHangThanhVien.Location = new System.Drawing.Point(480, 172);
            this.txtHangThanhVien.Name = "txtHangThanhVien";
            this.txtHangThanhVien.ReadOnly = true;
            this.txtHangThanhVien.Size = new System.Drawing.Size(290, 30);
            this.txtHangThanhVien.TabIndex = 18;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(360, 140);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(66, 23);
            this.lblDiaChi.TabIndex = 15;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(480, 137);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ReadOnly = true;
            this.txtDiaChi.Size = new System.Drawing.Size(290, 30);
            this.txtDiaChi.TabIndex = 16;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(360, 105);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(44, 23);
            this.lblSDT.TabIndex = 13;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(480, 102);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.ReadOnly = true;
            this.txtSDT.Size = new System.Drawing.Size(290, 30);
            this.txtSDT.TabIndex = 14;
            this.txtSDT.TextChanged += new System.EventHandler(this.TxtSDT_TextChanged);
            this.txtSDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSDT_KeyPress);
            // 
            // btnThemKhach
            // 
            this.btnThemKhach.BackColor = System.Drawing.Color.OrangeRed;
            this.btnThemKhach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemKhach.ForeColor = System.Drawing.Color.White;
            this.btnThemKhach.Location = new System.Drawing.Point(740, 66);
            this.btnThemKhach.Name = "btnThemKhach";
            this.btnThemKhach.Size = new System.Drawing.Size(30, 30);
            this.btnThemKhach.TabIndex = 12;
            this.btnThemKhach.Text = "+";
            this.btnThemKhach.UseVisualStyleBackColor = false;
            this.btnThemKhach.Click += new System.EventHandler(this.BtnThemKhach_Click);
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(360, 70);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(67, 23);
            this.lblKhachHang.TabIndex = 10;
            this.lblKhachHang.Text = "Tên KH:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboKhachHang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(480, 67);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(250, 31);
            this.cboKhachHang.TabIndex = 11;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.CboKhachHang_SelectedIndexChanged);
            // 
            // lblMaKH
            // 
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(360, 35);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(65, 23);
            this.lblMaKH.TabIndex = 8;
            this.lblMaKH.Text = "Mã KH:";
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(480, 32);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.ReadOnly = true;
            this.txtMaKH.Size = new System.Drawing.Size(290, 30);
            this.txtMaKH.TabIndex = 9;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(20, 140);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(69, 23);
            this.lblNhanVien.TabIndex = 6;
            this.lblNhanVien.Text = "Tên NV:";
            // 
            // txtNhanVien
            // 
            this.txtNhanVien.Location = new System.Drawing.Point(120, 137);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.ReadOnly = true;
            this.txtNhanVien.Size = new System.Drawing.Size(200, 30);
            this.txtNhanVien.TabIndex = 7;
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(20, 105);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(67, 23);
            this.lblMaNV.TabIndex = 4;
            this.lblMaNV.Text = "Mã NV:";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(120, 102);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.ReadOnly = true;
            this.txtMaNV.Size = new System.Drawing.Size(200, 30);
            this.txtMaNV.TabIndex = 5;
            // 
            // lblNgayBan
            // 
            this.lblNgayBan.AutoSize = true;
            this.lblNgayBan.Location = new System.Drawing.Point(20, 70);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(88, 23);
            this.lblNgayBan.TabIndex = 2;
            this.lblNgayBan.Text = "Ngày bán:";
            // 
            // txtNgayBan
            // 
            this.txtNgayBan.Location = new System.Drawing.Point(120, 67);
            this.txtNgayBan.Name = "txtNgayBan";
            this.txtNgayBan.ReadOnly = true;
            this.txtNgayBan.Size = new System.Drawing.Size(200, 30);
            this.txtNgayBan.TabIndex = 3;
            // 
            // lblMaHD
            // 
            this.lblMaHD.Location = new System.Drawing.Point(20, 35);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(95, 23);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Mã hóa đơn:";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(120, 32);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.ReadOnly = true;
            this.txtMaHD.Size = new System.Drawing.Size(200, 30);
            this.txtMaHD.TabIndex = 1;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChiTiet.EnableHeadersVisualStyles = false;
            this.dgvChiTiet.Location = new System.Drawing.Point(20, 335);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 24;
            this.dgvChiTiet.Size = new System.Drawing.Size(850, 240);
            this.dgvChiTiet.TabIndex = 2;
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.ForeColor = System.Drawing.Color.Yellow;
            this.lblTongTien.Location = new System.Drawing.Point(618, 585);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(109, 41);
            this.lblTongTien.TabIndex = 3;
            this.lblTongTien.Text = "0 VNĐ";
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.Green;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(150, 681);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(130, 40);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Lưu Hóa Đơn";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.Blue;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(286, 681);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(130, 40);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa Hóa Đơn";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Visible = false;
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnIn
            // 
            this.btnIn.BackColor = System.Drawing.Color.Orange;
            this.btnIn.Enabled = false;
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(422, 681);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(130, 40);
            this.btnIn.TabIndex = 6;
            this.btnIn.Text = "In Hóa Đơn";
            this.btnIn.UseVisualStyleBackColor = false;
            this.btnIn.Click += new System.EventHandler(this.BtnIn_Click);
            // 
            // btnHuyHoaDon
            // 
            this.btnHuyHoaDon.BackColor = System.Drawing.Color.Red;
            this.btnHuyHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnHuyHoaDon.Location = new System.Drawing.Point(558, 681);
            this.btnHuyHoaDon.Name = "btnHuyHoaDon";
            this.btnHuyHoaDon.Size = new System.Drawing.Size(130, 40);
            this.btnHuyHoaDon.TabIndex = 7;
            this.btnHuyHoaDon.Text = "Hủy Hóa Đơn";
            this.btnHuyHoaDon.UseVisualStyleBackColor = false;
            this.btnHuyHoaDon.Visible = false;
            this.btnHuyHoaDon.Click += new System.EventHandler(this.BtnHuyHoaDon_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.Gray;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(694, 681);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(130, 40);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.BtnDong_Click);
            // 
            // btnXoaSP
            // 
            this.btnXoaSP.BackColor = System.Drawing.Color.OrangeRed;
            this.btnXoaSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaSP.ForeColor = System.Drawing.Color.White;
            this.btnXoaSP.Location = new System.Drawing.Point(20, 579);
            this.btnXoaSP.Name = "btnXoaSP";
            this.btnXoaSP.Size = new System.Drawing.Size(150, 35);
            this.btnXoaSP.TabIndex = 20;
            this.btnXoaSP.Text = "Xóa Sản Phẩm";
            this.btnXoaSP.UseVisualStyleBackColor = false;
            this.btnXoaSP.Click += new System.EventHandler(this.BtnXoaSP_Click);
            // 
            // lblTienKhachTra
            // 
            this.lblTienKhachTra.AutoSize = true;
            this.lblTienKhachTra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienKhachTra.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTienKhachTra.Location = new System.Drawing.Point(200, 585);
            this.lblTienKhachTra.Name = "lblTienKhachTra";
            this.lblTienKhachTra.Size = new System.Drawing.Size(104, 20);
            this.lblTienKhachTra.TabIndex = 21;
            this.lblTienKhachTra.Text = "Tiền khách trả:";
            // 
            // txtTienKhachTra
            // 
            this.txtTienKhachTra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienKhachTra.ForeColor = System.Drawing.Color.Black;
            this.txtTienKhachTra.Location = new System.Drawing.Point(200, 605);
            this.txtTienKhachTra.Name = "txtTienKhachTra";
            this.txtTienKhachTra.Size = new System.Drawing.Size(150, 30);
            this.txtTienKhachTra.TabIndex = 22;
            this.txtTienKhachTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienKhachTra.TextChanged += new System.EventHandler(this.TxtTienKhachTra_TextChanged);
            this.txtTienKhachTra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTienKhachTra_KeyPress);
            // 
            // lblTienThua
            // 
            this.lblTienThua.AutoSize = true;
            this.lblTienThua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienThua.ForeColor = System.Drawing.Color.Lime;
            this.lblTienThua.Location = new System.Drawing.Point(370, 585);
            this.lblTienThua.Name = "lblTienThua";
            this.lblTienThua.Size = new System.Drawing.Size(74, 20);
            this.lblTienThua.TabIndex = 23;
            this.lblTienThua.Text = "Tiền thừa:";
            // 
            // lblTienThuaValue
            // 
            this.lblTienThuaValue.AutoSize = true;
            this.lblTienThuaValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienThuaValue.ForeColor = System.Drawing.Color.Lime;
            this.lblTienThuaValue.Location = new System.Drawing.Point(370, 605);
            this.lblTienThuaValue.Name = "lblTienThuaValue";
            this.lblTienThuaValue.Size = new System.Drawing.Size(70, 25);
            this.lblTienThuaValue.TabIndex = 24;
            this.lblTienThuaValue.Text = "0 VNĐ";
            // 
            // lblGiamGiaBill
            // 
            this.lblGiamGiaBill.AutoSize = true;
            this.lblGiamGiaBill.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiamGiaBill.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGiamGiaBill.Location = new System.Drawing.Point(582, 631);
            this.lblGiamGiaBill.Name = "lblGiamGiaBill";
            this.lblGiamGiaBill.Size = new System.Drawing.Size(97, 20);
            this.lblGiamGiaBill.TabIndex = 25;
            this.lblGiamGiaBill.Text = "Giảm giá bill:";
            // 
            // txtGiamGiaBill
            // 
            this.txtGiamGiaBill.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiamGiaBill.ForeColor = System.Drawing.Color.Black;
            this.txtGiamGiaBill.Location = new System.Drawing.Point(685, 626);
            this.txtGiamGiaBill.Name = "txtGiamGiaBill";
            this.txtGiamGiaBill.Size = new System.Drawing.Size(100, 30);
            this.txtGiamGiaBill.TabIndex = 26;
            this.txtGiamGiaBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiamGiaBill.TextChanged += new System.EventHandler(this.TxtGiamGiaBill_TextChanged);
            this.txtGiamGiaBill.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtGiamGiaBill_KeyPress);
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.ClientSize = new System.Drawing.Size(900, 800);
            this.Controls.Add(this.txtGiamGiaBill);
            this.Controls.Add(this.lblGiamGiaBill);
            this.Controls.Add(this.lblTienThuaValue);
            this.Controls.Add(this.lblTienThua);
            this.Controls.Add(this.txtTienKhachTra);
            this.Controls.Add(this.lblTienKhachTra);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnHuyHoaDon);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.btnXoaSP);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.title);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "frmHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HÓA ĐƠN BÁN HÀNG";
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.TextBox txtNgayBan;
        private System.Windows.Forms.Label lblNgayBan;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.TextBox txtNhanVien;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Button btnThemKhach;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnHuyHoaDon;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXoaSP;
        private System.Windows.Forms.Label lblTienKhachTra;
        private System.Windows.Forms.TextBox txtTienKhachTra;
        private System.Windows.Forms.Label lblTienThua;
        private System.Windows.Forms.Label lblTienThuaValue;
        private System.Windows.Forms.Label lblHangThanhVien;
        private System.Windows.Forms.TextBox txtHangThanhVien;
        private System.Windows.Forms.Label lblGiamGiaBill;
        private System.Windows.Forms.TextBox txtGiamGiaBill;
    }
}
