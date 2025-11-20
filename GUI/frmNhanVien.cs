using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions; 
using System.IO;
using BTL_LTTQ.BLL;
using ClosedXML.Excel; 

namespace BTL_LTTQ.GUI
{
    public partial class frmNhanVien : Form
    {
        private NhanVienBLL bllNhanVien = new NhanVienBLL();

        public frmNhanVien()
        {
            InitializeComponent();
            ApplyDashboardTemplate();
            InitComboBoxFilter();
        }

        private void frnNhanVien_Load(object sender, EventArgs e)
        {
            if (cmbLocTrangThai.Items.Count > 0) cmbLocTrangThai.SelectedIndex = 0;


            LoadData();

            if (dtpNgayVaoLam != null)
            {
                dtpNgayVaoLam.Format = DateTimePickerFormat.Custom;
                dtpNgayVaoLam.CustomFormat = "dd/MM/yyyy";
            }
        }

        private void InitComboBoxFilter()
        {
            cmbLocTrangThai.Items.Clear();
            cmbLocTrangThai.Items.Add("Tất cả");       
            cmbLocTrangThai.Items.Add("Đang hoạt động"); 
            cmbLocTrangThai.Items.Add("Đã nghỉ việc");   

            cmbLocTrangThai.SelectedIndexChanged += CmbLocTrangThai_SelectedIndexChanged;
        }

        private void CmbLocTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void LoadData(string keyword = "")
        {

            int statusFilter = -1; 

            if (cmbLocTrangThai.SelectedIndex == 1) statusFilter = 1; 
            else if (cmbLocTrangThai.SelectedIndex == 2) statusFilter = 0; 

            dgvNhanVien.DataSource = bllNhanVien.FindNhanVien(keyword, statusFilter);

            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã";
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvNhanVien.Columns["TaiKhoan"].HeaderText = "Tài Khoản";
            dgvNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
            dgvNhanVien.Columns["Email"].HeaderText = "Email";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns["NgayVaoLam"].HeaderText = "Ngày Vào";
            dgvNhanVien.Columns["IsAdmin"].HeaderText = "Admin";
            dgvNhanVien.Columns["TrangThai"].HeaderText = "Hoạt động";

            if (dgvNhanVien.Columns.Contains("MatKhau")) dgvNhanVien.Columns["MatKhau"].Visible = false;
        }

        private void ApplyDashboardTemplate()
        {
            this.BackColor = Color.FromArgb(45, 47, 72);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;

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
            dgvNhanVien.ScrollBars = ScrollBars.Both;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (panelProductInfo != null)
            {
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
            }

            txtSearch.BackColor = Color.FromArgb(34, 37, 57);
            txtSearch.ForeColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            cmbLocTrangThai.BackColor = Color.FromArgb(34, 37, 57);
            cmbLocTrangThai.ForeColor = Color.White;
            cmbLocTrangThai.FlatStyle = FlatStyle.Flat;
            if (lblLoc != null) lblLoc.ForeColor = Color.Gainsboro;

            StyleButton(btnThem, false);
            StyleButton(btnLuu, false);
            StyleButton(btnXoa, true);
            StyleButton(btnLamMoi, false);
            StyleButton(btnXuatFile, false);

        }

        private void StyleButton(Button btn, bool isDelete)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Left; 

            if (isDelete) btn.BackColor = Color.FromArgb(232, 90, 79);
            else btn.BackColor = Color.FromArgb(58, 61, 90);
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
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Nhập họ tên!"); txtHoTen.Focus(); return false; }
            if (txtTaiKhoan.Enabled && string.IsNullOrWhiteSpace(txtTaiKhoan.Text)) { MessageBox.Show("Nhập tài khoản!"); txtTaiKhoan.Focus(); return false; }
            if (string.IsNullOrEmpty(txtMaNV.Text) && string.IsNullOrWhiteSpace(txtMatKhau.Text)) { MessageBox.Show("Nhập mật khẩu!"); txtMatKhau.Focus(); return false; }
            if (!string.IsNullOrWhiteSpace(txtSDT.Text) && !Regex.IsMatch(txtSDT.Text, @"^\d{10,11}$")) { MessageBox.Show("SĐT sai định dạng!"); txtSDT.Focus(); return false; }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(null, null);
            txtTaiKhoan.Enabled = true;
            txtHoTen.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                { 
                    bool ok = bllNhanVien.CreateNhanVien(txtHoTen.Text, txtTaiKhoan.Text, txtMatKhau.Text, chkIsAdmin.Checked, txtSDT.Text, txtEmail.Text, txtDiaChi.Text, dtpNgayVaoLam.Value);
                    if (ok) { MessageBox.Show("Thêm thành công!"); LoadData(txtSearch.Text); }
                }
                else
                { 
                    bool ok = bllNhanVien.EditNhanVien(Convert.ToInt32(txtMaNV.Text), txtHoTen.Text, chkIsAdmin.Checked, chkTrangThai.Checked, txtSDT.Text, txtEmail.Text, txtDiaChi.Text, dtpNgayVaoLam.Value);
                    if (ok) { MessageBox.Show("Sửa thành công!"); LoadData(txtSearch.Text); }
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
                    LoadData(txtSearch.Text);
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = ""; txtHoTen.Text = ""; txtTaiKhoan.Text = ""; txtMatKhau.Text = "";
            txtSDT.Text = ""; txtEmail.Text = ""; txtDiaChi.Text = "";
            dtpNgayVaoLam.Value = DateTime.Now;
            chkIsAdmin.Checked = false; chkTrangThai.Checked = true;
            txtTaiKhoan.Enabled = true;

            cmbLocTrangThai.SelectedIndex = 0;
            txtSearch.Text = "";
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Title = "Xuất Excel", Filter = "Excel (*.xlsx)|*.xlsx", FileName = "NhanVien_" + DateTime.Now.ToString("ddMMyy") };
            if (sfd.ShowDialog() == DialogResult.OK) ExportExcel(sfd.FileName);
        }

        private void ExportExcel(string filePath)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("NhanVien");
                for (int i = 0; i < dgvNhanVien.Columns.Count; i++)
                {
                    if (dgvNhanVien.Columns[i].Visible)
                    {
                        ws.Cell(1, i + 1).Value = dgvNhanVien.Columns[i].HeaderText;
                        ws.Cell(1, i + 1).Style.Font.Bold = true;
                        ws.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                    }
                }
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvNhanVien.Columns.Count; j++)
                    {
                        if (dgvNhanVien.Columns[j].Visible && dgvNhanVien.Rows[i].Cells[j].Value != null)
                        {
                            if (dgvNhanVien.Columns[j].Name == "NgayVaoLam") ws.Cell(i + 2, j + 1).Value = Convert.ToDateTime(dgvNhanVien.Rows[i].Cells[j].Value).ToString("dd/MM/yyyy");
                            else ws.Cell(i + 2, j + 1).Value = dgvNhanVien.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                ws.Columns().AdjustToContents();
                wb.SaveAs(filePath);
            }
            MessageBox.Show("Xuất file thành công!");
            System.Diagnostics.Process.Start(filePath);
        }
    }
}