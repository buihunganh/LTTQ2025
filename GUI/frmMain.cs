using BTL_LTTQ.BLL;
using BTL_LTTQ.DAL;
using BTL_LTTQ.GUI;
using System;
using System.Drawing;
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
        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            currentFormChild = childForm;

            childForm.TopLevel = false; 
            childForm.FormBorderStyle = FormBorderStyle.None; 
            childForm.Dock = DockStyle.Fill; 
           
            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            PhienDangNhap.DangXuat();

            frmLogin fLogin = new frmLogin();
            fLogin.Show();
            this.Close();
        }

        private void SetActiveButton(Button btnActive)
        {

            Color defaultColor = Color.FromArgb(29, 32, 50);
            Color activeColor = Color.FromArgb(45, 47, 72);

            btnDashboard.BackColor = defaultColor;
            btnProduct.BackColor = defaultColor;
            btnInventory.BackColor = defaultColor;
            btnPos.BackColor = defaultColor;
            btnInvoice.BackColor = defaultColor;
            btnCustomer.BackColor = defaultColor;
            btnStaff.BackColor = defaultColor;
            btnReport.BackColor = defaultColor;

            if (btnActive != null)
            {
                btnActive.BackColor = activeColor;
            }
        }
        private void btnStaff_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnStaff);
            OpenChildForm(new frnNhanVien());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCustomer); 
            OpenChildForm(new frmKhachHang()); 
        }
    }
}