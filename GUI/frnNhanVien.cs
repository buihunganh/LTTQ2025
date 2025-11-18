using System;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frnNhanVien : Form
    {
        private NhanVienBLL bllNhanVien = new NhanVienBLL();

        public frnNhanVien()
        {
            InitializeComponent();
            ApplyDashboardTemplate(); 
        }

        private void frnNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            if (dtpNgayVaoLam != null)
            {
                dtpNgayVaoLam.Format = DateTimePickerFormat.Custom;
                dtpNgayVaoLam.CustomFormat = "dd/MM/yyyy";
            }
        }

        // --- 1. HÀM TRANG TRÍ GIAO DIỆN (GIỮ NGUYÊN STYLE TEMPLATE) ---
        private void ApplyDashboardTemplate()
        {
            // Style Form
            this.BackColor = Color.FromArgb(45, 47, 72);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

            // Style DataGridView
            dgvNhanVien.BackgroundColor = Color.FromArgb(45, 47, 72);
            dgvNhanVien.BorderStyle = BorderStyle.None;
            dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(34, 37, 57);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvNhanVien.ColumnHeadersHeight = 40;

            dgvNhanVien.DefaultCellStyle.BackColor = Color.FromArgb(45, 47, 72);
            dgvNhanVien.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 92, 120);
            dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvNhanVien.RowTemplate.Height = 35;

            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.ScrollBars = ScrollBars.Both;

            // Style các ô nhập liệu (TextBox)
            foreach (Control c in panelProductInfo.Controls)
            {
                if (c is TextBox)
                {
                    c.BackColor = Color.FromArgb(34, 37, 57);
                    c.ForeColor = Color.White;
                    ((TextBox)c).BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is Label || c is CheckBox)
                {
                    c.ForeColor = Color.Gainsboro;
                }
            }
            // Style ô tìm kiếm riêng
            txtSearch.BackColor = Color.FromArgb(34, 37, 57);
            txtSearch.ForeColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            // Style Nút bấm
            StyleButton(btnThem, false);
            StyleButton(btnLuu, false);
            StyleButton(btnXoa, true);
            StyleButton(btnLamMoi, false);
        }

        private void StyleButton(Button btn, bool isDelete)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;

            // Neo nút bấm (theo yêu cầu của bạn)
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            if (isDelete)
                btn.BackColor = Color.FromArgb(232, 90, 79);
            else
                btn.BackColor = Color.FromArgb(58, 61, 90);
        }

        // --- 2. HÀM LOGIC (GIỮ ĐỦ CÁC TRƯỜNG DỮ LIỆU) ---

        private void LoadData(string keyword = "")
        {
            if (string.IsNullOrEmpty(keyword))
                dgvNhanVien.DataSource = bllNhanVien.GetNhanVienList();
            else
                dgvNhanVien.DataSource = bllNhanVien.FindNhanVien(keyword);

            // Hiển thị đầy đủ các cột
            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã";
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvNhanVien.Columns["TaiKhoan"].HeaderText = "Tài Khoản";
            dgvNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
            dgvNhanVien.Columns["Email"].HeaderText = "Email";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ"; // Thêm lại cột này
            dgvNhanVien.Columns["NgayVaoLam"].HeaderText = "Ngày Vào"; // Thêm lại cột này
            dgvNhanVien.Columns["IsAdmin"].HeaderText = "Admin";
            dgvNhanVien.Columns["TrangThai"].HeaderText = "Hoạt động";

            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn mật khẩu
            if (dgvNhanVien.Columns.Contains("MatKhau")) dgvNhanVien.Columns["MatKhau"].Visible = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                // Lấy dữ liệu từ bảng đổ về các ô (ĐỦ TRƯỜNG)
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();

                // Các trường mới
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";

                if (row.Cells["NgayVaoLam"].Value != DBNull.Value && row.Cells["NgayVaoLam"].Value != null)
                    dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);

                chkIsAdmin.Checked = Convert.ToBoolean(row.Cells["IsAdmin"].Value);
                chkTrangThai.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);

                txtTaiKhoan.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(null, null);
            txtTaiKhoan.Enabled = true;
            txtHoTen.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text)) // Thêm mới
                {
                    bool success = bllNhanVien.CreateNhanVien(
                        txtHoTen.Text,
                        txtTaiKhoan.Text,
                        txtMatKhau.Text,
                        chkIsAdmin.Checked,
                        txtSDT.Text,        // ĐỦ TRƯỜNG
                        txtEmail.Text,      // ĐỦ TRƯỜNG
                        txtDiaChi.Text,     // ĐỦ TRƯỜNG
                        dtpNgayVaoLam.Value // ĐỦ TRƯỜNG
                    );
                    if (success) { MessageBox.Show("Thêm thành công!"); LoadData(); }
                }
                else // Cập nhật
                {
                    bool success = bllNhanVien.EditNhanVien(
                        Convert.ToInt32(txtMaNV.Text),
                        txtHoTen.Text,
                        chkIsAdmin.Checked,
                        chkTrangThai.Checked,
                        txtSDT.Text,        // ĐỦ TRƯỜNG
                        txtEmail.Text,      // ĐỦ TRƯỜNG
                        txtDiaChi.Text,     // ĐỦ TRƯỜNG
                        dtpNgayVaoLam.Value // ĐỦ TRƯỜNG
                    );
                    if (success) { MessageBox.Show("Cập nhật thành công!"); LoadData(); }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text)) return;
            if (MessageBox.Show($"Vô hiệu hóa nhân viên '{txtHoTen.Text}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bllNhanVien.DeleteNhanVien(Convert.ToInt32(txtMaNV.Text)))
                {
                    MessageBox.Show("Đã vô hiệu hóa.");
                    LoadData();
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            dtpNgayVaoLam.Value = DateTime.Now;
            chkIsAdmin.Checked = false;
            chkTrangThai.Checked = true;
            txtTaiKhoan.Enabled = true;
            LoadData();
        }
    }
}