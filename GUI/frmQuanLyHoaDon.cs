using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmQuanLyHoaDon : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private DataGridView dgvHoaDon;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtTenNV, txtTenKH;
        private Button btnTim;

        // Màu sắc Dark Mode
        private readonly Color COLOR_BG = Color.FromArgb(45, 47, 72);
        private readonly Color COLOR_PANEL = Color.FromArgb(58, 60, 92);
        private readonly Color COLOR_ACCENT = Color.FromArgb(232, 90, 79);
        private readonly Color COLOR_TEXT = Color.White;

        public frmQuanLyHoaDon()
        {
            InitializeComponent();
            SetupUI();
        }

        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _bll.FindInvoices(dtpFrom.Value, dtpTo.Value, txtTenNV.Text.Trim(), txtTenKH.Text.Trim());
                dgvHoaDon.DataSource = dt;

                // Ẩn cột ID, chỉ hiện Mã hiển thị
                if (dgvHoaDon.Columns.Contains("MaHD")) dgvHoaDon.Columns["MaHD"].Visible = false;

                // Format tiền
                if (dgvHoaDon.Columns.Contains("TongTien"))
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";

                // Đổi tên cột cho đẹp
                dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã HĐ";
                dgvHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvHoaDon.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
                dgvHoaDon.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- SỰ KIỆN TÌM KIẾM ---
        private void BtnTim_Click(object sender, EventArgs e) => LoadData();

        // --- SỰ KIỆN XEM CHI TIẾT (DOUBLE CLICK) ---
        private void DgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy ID hóa đơn từ dòng được chọn
                int maHD = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value);

                // Mở form chi tiết (Dùng constructor mới vừa thêm)
                frmHoaDon f = new frmHoaDon(maHD);
                f.ShowDialog();
            }
        }

        // --- VẼ GIAO DIỆN ---
        // --- VẼ GIAO DIỆN (ĐÃ CĂN CHỈNH LẠI VỊ TRÍ) ---
        private void SetupUI()
        {
            this.Text = "Quản lý hóa đơn";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = COLOR_BG;
            this.ForeColor = COLOR_TEXT;

            // 1. GroupBox chứa bộ lọc
            GroupBox grpFilter = new GroupBox { Text = "Bộ lọc tìm kiếm", Dock = DockStyle.Top, Height = 140, ForeColor = Color.Gainsboro, Padding = new Padding(10) };
            this.Controls.Add(grpFilter);

            // 2. SỬ DỤNG TABLE LAYOUT (Chia lưới tự động)
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 4; // 4 Cột
            tlp.RowCount = 3;    // 3 Dòng

            // Cấu hình tỉ lệ cột: (Label bé) - (Input to) - (Label bé) - (Input to)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Cột 1: Nhãn (100px)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Cột 2: Ô nhập (Giãn 50%)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F)); // Cột 3: Nhãn (100px)
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Cột 4: Ô nhập (Giãn 50%)

            grpFilter.Controls.Add(tlp);

            // --- DÒNG 1 ---
            // Từ ngày
            tlp.Controls.Add(CreateLabel("Từ ngày:"), 0, 0);
            dtpFrom = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Value = DateTime.Now.AddDays(-30) };
            tlp.Controls.Add(dtpFrom, 1, 0);

            // Đến ngày
            tlp.Controls.Add(CreateLabel("Đến ngày:"), 2, 0);
            dtpTo = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Value = DateTime.Now };
            tlp.Controls.Add(dtpTo, 3, 0);

            // --- DÒNG 2 ---
            // Nhân viên
            tlp.Controls.Add(CreateLabel("Nhân viên:"), 0, 1);
            txtTenNV = new TextBox { Dock = DockStyle.Fill };
            tlp.Controls.Add(txtTenNV, 1, 1);

            // Khách hàng
            tlp.Controls.Add(CreateLabel("Khách hàng:"), 2, 1);
            txtTenKH = new TextBox { Dock = DockStyle.Fill };
            tlp.Controls.Add(txtTenKH, 3, 1);

            // --- DÒNG 3: NÚT TÌM KIẾM (Chiếm trọn chiều ngang và căn giữa) ---
            btnTim = new Button
            {
                Text = "🔍 TÌM KIẾM HÓA ĐƠN",
                Size = new Size(250, 40),
                BackColor = COLOR_ACCENT,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.None // Căn giữa ô
            };
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.Click += BtnTim_Click;

            tlp.Controls.Add(btnTim, 0, 2);
            tlp.SetColumnSpan(btnTim, 4); // Gộp 4 cột làm 1 để nút nằm giữa

            // GridView (Phần dưới giữ nguyên)
            dgvHoaDon = new DataGridView { Dock = DockStyle.Fill, BackgroundColor = COLOR_BG, BorderStyle = BorderStyle.None, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, ReadOnly = true, RowTemplate = { Height = 35 } };
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHoaDon.DefaultCellStyle.BackColor = COLOR_PANEL;
            dgvHoaDon.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 82, 110);

            dgvHoaDon.CellDoubleClick += DgvHoaDon_CellDoubleClick;

            this.Controls.Add(dgvHoaDon);
            grpFilter.SendToBack();
        }

        private void frmQuanLyHoaDon_Load_1(object sender, EventArgs e)
        {

        }

        // Sửa lại hàm này một chút để trả về Label thay vì Add luôn
        private Label CreateLabel(string t)
        {
            return new Label
            {
                Text = t,
                Dock = DockStyle.Fill, // Tự căn lề
                TextAlign = ContentAlignment.MiddleLeft, // Căn chữ bên trái
                AutoSize = true,
                ForeColor = Color.Gainsboro
            };
        }
        // --- HÀM PHỤ TRỢ ĐỂ VẼ LABEL NHANH ---
        private void CreateLabel(Control p, string t, int x, int y)
        {
            p.Controls.Add(new Label
            {
                Text = t,
                Location = new Point(x, y),
                AutoSize = true,
                ForeColor = Color.Gainsboro // Màu chữ sáng cho nền tối
            });
        }
    }

}