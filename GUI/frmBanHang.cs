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

        // --- Bảng màu (Dùng lại từ frmNhapHang) ---
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
            SetupTheme();

            cboSanPham.DataSource = _bll.GetSanPhamBanHang();
            cboSanPham.DisplayMember = "TenHienThi";
            cboSanPham.ValueMember = "MaCTSP";

            cboKhachHang.DataSource = _bll.GetKhachHang();
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "MaKH";
        }

        private void SetupTheme()
        {
            this.BackColor = COLOR_ROOT;
            panelLeft.BackColor = COLOR_CARD;
            panelPayment.BackColor = COLOR_CARD;

            foreach (Control c in panelLeft.Controls) if (c is Label) c.ForeColor = COLOR_TEXT_SUB;
            foreach (Control c in panelPayment.Controls) if (c is Label) c.ForeColor = COLOR_TEXT_SUB;

            btnThanhToan.BackColor = Color.Green;
            btnThanhToan.ForeColor = Color.White;
            btnAdd.BackColor = COLOR_ACCENT;
            btnAdd.ForeColor = Color.White;

            // Style Grid
            dgvGioHang.BackgroundColor = Color.FromArgb(55, 57, 82);
            dgvGioHang.DefaultCellStyle.BackColor = Color.FromArgb(55, 57, 82);
            dgvGioHang.DefaultCellStyle.ForeColor = COLOR_TEXT_SUB;
            dgvGioHang.EnableHeadersVisualStyles = false;
            dgvGioHang.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT;
            dgvGioHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;

            // Lấy thông tin sản phẩm đang chọn (Hack nhẹ: lấy từ DataRowView của ComboBox)
            DataRowView drv = (DataRowView)cboSanPham.SelectedItem;
            int soLuongTon = Convert.ToInt32(drv["SoLuongTon"]);
            decimal giaBan = Convert.ToDecimal(drv["GiaBan"]);
            int soLuongMua = (int)numSoLuong.Value;

            if (soLuongMua > soLuongTon)
            {
                MessageBox.Show($"Trong kho chỉ còn {soLuongTon} sản phẩm!", "Cảnh báo");
                return;
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ chưa
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

            // Nếu chưa có thì thêm mới
            _dtGioHang.Rows.Add(cboSanPham.SelectedValue, cboSanPham.Text, soLuongMua, giaBan, soLuongMua * giaBan);
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            decimal tongTienHang = 0;
            foreach (DataRow r in _dtGioHang.Rows) tongTienHang += Convert.ToDecimal(r["ThanhTien"]);

            decimal giamGiaPercent = numGiamGia.Value;
            decimal tienGiam = tongTienHang * (giamGiaPercent / 100);
            decimal phaiTra = tongTienHang - tienGiam;

            lblTongTien.Text = tongTienHang.ToString("N0") + " VNĐ";
            lblThanhToan.Text = phaiTra.ToString("N0") + " VNĐ";

            // Tính lại tiền thừa nếu đang nhập tiền khách
            if (!string.IsNullOrEmpty(txtTienKhach.Text)) CalculateChange();
        }

        private void CalculateChange()
        {
            if (decimal.TryParse(txtTienKhach.Text, out decimal tienKhach))
            {
                decimal phaiTra = decimal.Parse(lblThanhToan.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));
                lblTienThua.Text = (tienKhach - phaiTra).ToString("N0") + " VNĐ";
            }
        }

        private void txtTienKhach_TextChanged(object sender, EventArgs e) => CalculateChange();
        private void SumTotal_Changed(object sender, EventArgs e) => CalculateTotals();

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0) return;

            decimal tienKhach = 0;
            decimal phaiTra = decimal.Parse(lblThanhToan.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));

            if (!decimal.TryParse(txtTienKhach.Text, out tienKhach) || tienKhach < phaiTra)
            {
                MessageBox.Show("Khách chưa đưa đủ tiền!", "Thông báo");
                return;
            }

            try
            {
                int maKH = (int)cboKhachHang.SelectedValue;
                int maNV = 1; // Lấy từ Session User
                decimal tongTien = decimal.Parse(lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));
                decimal giamGia = tongTien - phaiTra;

                bool result = _bll.ThanhToan(maKH, maNV, tongTien, giamGia, phaiTra, _dtGioHang);
                if (result)
                {
                    MessageBox.Show("Thanh toán thành công! Đang in hóa đơn...", "Thành công");
                    _dtGioHang.Clear();
                    lblTongTien.Text = "0 VNĐ";
                    lblThanhToan.Text = "0 VNĐ";
                    txtTienKhach.Text = "";
                    lblTienThua.Text = "0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }
    }
}