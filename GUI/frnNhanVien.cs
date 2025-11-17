using System;
using System.Data;
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
        }

        private void frnNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvNhanVien.DataSource = bllNhanVien.GetNhanVienList();

            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvNhanVien.Columns["TaiKhoan"].HeaderText = "Tài Khoản";
            dgvNhanVien.Columns["IsAdmin"].HeaderText = "Là Admin";
            dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                txtMatKhau.Text = "";

                chkIsAdmin.Checked = (bool)row.Cells["IsAdmin"].Value;
                chkTrangThai.Checked = (bool)row.Cells["TrangThai"].Value;

                txtTaiKhoan.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            chkIsAdmin.Checked = false;
            chkTrangThai.Checked = true;

            txtTaiKhoan.Enabled = true;
            txtHoTen.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                {
                    bool success = bllNhanVien.CreateNhanVien(
                        txtHoTen.Text,
                        txtTaiKhoan.Text,
                        txtMatKhau.Text,
                        chkIsAdmin.Checked
                    );

                    if (success)
                    {
                        MessageBox.Show("Thêm nhân viên thành công!");
                        LoadData();
                    }
                }
                else
                {
                    bool success = bllNhanVien.EditNhanVien(
                        Convert.ToInt32(txtMaNV.Text),
                        txtHoTen.Text,
                        chkIsAdmin.Checked,
                        chkTrangThai.Checked
                    );

                    if (success)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn vô hiệu hóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int maNV = Convert.ToInt32(txtMaNV.Text);
                bool success = bllNhanVien.DeleteNhanVien(maNV);

                if (success)
                {
                    MessageBox.Show("Vô hiệu hóa nhân viên thành công.");
                    LoadData();
                    btnThem_Click(null, null);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}