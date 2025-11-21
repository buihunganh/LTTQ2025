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

        // --- Bảng màu ---
        private readonly Color COLOR_ROOT = Color.FromArgb(45, 47, 72);
        private readonly Color COLOR_CARD = Color.FromArgb(58, 60, 92);
        private readonly Color COLOR_ACCENT = Color.FromArgb(232, 90, 79);
        private readonly Color COLOR_TEXT_SUB = Color.Gainsboro;

        // Controls khai báo thủ công
        private ComboBox cboSanPham;
        private NumericUpDown numSoLuong;
        private NumericUpDown numGiamGiaSP;
        private Button btnAdd;
        private DataGridView dgvGioHang;
        private Button btnGoToInvoice;

        public frmBanHang()
        {
            InitializeComponent();

            // --- THÊM DÒNG NÀY VÀO ĐỂ KÍCH HOẠT GIAO DIỆN ---
            this.Load += frmBanHang_Load;
            // -----------------------------------------------

            InitGioHang();
        }

        private void InitGioHang()
        {
            _dtGioHang = new DataTable();
            _dtGioHang.Columns.Add("MaCTSP", typeof(int));
            _dtGioHang.Columns.Add("TenSP", typeof(string));
            _dtGioHang.Columns.Add("SoLuong", typeof(int));
            _dtGioHang.Columns.Add("DonGia", typeof(decimal));
            _dtGioHang.Columns.Add("GiamGia", typeof(int));
            _dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

            if (dgvGioHang == null) dgvGioHang = new DataGridView();
            dgvGioHang.DataSource = _dtGioHang;
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            SetupUI_Simple(); // Giao diện rút gọn
            LoadData();
        }

        private void LoadData()
        {
            cboSanPham.DataSource = _bll.GetSanPhamBanHang();
            cboSanPham.DisplayMember = "TenHienThi";
            cboSanPham.ValueMember = "MaCTSP";
        }

        // --- GIAO DIỆN POS RÚT GỌN (CHỈ CÒN GIỎ HÀNG) ---
        private void SetupUI_Simple()
        {
            this.BackColor = COLOR_ROOT;

            // 1. PANEL TRÁI (Chọn hàng)
            Panel pnlLeft = new Panel { Dock = DockStyle.Left, Width = 350, BackColor = COLOR_CARD, Padding = new Padding(20) };
            this.Controls.Add(pnlLeft);

            CreateLabel(pnlLeft, "Chọn giày:", 20, 20);
            cboSanPham = new ComboBox { Location = new Point(20, 45), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlLeft.Controls.Add(cboSanPham);

            CreateLabel(pnlLeft, "Số lượng:", 20, 85);
            numSoLuong = new NumericUpDown { Location = new Point(20, 110), Width = 300, Minimum = 1, Value = 1 };
            pnlLeft.Controls.Add(numSoLuong);

            CreateLabel(pnlLeft, "Giảm giá (%):", 20, 150);
            numGiamGiaSP = new NumericUpDown { Location = new Point(20, 175), Width = 300, Maximum = 100, Minimum = 0 };
            pnlLeft.Controls.Add(numGiamGiaSP);

            btnAdd = new Button
            {
                Text = "🛒  THÊM VÀO GIỎ",
                Location = new Point(20, 230),
                Size = new Size(300, 55),
                BackColor = COLOR_ACCENT,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(6, 0, 6, 0),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatAppearance.MouseOverBackColor = ControlPaint.Light(COLOR_ACCENT, .15f);
            btnAdd.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(COLOR_ACCENT, .05f);
            btnAdd.Click += btnAdd_Click;
            pnlLeft.Controls.Add(btnAdd);

            // NÚT CHUYỂN TIẾP (TO VÀ NỔI BẬT)
            btnGoToInvoice = new Button
            {
                Text = "🧾  TẠO HÓA ĐƠN",
                Location = new Point(20, 305),
                Size = new Size(300, 65),
                BackColor = Color.ForestGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(6, 0, 6, 0)
            };
            btnGoToInvoice.FlatAppearance.BorderSize = 0;
            btnGoToInvoice.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.ForestGreen, .2f);
            btnGoToInvoice.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Color.ForestGreen, .1f);
            btnGoToInvoice.Click += btnGoToInvoice_Click;
            pnlLeft.Controls.Add(btnGoToInvoice);

            CreateLabel(pnlLeft, "* Kích đúp dòng để xóa", 20, 380, Color.Yellow);

            // 2. GRID GIỎ HÀNG (Chiếm toàn bộ bên phải)
            dgvGioHang = new DataGridView { Dock = DockStyle.Fill, BackgroundColor = Color.FromArgb(55, 57, 82), BorderStyle = BorderStyle.None, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false };
            dgvGioHang.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT;
            dgvGioHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvGioHang.DefaultCellStyle.BackColor = Color.FromArgb(55, 57, 82);
            dgvGioHang.DefaultCellStyle.ForeColor = COLOR_TEXT_SUB;
            dgvGioHang.CellDoubleClick += DgvGioHang_CellDoubleClick; // Sự kiện xóa

            this.Controls.Add(dgvGioHang);

            // Gán nguồn lại
            dgvGioHang.DataSource = _dtGioHang;
            if (dgvGioHang.Columns["MaCTSP"] != null) dgvGioHang.Columns["MaCTSP"].Visible = false;
        }

        private void CreateLabel(Control p, string t, int x, int y, Color? c = null)
        {
            p.Controls.Add(new Label { Text = t, Location = new Point(x, y), AutoSize = true, ForeColor = c ?? COLOR_TEXT_SUB });
        }

        // --- LOGIC THÊM HÀNG ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;

            DataRowView drv = (DataRowView)cboSanPham.SelectedItem;
            int tonKho = Convert.ToInt32(drv["SoLuongTon"]);
            decimal giaBan = Convert.ToDecimal(drv["GiaBan"]);
            int slMua = (int)numSoLuong.Value;
            int giamGia = (int)numGiamGiaSP.Value;

            if (slMua > tonKho) { MessageBox.Show($"Kho chỉ còn {tonKho}!"); return; }

            // Tính tiền từng món
            decimal tienGiam = (giaBan * slMua) * giamGia / 100;
            decimal thanhTien = (giaBan * slMua) - tienGiam;

            // Check trùng
            foreach (DataRow r in _dtGioHang.Rows)
            {
                if ((int)r["MaCTSP"] == (int)cboSanPham.SelectedValue)
                {
                    r["SoLuong"] = (int)r["SoLuong"] + slMua;
                    r["ThanhTien"] = (int)r["ThanhTien"] + thanhTien;
                    // Lưu ý: Cộng dồn tiền thì % giảm giá của lần sau phải tính lại cẩn thận nếu khác % lần đầu.
                    // Để đơn giản, ta cứ cộng dồn số lượng và tính lại tổng tiền theo giá mới nhất.
                    return;
                }
            }

            _dtGioHang.Rows.Add(cboSanPham.SelectedValue, cboSanPham.Text, slMua, giaBan, giamGia, thanhTien);

            // Reset nhập
            numSoLuong.Value = 1;
            numGiamGiaSP.Value = 0;
        }

        // --- SỰ KIỆN XÓA DÒNG ---
        private void DgvGioHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) _dtGioHang.Rows.RemoveAt(e.RowIndex);
        }

        // --- CHUYỂN SANG FORM HÓA ĐƠN ---
        private void btnGoToInvoice_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0) { MessageBox.Show("Giỏ hàng trống!"); return; }

            // Mở form Hóa Đơn và truyền Giỏ hàng sang
            frmHoaDon f = new frmHoaDon(_dtGioHang);
            f.ShowDialog();

            // Nếu bên kia Lưu thành công (DialogResult = OK) thì xóa giỏ hàng
            if (f.DialogResult == DialogResult.OK)
            {
                _dtGioHang.Rows.Clear();
                cboSanPham.SelectedIndex = -1;
                numSoLuong.Value = 1;
            }
        }

        private void frmBanHang_Load_1(object sender, EventArgs e)
        {

        }
    }
}