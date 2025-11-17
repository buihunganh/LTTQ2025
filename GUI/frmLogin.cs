using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using BTL_LTTQ.DAL;
using BTL_LTTQ.BLL; 
using BTL_LTTQ.DTO; 

namespace BTL_LTTQ
{
    public partial class frmLogin : Form
    {

        private NhanVienBLL bllNhanVien = new NhanVienBLL();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim(); 
            var password = txtPassword.Text; 

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus(); 
                return;
            }

            try
            {
                ToggleInputs(false); 



                if (bllNhanVien.KiemTraDangNhap(username, password))
                {
                    OpenMainForm();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.SelectAll(); 
                    txtPassword.Focus(); 
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn.\nChi tiết: {ex.Message}",
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleInputs(true); 
            }
        }


        private void OpenMainForm()
        {
 
            var mainForm = new frmMain();

            mainForm.FormClosed += (s, args) =>
            {
                Show();
                ResetInput();
            };

            Hide();
            mainForm.Show();
        }

        private void ResetInput()
        {
            txtUsername.Focus(); 
            if (!chkRemember.Checked)
            {
                txtUsername.Clear(); 
            }

            txtPassword.Clear(); 
        }

        private void ToggleInputs(bool enabled)
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            btnLogin.Enabled = enabled; 
            btnCancel.Enabled = enabled; 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}