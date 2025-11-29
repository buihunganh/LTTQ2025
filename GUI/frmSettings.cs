using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BTL_LTTQ.BLL;
using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ
{
    public partial class frmSettings : Form
    {
        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        private const int SB_VERT = 0x1;

        private readonly LoginResult _currentUser;
        private EmployeeProfile _profile;
        private string _tempAvatarPath;
        private System.Windows.Forms.Timer _hideScrollbarTimer;

        private readonly string _projectRootPath;

        public frmSettings(LoginResult currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            _projectRootPath = GetProjectRootPath();
            LoadProfile();
            InitializeScrollbarHiding();
        }

        private void InitializeScrollbarHiding()
        {
            _hideScrollbarTimer = new System.Windows.Forms.Timer();
            _hideScrollbarTimer.Interval = 10;
            _hideScrollbarTimer.Tick += (s, e) => HideScrollbars();
            _hideScrollbarTimer.Start();

            panelProfileSection.Paint += (s, e) => HideScrollbars();
            panelPasswordSection.Paint += (s, e) => HideScrollbars();
        }

        private void HideScrollbars()
        {
            // Check if panels still exist and not disposed
            if (panelProfileSection != null && !panelProfileSection.IsDisposed && panelProfileSection.IsHandleCreated)
            {
                ShowScrollBar(panelProfileSection.Handle, SB_VERT, 0);
            }
            if (panelPasswordSection != null && !panelPasswordSection.IsDisposed && panelPasswordSection.IsHandleCreated)
            {
                ShowScrollBar(panelPasswordSection.Handle, SB_VERT, 0);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ShowScrollBar(panelProfileSection.Handle, SB_VERT, 0);
            ShowScrollBar(panelPasswordSection.Handle, SB_VERT, 0);
        }

        private void LoadProfile()
        {
            using (var dataProcesser = new DataProcesser())
            {
                _profile = dataProcesser.GetEmployeeProfile(_currentUser.EmployeeId);

                if (_profile != null)
                {
                    txtFullName.Text = _profile.FullName;
                    txtPhone.Text = _profile.Phone;
                    txtEmail.Text = _profile.Email;
                    txtAddress.Text = _profile.Address;
                    txtHireDate.Text = _profile.HireDate?.ToString("dd/MM/yyyy") ?? "Chưa có thông tin";

                    if (!string.IsNullOrEmpty(_profile.AvatarPath))
                    {
                        string fullPath = System.IO.Path.Combine(_projectRootPath, _profile.AvatarPath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            picAvatar.Image = System.Drawing.Image.FromFile(fullPath);
                        }
                    }
                }
            }
        }

        private void btnUploadAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn ảnh đại diện";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _tempAvatarPath = openFileDialog.FileName;
                    picAvatar.Image = System.Drawing.Image.FromFile(_tempAvatarPath);
                }
            }
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string avatarPathToSave = _profile.AvatarPath;

            if (!string.IsNullOrEmpty(_tempAvatarPath))
            {
                try
                {
                    string avatarFolder = System.IO.Path.Combine(_projectRootPath, "Resources", "Images", "Avatar");
                    if (!System.IO.Directory.Exists(avatarFolder))
                    {
                        System.IO.Directory.CreateDirectory(avatarFolder);
                    }

                    string extension = System.IO.Path.GetExtension(_tempAvatarPath);
                    string safeUserName = string.Join("", fullName.Split(System.IO.Path.GetInvalidFileNameChars()));
                    string fileName = $"avt{safeUserName}{extension}";
                    string destPath = System.IO.Path.Combine(avatarFolder, fileName);

                    System.IO.File.Copy(_tempAvatarPath, destPath, true);
                    avatarPathToSave = System.IO.Path.Combine("Resources", "Images", "Avatar", fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (var userService = new UserService())
            {
                bool success = userService.UpdateProfile(
                    _currentUser.EmployeeId,
                    fullName,
                    phone,
                    email,
                    address,
                    avatarPathToSave,
                    out string errorMessage
                );

                if (success)
                {
                    MessageBox.Show(
                        "Cập nhật thông tin thành công!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    _tempAvatarPath = null;
                    LoadProfile();
                }
                else
                {
                    MessageBox.Show(
                        errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            using (var userService = new UserService())
            {
                bool success = userService.UpdatePassword(
                    _currentUser.EmployeeId,
                    oldPassword,
                    newPassword,
                    confirmPassword,
                    out string errorMessage
                );

                if (success)
                {
                    MessageBox.Show(
                        "Đổi mật khẩu thành công!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    txtOldPassword.Clear();
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();
                    txtOldPassword.Focus();
                }
                else
                {
                    MessageBox.Show(
                        errorMessage,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void btnCancelPassword_Click(object sender, EventArgs e)
        {
            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private static string GetProjectRootPath()
        {
            string root = Application.StartupPath;

            try
            {
                // Move up two levels from bin\Debug (or Release) to project directory
                root = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.StartupPath, @"..\.."));
            }
            catch
            {
                // Ignore and fall back to StartupPath
            }

            return root;
        }
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}