using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmBanHang : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private DataTable _dtGioHang;
        private Button btnThemKH; // Nút thêm khách hàng

        // --- Bảng màu giao diện Dark Mode ---
        private readonly Color COLOR_ROOT = Color.FromArgb(45, 47, 72);
        private readonly Color COLOR_CARD = Color.FromArgb(58, 60, 92);
        private readonly Color COLOR_ACCENT = Color.FromArgb(232, 90, 79);
        private readonly Color COLOR_TEXT_MAIN = Color.White;
        private readonly Color COLOR_TEXT_SUB = Color.Gainsboro;

        public frmBanHang()
        {
            InitializeComponent();
            InitGioHang();
        }

        private void InitGioHang()
        {
            _dtGioHang = new DataTable();
            _dtGioHang.Columns.Add("MaCTSP", typeof(int));
            _dtGioHang.Columns.Add("TenSP", typeof(string));
            _dtGioHang.Columns.Add("SoLuong", typeof(int));
            _dtGioHang.Columns.Add("DonGia", typeof(decimal));
            _dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

            dgvGioHang.DataSource = _dtGioHang;
            if (dgvGioHang.Columns["MaCTSP"] != null) dgvGioHang.Columns["MaCTSP"].Visible = false;
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            try
            {
                SetupTheme();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadData()
        {
            // Load Sản phẩm
            cboSanPham.DataSource = _bll.GetSanPhamBanHang();
            cboSanPham.DisplayMember = "TenHienThi";
            cboSanPham.ValueMember = "MaCTSP";

            // Load Khách hàng
            cboKhachHang.DataSource = _bll.GetKhachHang();
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "MaKH";
            cboKhachHang.SelectedIndex = -1; // Mặc định chưa chọn ai
        }

        // --- CẤU HÌNH GIAO DIỆN & NÚT THÊM KHÁCH ---
        private void SetupTheme()
        {
            this.BackColor = COLOR_ROOT;
            panelLeft.BackColor = COLOR_CARD;
            panelPayment.BackColor = COLOR_CARD;

            foreach (Control c in panelLeft.Controls) if (c is Label) c.ForeColor = COLOR_TEXT_SUB;
            foreach (Control c in panelPayment.Controls) if (c is Label) c.ForeColor = COLOR_TEXT_SUB;

            // Style Buttons
            btnThanhToan.BackColor = Color.Green;
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.FlatAppearance.BorderSize = 0;

            btnAdd.BackColor = COLOR_ACCENT;
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;

            // Style GridView
            dgvGioHang.BackgroundColor = Color.FromArgb(55, 57, 82);
            dgvGioHang.DefaultCellStyle.BackColor = Color.FromArgb(55, 57, 82);
            dgvGioHang.DefaultCellStyle.ForeColor = COLOR_TEXT_SUB;
            dgvGioHang.EnableHeadersVisualStyles = false;
            dgvGioHang.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT;
            dgvGioHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGioHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGioHang.BorderStyle = BorderStyle.None;

            // --- TẠO NÚT (+) THÊM KHÁCH HÀNG ---
            btnThemKH = new Button();
            btnThemKH.Text = "+";
            btnThemKH.Size = new Size(40, 28); // Chiều cao khớp với Combobox
            btnThemKH.Location = new Point(cboKhachHang.Location.X + cboKhachHang.Width + 5, cboKhachHang.Location.Y);
            btnThemKH.BackColor = COLOR_ACCENT;
            btnThemKH.ForeColor = Color.White;
            btnThemKH.FlatStyle = FlatStyle.Flat;
            btnThemKH.FlatAppearance.BorderSize = 0;
            btnThemKH.Click += BtnThemKH_Click;

            panelPayment.Controls.Add(btnThemKH);
        }

        // --- XỬ LÝ THÊM KHÁCH HÀNG NHANH ---
        private void BtnThemKH_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Text = "Thêm khách mới";
            f.Size = new Size(350, 220);
            f.StartPosition = FormStartPosition.CenterParent;
            f.BackColor = COLOR_CARD;
            f.ForeColor = COLOR_TEXT_MAIN;
            f.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            Label l1 = new Label() { Text = "Họ tên:", Location = new Point(20, 20), AutoSize = true };
            TextBox t1 = new TextBox() { Location = new Point(20, 45), Width = 280 };

            Label l2 = new Label() { Text = "Số điện thoại:", Location = new Point(20, 85), AutoSize = true };
            TextBox t2 = new TextBox() { Location = new Point(20, 110), Width = 280 };

            Button b = new Button() { Text = "LƯU KHÁCH HÀNG", Location = new Point(150, 145), Width = 150, Height = 35, DialogResult = DialogResult.OK, BackColor = COLOR_ACCENT, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            b.FlatAppearance.BorderSize = 0;

            f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
            f.AcceptButton = b;

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(t1.Text)) return;
                try
                {
                    int newID = _bll.QuickAddCustomer(t1.Text, t2.Text);
                    // Load lại và chọn ngay
                    cboKhachHang.DataSource = _bll.GetKhachHang();
                    cboKhachHang.SelectedValue = newID;
                    MessageBox.Show("Đã thêm: " + t1.Text);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // --- XỬ LÝ THÊM VÀO GIỎ ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;

            DataRowView drv = (DataRowView)cboSanPham.SelectedItem;
            int soLuongTon = Convert.ToInt32(drv["SoLuongTon"]);
            decimal giaBan = Convert.ToDecimal(drv["GiaBan"]);
            int soLuongMua = (int)numSoLuong.Value;

            if (soLuongMua > soLuongTon)
            {
                MessageBox.Show($"Kho chỉ còn {soLuongTon} sản phẩm!", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check trùng sản phẩm trong giỏ
            foreach (DataRow r in _dtGioHang.Rows)
            {
                if ((int)r["MaCTSP"] == (int)cboSanPham.SelectedValue)
                {
                    r["SoLuong"] = (int)r["SoLuong"] + soLuongMua;
                    r["ThanhTien"] = (int)r["SoLuong"] * giaBan;
                    CalculateTotals();
                    return;
                }
            }

            _dtGioHang.Rows.Add(cboSanPham.SelectedValue, cboSanPham.Text, soLuongMua, giaBan, soLuongMua * giaBan);
            CalculateTotals();
        }

        // --- TÍNH TOÁN TIỀN ---
        private void CalculateTotals()
        {
            decimal tongTienHang = 0;
            foreach (DataRow r in _dtGioHang.Rows) tongTienHang += Convert.ToDecimal(r["ThanhTien"]);

            decimal giamGiaPercent = numGiamGia.Value;
            decimal tienGiam = tongTienHang * (giamGiaPercent / 100);
            decimal phaiTra = tongTienHang - tienGiam;

            lblTongTien.Text = tongTienHang.ToString("N0") + " VNĐ";
            lblThanhToan.Text = phaiTra.ToString("N0") + " VNĐ";

            if (!string.IsNullOrEmpty(txtTienKhach.Text)) CalculateChange();
        }

        private void CalculateChange()
        {
            if (decimal.TryParse(txtTienKhach.Text, out decimal tienKhach))
            {
                decimal phaiTra = decimal.Parse(lblThanhToan.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));
                lblTienThua.Text = (tienKhach - phaiTra).ToString("N0") + " VNĐ";
            }
            else
            {
                lblTienThua.Text = "0 VNĐ";
            }
        }

        private void txtTienKhach_TextChanged(object sender, EventArgs e) => CalculateChange();
        private void SumTotal_Changed(object sender, EventArgs e) => CalculateTotals();

        // --- THANH TOÁN ---
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo");
                return;
            }

            // 1. Kiểm tra chọn khách hàng (FIX LỖI NULL)
            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước!", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            decimal tienKhach = 0;
            decimal phaiTra = decimal.Parse(lblThanhToan.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));

            if (!decimal.TryParse(txtTienKhach.Text, out tienKhach) || tienKhach < phaiTra)
            {
                MessageBox.Show("Tiền khách đưa không đủ!", "Thông báo");
                return;
            }

            try
            {
                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue); // Đã safe check ở trên
                int maNV = 1; // Lấy từ Session User (Tạm fix cứng)
                decimal tongTien = decimal.Parse(lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));
                decimal giamGia = tongTien - phaiTra;

                bool result = _bll.ThanhToan(maKH, maNV, tongTien, giamGia, phaiTra, _dtGioHang);
                if (result)
                {
                    MessageBox.Show("Thanh toán thành công! Đang in hóa đơn...", "Thành công");
                    // Reset form
                    _dtGioHang.Clear();
                    lblTongTien.Text = "0 VNĐ";
                    lblThanhToan.Text = "0 VNĐ";
                    txtTienKhach.Text = "";
                    lblTienThua.Text = "0 VNĐ";
                    cboSanPham.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }
    }
}