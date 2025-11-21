using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ
{
    public partial class frmLogin : Form
    {
        private DataProcesser _dataProcesser;

        public frmLogin()
        {
            InitializeComponent();
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
                EnsureDataProcesser();
                ToggleInputs(false);
                var loginResult = _dataProcesser.AuthenticateUser(username, password);
                if (loginResult == null)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.", "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                    return;
                }

                OpenMainForm(loginResult);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Không thể kết nối tới cơ sở dữ liệu.\nChi tiết: {ex.Message}",
                    "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void OpenMainForm(LoginResult loginResult)
        {
            var mainForm = new frmMain(loginResult);
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _dataProcesser?.Dispose();
            base.OnFormClosed(e);
        }

        private void EnsureDataProcesser()
        {
            if (_dataProcesser == null)
            {
                _dataProcesser = new DataProcesser();
            }
        }
    }
}
