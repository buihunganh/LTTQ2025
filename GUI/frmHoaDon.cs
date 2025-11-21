using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmHoaDon : Form
    {
        private SalesBLL _bll = new SalesBLL();

        // Biến toàn cục
        private string _maHDString = "";
        private string _ngayBan = "";
        private string _tenNV = "";
        private string _tenKH = "";
        private string _sdt = "";
        private string _diaChi = "";
        private decimal _tongTienSo = 0;

        private DataTable _dtChiTiet;
        private int _idHoaDonVuaLuu = 0;

        // Controls
        private TextBox txtMaHD, txtNgayBan, txtNhanVien, txtDiaChi, txtSDT;
        private ComboBox cboKhachHang;
        private Label lblTongTien;
        private NumericUpDown numGiamGia;
        private Button btnThemKhach, btnLuu, btnIn, btnHuy;
        private DataGridView dgvChiTiet;

        // =================================================================================
        // CONSTRUCTOR 1: DÙNG CHO POS (Lập hóa đơn mới)
        // =================================================================================
        public frmHoaDon(DataTable gioHangTuPOS)
        {
            InitializeComponent();
            _dtChiTiet = gioHangTuPOS.Copy();

            SetupFullUI(); // Vẽ giao diện

            // Điền dữ liệu mặc định cho đơn mới
            _maHDString = "HD" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _ngayBan = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            _tenNV = "Admin";

            LoadInitData(isViewOnly: false); // Chế độ nhập liệu
            CalculateTotal();
        }

        // =================================================================================
        // CONSTRUCTOR 2: DÙNG CHO QUẢN LÝ (Xem lại hóa đơn cũ)
        // =================================================================================
        public frmHoaDon(int maHD)
        {
            InitializeComponent();

            // Lấy dữ liệu từ SQL lên
            DataTable dtChung = _bll.GetInvoiceGeneral(maHD);
            DataTable dtChiTiet = _bll.GetInvoiceDetail(maHD);

            if (dtChung.Rows.Count > 0)
            {
                DataRow r = dtChung.Rows[0];
                _maHDString = r["MaHoaDon"].ToString();
                _ngayBan = Convert.ToDateTime(r["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                _tenNV = r["NhanVien"].ToString();
                _tenKH = r["KhachHang"].ToString();
                _sdt = r["SoDienThoai"].ToString();
                _diaChi = r["DiaChi"].ToString();
                _tongTienSo = r["ThanhToan"] != DBNull.Value ? Convert.ToDecimal(r["ThanhToan"]) : 0;
                _idHoaDonVuaLuu = maHD;
            }

            _dtChiTiet = dtChiTiet;

            SetupFullUI(); // Vẽ giao diện
            LoadInitData(isViewOnly: true); // Chế độ xem
        }

        private void frmHoaDon_Load(object sender, EventArgs e) { }

        // Hàm load dữ liệu chung
        private void LoadInitData(bool isViewOnly)
        {
            txtMaHD.Text = _maHDString;
            txtNgayBan.Text = _ngayBan;
            txtNhanVien.Text = _tenNV;

            try
            {
                cboKhachHang.DataSource = _bll.GetKhachHang();
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";

                if (isViewOnly)
                {
                    cboKhachHang.Text = _tenKH;
                    cboKhachHang.Enabled = false;
                    txtSDT.Text = _sdt;
                    txtDiaChi.Text = _diaChi;
                    lblTongTien.Text = _tongTienSo.ToString("N0") + " VNĐ";

                    btnLuu.Visible = false;
                    btnIn.Enabled = true;
                    btnThemKhach.Visible = false;
                    numGiamGia.Enabled = false;
                }
                else
                {
                    cboKhachHang.SelectedIndex = -1;
                }
            }
            catch { }

            dgvChiTiet.DataSource = _dtChiTiet;
            if (dgvChiTiet.Columns.Contains("MaCTSP")) dgvChiTiet.Columns["MaCTSP"].Visible = false;
            if (dgvChiTiet.Columns.Contains("DonGia")) dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvChiTiet.Columns.Contains("ThanhTien")) dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void CalculateTotal()
        {
            decimal tongTienHang = 0;
            foreach (DataRow r in _dtChiTiet.Rows) tongTienHang += Convert.ToDecimal(r["ThanhTien"]);

            decimal giamGia = tongTienHang * (numGiamGia.Value / 100);
            decimal thanhToan = tongTienHang - giamGia;

            lblTongTien.Text = thanhToan.ToString("N0") + " VNĐ";
        }

        private void CboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboKhachHang.SelectedItem;
                txtSDT.Text = drv["SoDienThoai"].ToString();
                txtDiaChi.Text = "Khách tại quầy";
            }
        }

        // --- LOGIC KHI BẤM NÚT (+) THÊM KHÁCH ---
        private void BtnThemKhach_Click(object sender, EventArgs e)
        {
            Form f = new Form { Text = "Thêm khách mới", Size = new Size(350, 220), StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(58, 60, 92), ForeColor = Color.White, FormBorderStyle = FormBorderStyle.FixedToolWindow };
            Label l1 = new Label { Text = "Họ tên:", Location = new Point(20, 20), AutoSize = true };
            TextBox t1 = new TextBox { Location = new Point(20, 45), Width = 280 };
            Label l2 = new Label { Text = "SĐT:", Location = new Point(20, 85), AutoSize = true };
            TextBox t2 = new TextBox { Location = new Point(20, 110), Width = 280 };
            Button b = new Button { Text = "LƯU", Location = new Point(150, 145), Width = 100, DialogResult = DialogResult.OK, BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
            f.AcceptButton = b;

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(t1.Text)) return;
                try
                {
                    int newID = _bll.QuickAddCustomer(t1.Text, t2.Text);
                    cboKhachHang.DataSource = _bll.GetKhachHang();
                    cboKhachHang.SelectedValue = newID;
                    MessageBox.Show("Đã thêm khách hàng: " + t1.Text);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedIndex == -1 || cboKhachHang.SelectedValue == null)
                if (_dtChiTiet.Rows.Count == 0) { MessageBox.Show("Không có hàng để lưu!"); return; }

            try
            {
                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                int maNV = 1;

                decimal tongHang = 0;
                foreach (DataRow r in _dtChiTiet.Rows) tongHang += Convert.ToDecimal(r["ThanhTien"]);
                decimal tienGiam = tongHang * (numGiamGia.Value / 100);
                decimal thanhToan = tongHang - tienGiam;

                _idHoaDonVuaLuu = _bll.ThanhToan(maKH, maNV, tongHang, tienGiam, thanhToan, _dtChiTiet);

                if (_idHoaDonVuaLuu > 0)
                {
                    MessageBox.Show("Lưu thành công! Mã HĐ: " + _idHoaDonVuaLuu);
                    btnLuu.Enabled = false;
                    btnIn.Enabled = true;
                    cboKhachHang.Enabled = false;
                    numGiamGia.Enabled = false;
                    txtMaHD.Text = "HD" + _idHoaDonVuaLuu;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (_idHoaDonVuaLuu == 0) { MessageBox.Show("Phải Lưu hóa đơn trước khi in!"); return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "HoaDon_" + txtMaHD.Text + ".xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string content = "<html><head><meta charset='utf-8'></head><body>";
                content += $"<h2 style='text-align:center'>HÓA ĐƠN BÁN HÀNG</h2>";
                content += $"<p>Mã HĐ: {txtMaHD.Text} - Ngày: {txtNgayBan.Text}</p>";
                content += $"<p>Khách hàng: {cboKhachHang.Text} - SĐT: {txtSDT.Text}</p>";
                content += "<table border='1' style='border-collapse:collapse; width:100%'>";
                content += "<tr style='background-color:#ccc'><th>Tên Hàng</th><th>SL</th><th>Đơn Giá</th><th>Thành Tiền</th></tr>";

                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    content += "<tr>";
                    content += $"<td>{row.Cells["TenSP"].Value}</td>";
                    content += $"<td>{row.Cells["SoLuong"].Value}</td>";
                    content += $"<td>{Convert.ToDecimal(row.Cells["DonGia"].Value):N0}</td>";
                    content += $"<td>{Convert.ToDecimal(row.Cells["ThanhTien"].Value):N0}</td>";
                    content += "</tr>";
                }
                content += "</table>";
                content += $"<h3 style='text-align:right; color:red'>TỔNG THANH TOÁN: {lblTongTien.Text}</h3>";
                content += "</body></html>";

                System.IO.File.WriteAllText(sfd.FileName, content);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void SetupFullUI()
        {
            this.Text = "HÓA ĐƠN BÁN HÀNG";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 47, 72);
            this.ForeColor = Color.White;

            Label title = new Label { Text = "HÓA ĐƠN BÁN HÀNG", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.OrangeRed, Dock = DockStyle.Top, Height = 60, TextAlign = ContentAlignment.MiddleCenter };
            this.Controls.Add(title);

            GroupBox grpInfo = new GroupBox { Text = "Thông tin chung", Location = new Point(20, 70), Size = new Size(850, 150), ForeColor = Color.Gainsboro, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(grpInfo);

            CreateLabel(grpInfo, "Mã hóa đơn:", 20, 30); txtMaHD = CreateTextBox(grpInfo, 110, 27, 200); txtMaHD.ReadOnly = true;
            CreateLabel(grpInfo, "Ngày bán:", 20, 70); txtNgayBan = CreateTextBox(grpInfo, 110, 67, 200); txtNgayBan.ReadOnly = true;
            CreateLabel(grpInfo, "Nhân viên:", 20, 110); txtNhanVien = CreateTextBox(grpInfo, 110, 107, 200); txtNhanVien.ReadOnly = true;

            CreateLabel(grpInfo, "Khách hàng:", 450, 30);
            cboKhachHang = new ComboBox
            {
                Location = new Point(550, 27),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDown,
        
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
         
                AutoCompleteSource = AutoCompleteSource.ListItems
            };
   

            cboKhachHang.SelectedIndexChanged += CboKhachHang_SelectedIndexChanged;
            grpInfo.Controls.Add(cboKhachHang);

            btnThemKhach = new Button { Text = "+", Location = new Point(760, 26), Size = new Size(30, 23), BackColor = Color.OrangeRed, FlatStyle = FlatStyle.Flat, ForeColor = Color.White };
            btnThemKhach.Click += BtnThemKhach_Click; // Gắn sự kiện Click cho nút
            grpInfo.Controls.Add(btnThemKhach);

            CreateLabel(grpInfo, "SĐT:", 450, 70); txtSDT = CreateTextBox(grpInfo, 550, 67, 240);
            CreateLabel(grpInfo, "Địa chỉ:", 450, 110); txtDiaChi = CreateTextBox(grpInfo, 550, 107, 240);

            dgvChiTiet = new DataGridView { Location = new Point(20, 240), Size = new Size(850, 300), BackgroundColor = Color.FromArgb(58, 60, 92), BorderStyle = BorderStyle.None, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, ReadOnly = true };
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 90, 79);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvChiTiet.DefaultCellStyle.BackColor = Color.FromArgb(45, 47, 72);
            dgvChiTiet.DefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.EnableHeadersVisualStyles = false;
            this.Controls.Add(dgvChiTiet);

            Label lblGiam = new Label { Text = "Giảm giá (%):", Location = new Point(20, 560), AutoSize = true, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(lblGiam);
            numGiamGia = new NumericUpDown { Location = new Point(120, 557), Width = 80, Font = new Font("Segoe UI", 10) };
            numGiamGia.ValueChanged += (s, e) => CalculateTotal();
            this.Controls.Add(numGiamGia);

            lblTongTien = new Label { Text = "0 VNĐ", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.Yellow, Location = new Point(600, 560), AutoSize = true };
            this.Controls.Add(lblTongTien);

            int btnY = 610;
            btnLuu = new Button { Text = "Lưu Hóa Đơn", Location = new Point(250, btnY), Size = new Size(150, 40), BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnLuu.Click += BtnLuu_Click;

            btnIn = new Button { Text = "In Hóa Đơn", Location = new Point(420, btnY), Size = new Size(150, 40), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };
            btnIn.Click += BtnIn_Click;

            btnHuy = new Button { Text = "Hủy / Đóng", Location = new Point(590, btnY), Size = new Size(150, 40), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnHuy.Click += (s, e) => { this.DialogResult = (_idHoaDonVuaLuu > 0) ? DialogResult.OK : DialogResult.Cancel; this.Close(); };

            this.Controls.Add(btnLuu);
            this.Controls.Add(btnIn);
            this.Controls.Add(btnHuy);
        }

        private void CreateLabel(Control p, string t, int x, int y) { p.Controls.Add(new Label { Text = t, Location = new Point(x, y), AutoSize = true }); }
        private TextBox CreateTextBox(Control p, int x, int y, int w) { var t = new TextBox { Location = new Point(x, y), Width = w }; p.Controls.Add(t); return t; }
    }
}