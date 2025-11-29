namespace BTL_LTTQ.GUI
{
    partial class frmKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.dgvKhachHang = new System.Windows.Forms.DataGridView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmbLocHang = new System.Windows.Forms.ComboBox();
            this.lblLocHang = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.btnLichSu = new System.Windows.Forms.Button();
            this.txtHang = new System.Windows.Forms.TextBox();
            this.lblHang = new System.Windows.Forms.Label();
            this.txtChiTieu = new System.Windows.Forms.TextBox();
            this.lblChiTieu = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.panelRoot.Controls.Add(this.dgvKhachHang);
            this.panelRoot.Controls.Add(this.grpFilter);
            this.panelRoot.Controls.Add(this.panelButtons);
            this.panelRoot.Controls.Add(this.panelInfo);
            this.panelRoot.Controls.Add(this.lblTitle);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.ForeColor = System.Drawing.Color.White;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Padding = new System.Windows.Forms.Padding(18, 20, 18, 20);
            this.panelRoot.Size = new System.Drawing.Size(975, 569);
            this.panelRoot.TabIndex = 0;
            // 
            // dgvKhachHang
            // 
            this.dgvKhachHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKhachHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhachHang.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKhachHang.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKhachHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvKhachHang.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(92)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKhachHang.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvKhachHang.EnableHeadersVisualStyles = false;
            this.dgvKhachHang.Location = new System.Drawing.Point(9, 366);
            this.dgvKhachHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvKhachHang.Name = "dgvKhachHang";
            this.dgvKhachHang.RowHeadersVisible = false;
            this.dgvKhachHang.RowHeadersWidth = 51;
            this.dgvKhachHang.RowTemplate.Height = 35;
            this.dgvKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhachHang.Size = new System.Drawing.Size(750, 179);
            this.dgvKhachHang.TabIndex = 4;
            this.dgvKhachHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);
            // 
            // grpFilter
            // 
            this.grpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFilter.Controls.Add(this.cmbLocHang);
            this.grpFilter.Controls.Add(this.lblLocHang);
            this.grpFilter.Controls.Add(this.txtSearch);
            this.grpFilter.Controls.Add(this.lblSearch);
            this.grpFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpFilter.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpFilter.Location = new System.Drawing.Point(9, 284);
            this.grpFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpFilter.Size = new System.Drawing.Size(750, 65);
            this.grpFilter.TabIndex = 3;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Lọc / Tìm kiếm";
            // 
            // cmbLocHang
            // 
            this.cmbLocHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.cmbLocHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLocHang.ForeColor = System.Drawing.Color.White;
            this.cmbLocHang.FormattingEnabled = true;
            this.cmbLocHang.Location = new System.Drawing.Point(375, 32);
            this.cmbLocHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbLocHang.Name = "cmbLocHang";
            this.cmbLocHang.Size = new System.Drawing.Size(151, 23);
            this.cmbLocHang.TabIndex = 3;
            this.cmbLocHang.SelectedIndexChanged += new System.EventHandler(this.cmbLocHang_SelectedIndexChanged);
            // 
            // lblLocHang
            // 
            this.lblLocHang.AutoSize = true;
            this.lblLocHang.Location = new System.Drawing.Point(375, 16);
            this.lblLocHang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocHang.Name = "lblLocHang";
            this.lblLocHang.Size = new System.Drawing.Size(98, 15);
            this.lblLocHang.TabIndex = 2;
            this.lblLocHang.Text = "Hạng thành viên:";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(15, 32);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(15, 16);
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
            this.panelButtons.Controls.Add(this.btnXuatFile);
            this.panelButtons.Controls.Add(this.btnLamMoi);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Controls.Add(this.btnLuu);
            this.panelButtons.Controls.Add(this.btnThem);
            this.panelButtons.Location = new System.Drawing.Point(788, 65);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(150, 488);
            this.panelButtons.TabIndex = 2;
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.btnXuatFile.FlatAppearance.BorderSize = 0;
            this.btnXuatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatFile.ForeColor = System.Drawing.Color.White;
            this.btnXuatFile.Location = new System.Drawing.Point(8, 179);
            this.btnXuatFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(135, 32);
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
            this.btnLamMoi.Location = new System.Drawing.Point(8, 138);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(135, 32);
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
            this.btnXoa.Location = new System.Drawing.Point(8, 98);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(135, 32);
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
            this.btnLuu.Location = new System.Drawing.Point(8, 57);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(135, 32);
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
            this.btnThem.Location = new System.Drawing.Point(8, 16);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(135, 32);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelInfo.Controls.Add(this.btnLichSu);
            this.panelInfo.Controls.Add(this.txtHang);
            this.panelInfo.Controls.Add(this.lblHang);
            this.panelInfo.Controls.Add(this.txtChiTieu);
            this.panelInfo.Controls.Add(this.lblChiTieu);
            this.panelInfo.Controls.Add(this.txtSDT);
            this.panelInfo.Controls.Add(this.lblSDT);
            this.panelInfo.Controls.Add(this.txtHoTen);
            this.panelInfo.Controls.Add(this.lblHoTen);
            this.panelInfo.Controls.Add(this.txtMaKH);
            this.panelInfo.Location = new System.Drawing.Point(15, 65);
            this.panelInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(750, 203);
            this.panelInfo.TabIndex = 1;
            // 
            // btnLichSu
            // 
            this.btnLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.btnLichSu.FlatAppearance.BorderSize = 0;
            this.btnLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLichSu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLichSu.ForeColor = System.Drawing.Color.White;
            this.btnLichSu.Location = new System.Drawing.Point(15, 146);
            this.btnLichSu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLichSu.Name = "btnLichSu";
            this.btnLichSu.Size = new System.Drawing.Size(188, 32);
            this.btnLichSu.TabIndex = 8;
            this.btnLichSu.Text = "Xem Lịch sử mua hàng";
            this.btnLichSu.UseVisualStyleBackColor = false;
            this.btnLichSu.Click += new System.EventHandler(this.btnLichSu_Click);
            // 
            // txtHang
            // 
            this.txtHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.txtHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHang.ForeColor = System.Drawing.Color.White;
            this.txtHang.Location = new System.Drawing.Point(262, 93);
            this.txtHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHang.Name = "txtHang";
            this.txtHang.ReadOnly = true;
            this.txtHang.Size = new System.Drawing.Size(226, 20);
            this.txtHang.TabIndex = 7;
            // 
            // lblHang
            // 
            this.lblHang.AutoSize = true;
            this.lblHang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHang.Location = new System.Drawing.Point(262, 73);
            this.lblHang.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHang.Name = "lblHang";
            this.lblHang.Size = new System.Drawing.Size(98, 15);
            this.lblHang.TabIndex = 6;
            this.lblHang.Text = "Hạng thành viên:";
            // 
            // txtChiTieu
            // 
            this.txtChiTieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.txtChiTieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChiTieu.ForeColor = System.Drawing.Color.White;
            this.txtChiTieu.Location = new System.Drawing.Point(15, 93);
            this.txtChiTieu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtChiTieu.Name = "txtChiTieu";
            this.txtChiTieu.ReadOnly = true;
            this.txtChiTieu.Size = new System.Drawing.Size(226, 20);
            this.txtChiTieu.TabIndex = 5;
            this.txtChiTieu.TextChanged += new System.EventHandler(this.txtChiTieu_TextChanged);
            // 
            // lblChiTieu
            // 
            this.lblChiTieu.AutoSize = true;
            this.lblChiTieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChiTieu.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblChiTieu.Location = new System.Drawing.Point(15, 73);
            this.lblChiTieu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChiTieu.Name = "lblChiTieu";
            this.lblChiTieu.Size = new System.Drawing.Size(115, 15);
            this.lblChiTieu.TabIndex = 4;
            this.lblChiTieu.Text = "Tổng chi tiêu (VND):";
            // 
            // txtSDT
            // 
            this.txtSDT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.ForeColor = System.Drawing.Color.White;
            this.txtSDT.Location = new System.Drawing.Point(262, 37);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(226, 20);
            this.txtSDT.TabIndex = 3;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSDT.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSDT.Location = new System.Drawing.Point(262, 16);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(79, 15);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "Số điện thoại:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(37)))), ((int)(((byte)(57)))));
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoTen.ForeColor = System.Drawing.Color.White;
            this.txtHoTen.Location = new System.Drawing.Point(15, 37);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(226, 20);
            this.txtHoTen.TabIndex = 1;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHoTen.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHoTen.Location = new System.Drawing.Point(15, 16);
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(94, 15);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Tên khách hàng:";
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(495, 37);
            this.txtMaKH.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(38, 20);
            this.txtMaKH.TabIndex = 9;
            this.txtMaKH.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 20);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 13);
            this.lblTitle.Size = new System.Drawing.Size(296, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý khách hàng";
            // 
            // frmKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(975, 569);
            this.Controls.Add(this.panelRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmKhachHang";
            this.Text = "Quản lý Khách hàng";
            this.Load += new System.EventHandler(this.frmKhachHang_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.DataGridView dgvKhachHang;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Button btnLichSu;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtChiTieu;
        private System.Windows.Forms.TextBox txtHang;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblChiTieu;
        private System.Windows.Forms.Label lblHang;
        private System.Windows.Forms.ComboBox cmbLocHang;
        private System.Windows.Forms.Label lblLocHang;
    }
}