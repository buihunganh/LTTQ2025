using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using BTL_LTTQ.BLL;
using Excel = Microsoft.Office.Interop.Excel;

namespace BTL_LTTQ.GUI
{
    public partial class frnNhanVien : Form
    {
        private NhanVienBLL bllNhanVien = new NhanVienBLL();

        public frnNhanVien()
        {
            InitializeComponent();
            ApplyDashboardTemplate();
            InitComboBoxFilter();
        }

        private void frnNhanVien_Load(object sender, EventArgs e)
        {
            InitComboBoxFilter();
            if (cmbLocTrangThai.Items.Count > 1)
            {
                cmbLocTrangThai.SelectedIndex = 1;
            }
            else
            {
                LoadData();
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
                int maNV = Convert.ToInt32(row.Cells["MaNV"].Value);

                txtMaNV.Text = maNV.ToString();
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

                try
                {
                    string matKhau = bllNhanVien.GetMatKhau(maNV);
                    txtMatKhau.Text = matKhau;
                    txtMatKhau.Enabled = true;
                    txtMatKhau.PasswordChar = '\0';
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể lấy mật khẩu: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Clear();
                    txtMatKhau.Enabled = true;
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (txtTaiKhoan.Enabled && string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtMaNV.Text) && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSDT.Text) && !Regex.IsMatch(txtSDT.Text, @"^\d{10,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!\nVui lòng nhập 10-11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email không hợp lệ!\nVui lòng nhập đúng định dạng email (ví dụ: example@email.com).",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                btnLamMoi_Click(null, null);

                txtTaiKhoan.Enabled = true;
                if (txtMatKhau != null)
                {
                    txtMatKhau.Enabled = true;
                    txtMatKhau.Clear();
                }

                chkTrangThai.Checked = true;

                txtHoTen.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuẩn bị form thêm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                {
                    bool ok = bllNhanVien.CreateNhanVien(
                        txtHoTen.Text.Trim(),
                        txtTaiKhoan.Text.Trim(),
                        txtMatKhau.Text,
                        chkIsAdmin.Checked,
                        txtSDT.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtDiaChi.Text.Trim(),
                        dtpNgayVaoLam.Value);

                    if (ok)
                    {
                        MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(txtSearch.Text);
                        btnLamMoi_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Thêm nhân viên thất bại!\n\nCó thể tài khoản đã tồn tại hoặc có lỗi xảy ra.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool ok = bllNhanVien.EditNhanVien(
                        Convert.ToInt32(txtMaNV.Text),
                        txtHoTen.Text.Trim(),
                        chkIsAdmin.Checked,
                        chkTrangThai.Checked,
                        txtSDT.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtDiaChi.Text.Trim(),
                        dtpNgayVaoLam.Value);

                    if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        try
                        {
                            bool okMatKhau = bllNhanVien.UpdateMatKhau(Convert.ToInt32(txtMaNV.Text), txtMatKhau.Text);
                            if (!okMatKhau)
                            {
                                MessageBox.Show("Cập nhật thông tin thành công nhưng không thể cập nhật mật khẩu!",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception exMatKhau)
                        {
                            MessageBox.Show($"Cập nhật thông tin thành công nhưng lỗi khi cập nhật mật khẩu: {exMatKhau.Message}",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    if (ok)
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(txtSearch.Text);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message ?? ex.ToString()}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{txtHoTen.Text}'?\n\nNhân viên này sẽ bị ẩn khỏi danh sách hoạt động.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (bllNhanVien.DeleteNhanVien(Convert.ToInt32(txtMaNV.Text)))
                    {
                        MessageBox.Show("Đã xóa nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (cmbLocTrangThai.SelectedIndex != 1)
                        {
                            cmbLocTrangThai.SelectedIndex = 1;
                        }
                        else
                        {
                            LoadData(txtSearch.Text);
                        }

                        btnLamMoi_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            txtSearch.Text = "";
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Title = "Xuất Excel", Filter = "Excel (*.xlsx)|*.xlsx", FileName = "NhanVien_" + DateTime.Now.ToString("ddMMyy") };
            if (sfd.ShowDialog() == DialogResult.OK) ExportExcel(sfd.FileName);
        }

        private void ExportExcel(string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "NhanVien";

                Excel.Range titleRange = worksheet.Range["A1", "I1"];
                titleRange.Merge();
                titleRange.Value2 = "DANH SÁCH NHÂN VIÊN";
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(titleRange);

                int headerRow = 3;
                int colIndex = 1;
                for (int i = 0; i < dgvNhanVien.Columns.Count; i++)
                {
                    if (dgvNhanVien.Columns[i].Visible)
                    {
                        worksheet.Cells[headerRow, colIndex] = dgvNhanVien.Columns[i].HeaderText;
                        colIndex++;
                    }
                }

                int totalVisibleCols = colIndex - 1;
                Excel.Range headerRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[headerRow, totalVisibleCols]];
                headerRange.Font.Bold = true;
                headerRange.Font.Size = 11;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(headerRange);

                int row = headerRow + 1;
                for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
                {
                    colIndex = 1;
                    for (int j = 0; j < dgvNhanVien.Columns.Count; j++)
                    {
                        if (dgvNhanVien.Columns[j].Visible && dgvNhanVien.Rows[i].Cells[j].Value != null && dgvNhanVien.Rows[i].Cells[j].Value != DBNull.Value)
                        {
                            if (dgvNhanVien.Columns[j].Name == "NgayVaoLam")
                            {
                                worksheet.Cells[row, colIndex] = Convert.ToDateTime(dgvNhanVien.Rows[i].Cells[j].Value).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                worksheet.Cells[row, colIndex] = dgvNhanVien.Rows[i].Cells[j].Value.ToString();
                            }
                            colIndex++;
                        }
                        else if (dgvNhanVien.Columns[j].Visible)
                        {
                            colIndex++;
                        }
                    }
                    row++;
                }

                if (row > headerRow + 1)
                {
                    Excel.Range dataRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[row - 1, totalVisibleCols]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);
                }

                worksheet.Columns.AutoFit();
                worksheet.UsedRange.WrapText = false;

                for (int i = 1; i <= totalVisibleCols; i++)
                {
                    Excel.Range col = (Excel.Range)worksheet.Columns[i];
                    col.ColumnWidth = Math.Max((double)col.ColumnWidth * 1.1, 12);
                    ReleaseObject(col);
                }

                workbook.SaveAs(filePath);

                MessageBox.Show("Xuất file thành công!");
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message);
            }
            finally
            {
                if (worksheet != null) ReleaseObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    ReleaseObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    ReleaseObject(excelApp);
                }
            }
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}