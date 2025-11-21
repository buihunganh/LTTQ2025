using System;
using System.Data;
using System.Drawing; // Cần thư viện này để xử lý màu sắc
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmNhapHang : Form
    {
        private InventoryBLL _bll = new InventoryBLL();
        private DataTable _dtChiTietNhap;

        // --- BẢNG MÀU CHUẨN TỪ REPORT ---
        private readonly Color COLOR_ROOT = Color.FromArgb(45, 47, 72);       // Nền chính
        private readonly Color COLOR_CARD = Color.FromArgb(58, 60, 92);       // Nền panel con
        private readonly Color COLOR_ACCENT = Color.FromArgb(232, 90, 79);    // Màu cam chủ đạo
        private readonly Color COLOR_TEXT_MAIN = Color.White;                 // Chữ chính
        private readonly Color COLOR_TEXT_SUB = Color.Gainsboro;              // Chữ phụ
        private readonly Color COLOR_GRID_BG = Color.FromArgb(55, 57, 82);    // Nền lưới

        public frmNhapHang()
        {
            InitializeComponent();
            InitBangTam();
        }

        private void InitBangTam()
        {
            _dtChiTietNhap = new DataTable();
            _dtChiTietNhap.Columns.Add("MaCTSP", typeof(int));
            _dtChiTietNhap.Columns.Add("TenSP", typeof(string));
            _dtChiTietNhap.Columns.Add("SoLuong", typeof(int));
            _dtChiTietNhap.Columns.Add("GiaNhap", typeof(decimal));
            _dtChiTietNhap.Columns.Add("ThanhTien", typeof(decimal));

            dgvChiTietNhap.DataSource = _dtChiTietNhap;
            if (dgvChiTietNhap.Columns["MaCTSP"] != null)
                dgvChiTietNhap.Columns["MaCTSP"].Visible = false;
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Áp dụng giao diện đồng bộ
                SetupTheme();

                // 2. Load dữ liệu
                LoadComboBoxes();
                LoadDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- HÀM QUAN TRỌNG: STYLE GIAO DIỆN ---
        private void SetupTheme()
        {
            // 1. Nền Form & TabControl
            this.BackColor = COLOR_ROOT;
            tabControl1.BackColor = COLOR_ROOT;

            // TabPage: Vì Winform TabControl khó chỉnh màu header, ta chỉnh màu nền content
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.BackColor = COLOR_ROOT;
                tab.ForeColor = COLOR_TEXT_MAIN;
            }

            // 2. Panel Input (Trái)
            panelInput.BackColor = COLOR_CARD;
            panelInput.ForeColor = COLOR_TEXT_SUB;

            // 3. Các Labels
            foreach (Control c in panelInput.Controls)
            {
                if (c is Label) c.ForeColor = COLOR_TEXT_SUB;
            }
            // Riêng Label tổng tiền và tiêu đề cần nổi bật
            lblTongTien.ForeColor = Color.Yellow;
            label6.ForeColor = COLOR_TEXT_MAIN; // Label "Tổng tiền nhập"
            label1.ForeColor = COLOR_TEXT_MAIN;
            label2.ForeColor = COLOR_TEXT_MAIN;
            label3.ForeColor = COLOR_TEXT_MAIN;
            label4.ForeColor = COLOR_TEXT_MAIN;

            // 4. Buttons
            StyleButton(btnThem, Color.Teal); // Nút thêm giữ màu xanh Teal hoặc đổi sang Cam tùy bạn
            StyleButton(btnLuu, COLOR_ACCENT); // Nút Lưu màu Cam chuẩn

            // 5. DataGridView (Làm đẹp lưới)
            StyleDataGridView(dgvChiTietNhap);
            StyleDataGridView(dgvCanhBao);
            StyleDataGridView(dgvLichSu);
        }

        private void StyleButton(Button btn, Color backColor)
        {
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            // Cấu hình chung
            dgv.BackgroundColor = COLOR_GRID_BG;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(70, 72, 98); // Màu kẻ mờ

            // Header (Tiêu đề cột)
            dgv.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT; // Màu Cam
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6);
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Rows (Dòng dữ liệu)
            dgv.DefaultCellStyle.BackColor = COLOR_GRID_BG;
            dgv.DefaultCellStyle.ForeColor = COLOR_TEXT_SUB;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 82, 110); // Màu khi click chọn dòng
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.RowTemplate.Height = 35;

            // Tự động co giãn cột
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        // ---------------------------------------

        private void LoadComboBoxes()
        {
            // Load Nhà cung cấp từ database
            cboNhaCungCap.DataSource = _bll.GetNhaCungCap();
            cboNhaCungCap.DisplayMember = "TenNCC";
            cboNhaCungCap.ValueMember = "MaNCC";
            cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList; // Chỉ chọn từ danh sách

            // Load Sản phẩm từ database - chỉ lấy dữ liệu từ database
            cboSanPham.DataSource = _bll.GetSanPhamBienThe();
            cboSanPham.DisplayMember = "TenHienThi";
            cboSanPham.ValueMember = "MaCTSP";
            cboSanPham.DropDownStyle = ComboBoxStyle.DropDownList; // Chỉ chọn từ danh sách, không cho nhập
            cboSanPham.AutoCompleteMode = AutoCompleteMode.None; // Tắt autocomplete
            cboSanPham.AutoCompleteSource = AutoCompleteSource.None; // Tắt autocomplete
            
            // Khi chọn sản phẩm, tự động điền giá nhập từ database
            cboSanPham.SelectedIndexChanged += CboSanPham_SelectedIndexChanged;
        }

        private void CboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedItem != null && cboSanPham.SelectedValue != null)
            {
                try
                {
                    DataRowView drv = (DataRowView)cboSanPham.SelectedItem;
                    if (drv["GiaNhap"] != DBNull.Value)
                    {
                        numGiaNhap.Value = Convert.ToDecimal(drv["GiaNhap"]);
                    }
                }
                catch { }
            }
        }

        private void LoadDashboard()
        {
            dgvCanhBao.DataSource = _bll.GetCanhBaoTonKho();
            dgvLichSu.DataSource = _bll.GetLichSuNhap();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (numSoLuong.Value <= 0 || numGiaNhap.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng và giá lớn hơn 0", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow row = _dtChiTietNhap.NewRow();
            row["MaCTSP"] = cboSanPham.SelectedValue;
            row["TenSP"] = cboSanPham.Text;
            row["SoLuong"] = (int)numSoLuong.Value;
            row["GiaNhap"] = numGiaNhap.Value;
            row["ThanhTien"] = (int)numSoLuong.Value * numGiaNhap.Value;

            _dtChiTietNhap.Rows.Add(row);
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataRow r in _dtChiTietNhap.Rows)
            {
                tong += Convert.ToDecimal(r["ThanhTien"]);
            }
            lblTongTien.Text = tong.ToString("N0") + " VNĐ";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_dtChiTietNhap.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách nhập hàng đang trống!", "Thông báo");
                return;
            }

            try
            {
                int maNCC = (int)cboNhaCungCap.SelectedValue;
                decimal tongTien = decimal.Parse(lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "").Replace(".", ""));

                // Mã NV cứng, nếu có session thì thay vào
                int maNV = 1;

                bool ketQua = _bll.LuuPhieuNhap(maNCC, maNV, tongTien, _dtChiTietNhap);

                if (ketQua)
                {
                    MessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _dtChiTietNhap.Clear();
                    lblTongTien.Text = "0 VNĐ";
                    numSoLuong.Value = 0;
                    numGiaNhap.Value = 0;
                    LoadDashboard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvChiTietNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}