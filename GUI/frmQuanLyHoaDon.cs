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
        private BTL_LTTQ.DTO.LoginResult _currentUser;
        private DataGridView dgvHoaDon;
        private DateTimePicker dtpFrom, dtpTo;
        private TextBox txtTenNV, txtTenKH;
        private ComboBox cboMaHD;
        private Button btnTim;

        // Màu sắc Dark Mode
        private readonly Color COLOR_BG = Color.FromArgb(45, 47, 72);
        private readonly Color COLOR_PANEL = Color.FromArgb(58, 60, 92);
        private readonly Color COLOR_ACCENT = Color.FromArgb(232, 90, 79);
        private readonly Color COLOR_TEXT = Color.White;

        public frmQuanLyHoaDon(BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
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

        private void BtnTim_Click(object sender, EventArgs e) => LoadData();

        private void DgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maHD = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value);
                frmHoaDon f = new frmHoaDon(maHD, _currentUser);
                f.ShowDialog();
            }
        }

        private void SetupUI()
        {
            this.Text = "Quản lý hóa đơn";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = COLOR_BG;
            this.ForeColor = COLOR_TEXT;

            GroupBox grpFilter = new GroupBox { Text = "Bộ lọc tìm kiếm", Dock = DockStyle.Top, Height = 180, ForeColor = Color.Gainsboro, Padding = new Padding(10) };
            this.Controls.Add(grpFilter);

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 4;
            tlp.RowCount = 4;

            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            grpFilter.Controls.Add(tlp);

            tlp.Controls.Add(CreateLabel("Từ ngày:"), 0, 0);
            dtpFrom = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Value = DateTime.Now.AddDays(-30) };
            tlp.Controls.Add(dtpFrom, 1, 0);

            tlp.Controls.Add(CreateLabel("Đến ngày:"), 2, 0);
            dtpTo = new DateTimePicker { Dock = DockStyle.Fill, Format = DateTimePickerFormat.Short, Value = DateTime.Now };
            tlp.Controls.Add(dtpTo, 3, 0);

            tlp.Controls.Add(CreateLabel("Nhân viên:"), 0, 1);
            txtTenNV = new TextBox { Dock = DockStyle.Fill };
            tlp.Controls.Add(txtTenNV, 1, 1);

            tlp.Controls.Add(CreateLabel("Khách hàng:"), 2, 1);
            txtTenKH = new TextBox { Dock = DockStyle.Fill };
            tlp.Controls.Add(txtTenKH, 3, 1);

            tlp.Controls.Add(CreateLabel("Mã HĐ:"), 0, 2);
            cboMaHD = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown, AutoCompleteMode = AutoCompleteMode.SuggestAppend, AutoCompleteSource = AutoCompleteSource.ListItems };
            cboMaHD.SelectedIndexChanged += (s, e) => {
                if (cboMaHD.SelectedValue != null)
                {
                    int maHD = Convert.ToInt32(cboMaHD.SelectedValue);
                    frmHoaDon f = new frmHoaDon(maHD, _currentUser);
                    f.ShowDialog();
                }
            };
            LoadDanhSachMaHoaDon();
            tlp.Controls.Add(cboMaHD, 1, 2);

            btnTim = new Button
            {
                Text = "🔍 TÌM KIẾM HÓA ĐƠN",
                Size = new Size(250, 40),
                BackColor = COLOR_ACCENT,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.None
            };
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.Click += BtnTim_Click;

            tlp.Controls.Add(btnTim, 0, 3);
            tlp.SetColumnSpan(btnTim, 4);
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

        private void LoadDanhSachMaHoaDon()
        {
            try
            {
                var dt = _bll.GetDanhSachMaHoaDon();
                cboMaHD.DataSource = dt;
                cboMaHD.DisplayMember = "MaHoaDon";
                cboMaHD.ValueMember = "MaHD";
            }
            catch { }
        }

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