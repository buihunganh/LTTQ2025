using BTL_LTTQ.BLL;
using BTL_LTTQ.DAL;
using BTL_LTTQ.GUI;
using System;
using System.Windows.Forms;

namespace BTL_LTTQ
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bool isAdmin = PhienDangNhap.NhanVienHienTai.IsAdmin;
            string hoTen = PhienDangNhap.NhanVienHienTai.HoTen;

            lblUser.Text = $"Người dùng: {hoTen}";
            lblRole.Text = $"Vai trò: {(isAdmin ? "Quản trị" : "Nhân viên")}";

            if (isAdmin == false)
            {
                btnProduct.Visible = false;
                btnInventory.Visible = false;
                btnStaff.Visible = false;
                btnCustomer.Visible = false;
                btnInvoice.Visible = false;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            PhienDangNhap.DangXuat();

            frmLogin fLogin = new frmLogin();
            fLogin.Show();
            this.Close();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            frnNhanVien fNhanVien = new frnNhanVien();

            fNhanVien.Show();
        }
    }
}