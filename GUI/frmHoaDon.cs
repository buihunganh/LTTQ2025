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
        private DataTable _dtChiTiet;
        private int _idHoaDonVuaLuu = 0;

        // Controls
        private TextBox txtMaHD, txtNgayBan, txtNhanVien, txtDiaChi, txtSDT;
        private ComboBox cboKhachHang;
        private Label lblTongTien;
        private NumericUpDown numGiamGia;
        private Button btnThemKhach, btnLuu, btnIn, btnHuy;
        private DataGridView dgvChiTiet;

        public frmHoaDon(DataTable gioHangTuPOS)
        {
            InitializeComponent();

            // Tạo BindingSource để đảm bảo hiển thị dữ liệu ổn định
            _dtChiTiet = gioHangTuPOS.Copy();

            SetupFullUI();
            LoadInitData();
            CalculateTotal();
        }

        // Giữ nguyên hàm này
        private void frmHoaDon_Load(object sender, EventArgs e) { }

        private void LoadInitData()
        {
            txtMaHD.Text = "HD" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            txtNgayBan.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtNhanVien.Text = "Admin";

            try
            {
                cboKhachHang.DataSource = _bll.GetKhachHang();
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.SelectedIndex = -1;
            }
            catch { }

            // --- ÉP BUỘC HIỂN THỊ DỮ LIỆU LÊN LƯỚI ---
            dgvChiTiet.DataSource = null;
            dgvChiTiet.DataSource = _dtChiTiet;

            // Định dạng cột
            if (dgvChiTiet.Columns.Contains("MaCTSP")) dgvChiTiet.Columns["MaCTSP"].Visible = false;
            if (dgvChiTiet.Columns.Contains("TenSP")) dgvChiTiet.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
            if (dgvChiTiet.Columns.Contains("GiamGia")) dgvChiTiet.Columns["GiamGia"].HeaderText = "Giảm %";

            // Format tiền tệ
            DataGridViewCellStyle moneyStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            if (dgvChiTiet.Columns.Contains("DonGia")) dgvChiTiet.Columns["DonGia"].DefaultCellStyle = moneyStyle;
            if (dgvChiTiet.Columns.Contains("ThanhTien")) dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle = moneyStyle;
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

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null) { MessageBox.Show("Vui lòng chọn khách hàng!"); return; }
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
                    MessageBox.Show("Lưu hóa đơn thành công! Bạn có thể in ngay bây giờ.");
                    btnLuu.Enabled = false;
                    btnIn.Enabled = true;
                    cboKhachHang.Enabled = false;
                    numGiamGia.Enabled = false;
                    txtMaHD.Text = "HD" + _idHoaDonVuaLuu; // Cập nhật ID thật
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

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            // Nếu đã lưu thành công -> Trả về OK để POS xóa giỏ hàng
            this.DialogResult = (_idHoaDonVuaLuu > 0) ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        // --- VẼ GIAO DIỆN ---
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

            CreateLabel(grpInfo, "Mã HĐ:", 20, 30); txtMaHD = CreateTextBox(grpInfo, 100, 27, 200); txtMaHD.ReadOnly = true;
            CreateLabel(grpInfo, "Ngày:", 20, 70); txtNgayBan = CreateTextBox(grpInfo, 100, 67, 200); txtNgayBan.ReadOnly = true;
            CreateLabel(grpInfo, "NV:", 20, 110); txtNhanVien = CreateTextBox(grpInfo, 100, 107, 200); txtNhanVien.ReadOnly = true;

            CreateLabel(grpInfo, "Khách hàng:", 450, 30);
            cboKhachHang = new ComboBox { Location = new Point(550, 27), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cboKhachHang.SelectedIndexChanged += CboKhachHang_SelectedIndexChanged;
            grpInfo.Controls.Add(cboKhachHang);

            btnThemKhach = new Button { Text = "+", Location = new Point(760, 26), Size = new Size(30, 23), BackColor = Color.OrangeRed, FlatStyle = FlatStyle.Flat, ForeColor = Color.White };
            grpInfo.Controls.Add(btnThemKhach);

            CreateLabel(grpInfo, "SĐT:", 450, 70); txtSDT = CreateTextBox(grpInfo, 550, 67, 240);
            CreateLabel(grpInfo, "Địa chỉ:", 450, 110); txtDiaChi = CreateTextBox(grpInfo, 550, 107, 240);

            // --- CẤU HÌNH LƯỚI RÕ RÀNG HƠN ---
            dgvChiTiet = new DataGridView { Location = new Point(20, 240), Size = new Size(850, 300), BackgroundColor = Color.FromArgb(58, 60, 92) };
            dgvChiTiet.DefaultCellStyle.BackColor = Color.White; // Nền dòng màu trắng
            dgvChiTiet.DefaultCellStyle.ForeColor = Color.Black; // Chữ màu đen (cho dễ nhìn)
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.OrangeRed; // Header màu cam
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.EnableHeadersVisualStyles = false;
            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(dgvChiTiet);

            Label lblGiam = new Label { Text = "Giảm giá (%):", Location = new Point(20, 560), AutoSize = true, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(lblGiam);
            numGiamGia = new NumericUpDown { Location = new Point(120, 557), Width = 80, Font = new Font("Segoe UI", 10) };
            numGiamGia.ValueChanged += (s, e) => CalculateTotal();
            this.Controls.Add(numGiamGia);

            lblTongTien = new Label { Text = "0 VNĐ", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.Yellow, Location = new Point(600, 560), AutoSize = true };
            this.Controls.Add(lblTongTien);

            int btnY = 610;
            btnLuu = CreateButton("Lưu Hóa Đơn", 250, btnY, Color.Green);
            btnLuu.Click += BtnLuu_Click;
            btnIn = CreateButton("In Hóa Đơn", 420, btnY, Color.Orange);
            btnIn.Enabled = false;
            btnIn.Click += BtnIn_Click;
            btnHuy = CreateButton("Hủy / Đóng", 590, btnY, Color.Gray);
            btnHuy.Click += BtnHuy_Click;

            this.Controls.Add(btnLuu);
            this.Controls.Add(btnIn);
            this.Controls.Add(btnHuy);
        }

        private void CreateLabel(Control p, string t, int x, int y) { p.Controls.Add(new Label { Text = t, Location = new Point(x, y), AutoSize = true }); }
        private TextBox CreateTextBox(Control p, int x, int y, int w) { var t = new TextBox { Location = new Point(x, y), Width = w }; p.Controls.Add(t); return t; }
        private Button CreateButton(string t, int x, int y, Color c) { return new Button { Text = t, Location = new Point(x, y), Size = new Size(150, 40), BackColor = c, ForeColor = Color.White, FlatStyle = FlatStyle.Flat }; }
    }
}