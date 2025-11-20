using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ
{
    public partial class frmMain : Form
    {
        private readonly LoginResult _currentUser;
        private readonly Dictionary<Button, ButtonAppearance> _menuButtonStyles = new Dictionary<Button, ButtonAppearance>();
        private Form _activeContentForm;
        private Button _activeMenuButton;
        private readonly ReportService _reportService;

        public frmMain() : this(null)
        {
        }

        public frmMain(LoginResult user)
        {
            InitializeComponent();
            _currentUser = user;
            ApplyUserContext();
            ConfigureMenuButtons();
            _reportService = IsInDesignMode() ? null : new ReportService();
        }


        private void ApplyUserContext()
        {
            var displayName = _currentUser?.FullName ?? "Không xác định";
            var roleLabel = _currentUser == null
                ? "Vai trò: ---"
                : $"Vai trò: {(_currentUser.IsAdmin ? "Quản trị" : "Nhân viên")}";

            lblUser.Text = $"Người dùng: {displayName}";
            lblRole.Text = roleLabel;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnDashboard);
            ShowHomeContent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnReport);
            ShowContentForm(new frmReport());
        }

        private void HighlightOnlyMenuButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                SetActiveMenuButton(button);
            }
        }

        /// <summary>
        /// Hiển thị một form con trong panelContent
        /// </summary>
        private void ShowContentForm(Form contentForm)
        {
            // Đóng form hiện tại nếu có
            if (_activeContentForm != null)
            {
                _activeContentForm.Close();
                _activeContentForm.Dispose();
            }

            // Cấu hình form mới như một child form
            _activeContentForm = contentForm;
            contentForm.TopLevel = false;
            contentForm.FormBorderStyle = FormBorderStyle.None;
            contentForm.Dock = DockStyle.Fill;

            // Thêm form vào panelContent
            panelContent.Controls.Clear();
            panelContent.Controls.Add(contentForm);
            contentForm.Show();
        }

        /// <summary>
        /// Hiển thị nội dung Dashboard (trang chủ)
        /// </summary>
        private void ShowHomeContent()
        {
            // Đóng form con hiện tại nếu có
            if (_activeContentForm != null)
            {
                _activeContentForm.Close();
                _activeContentForm.Dispose();
                _activeContentForm = null;
            }

            // Hiển thị các control của Dashboard
            panelContent.Controls.Clear();
            panelContent.Controls.Add(panelDashboard);
            panelContent.Controls.Add(panelGreeting);
            panelContent.Controls.Add(lblContentSubtitle);
            panelContent.Controls.Add(lblContentTitle);

            // Đảm bảo tất cả control đều visible
            lblContentTitle.Visible = true;
            lblContentSubtitle.Visible = true;
            panelDashboard.Visible = true;
            panelGreeting.Visible = true;

            // Đưa các control lên trước
            lblContentTitle.BringToFront();
            lblContentSubtitle.BringToFront();
            panelDashboard.BringToFront();
            panelGreeting.BringToFront();

            // Cập nhật dữ liệu dashboard
            UpdateDashboardOverview();
        }

        private void ConfigureMenuButtons()
        {
            // Lưu style mặc định của các button menu để có thể reset sau này
            var buttons = new[] { btnDashboard, btnProduct, btnInventory, btnPos, btnInvoice, btnCustomer, btnStaff, btnReport };

            foreach (var button in buttons)
            {
                if (button != null && !_menuButtonStyles.ContainsKey(button))
                {
                    _menuButtonStyles[button] = new ButtonAppearance
                    {
                        BackColor = button.BackColor,
                        ForeColor = button.ForeColor,
                        Font = button.Font
                    };
                }
            }

            // Đặt Dashboard là button mặc định được chọn
            SetActiveMenuButton(btnDashboard);
        }

        /// <summary>
        /// Cập nhật dữ liệu tổng quan trên Dashboard
        /// </summary>
        private void UpdateDashboardOverview()
        {
            if (_reportService == null)
            {
                return;
            }

            var overview = _reportService.GetTodayOverview(DateTime.Now);

            // Cập nhật các chỉ số
            lblRevenueTodayValue.Text = $"{overview.TodayRevenue:N0} đ";
            lblOrdersTodayValue.Text = overview.TodayOrders.ToString();
            lblTopProductValue.Text = overview.TopProductName;
            lblLowStockValue.Text = overview.LowStockAlert;

            // Cập nhật thông tin chào mừng
            var userName = _currentUser?.FullName ?? "Bạn";
            lblGreeting.Text = $"Xin chào, {userName}!";
            lblWorkingDate.Text = $"Ngày làm việc: {DateTime.Now:dd/MM/yyyy (dddd)}";
        }

        /// <summary>
        /// Kiểm tra xem form có đang chạy trong Visual Studio Designer không
        /// </summary>
        private static bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Đặt một button menu là active (highlight)
        /// </summary>
        private void SetActiveMenuButton(Button button)
        {
            if (button == null || _activeMenuButton == button)
            {
                return;
            }

            // Reset button cũ về style mặc định
            ResetMenuButton(_activeMenuButton);

            // Áp dụng style highlight cho button mới
            _activeMenuButton = button;
            button.BackColor = Color.FromArgb(102, 106, 148);
            button.ForeColor = Color.White;
            button.Font = new Font(button.Font, FontStyle.Bold);
        }

        /// <summary>
        /// Reset button menu về style mặc định
        /// </summary>
        private void ResetMenuButton(Button button)
        {
            if (button == null || !_menuButtonStyles.TryGetValue(button, out var appearance))
            {
                return;
            }

            button.BackColor = appearance.BackColor;
            button.ForeColor = appearance.ForeColor;
            button.Font = appearance.Font;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowContentForm(new frmSettings(_currentUser));
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            MessageBox.Show(
                $"✓ Check-in thành công!\nThời gian: {currentTime}",
                "Chấm công - Vào làm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            MessageBox.Show(
                $"✓ Check-out thành công!\nThời gian: {currentTime}",
                "Chấm công - Tan làm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnCustomer);
            ShowContentForm(new GUI.frmKhachHang());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnStaff);
            ShowContentForm(new GUI.frnNhanVien());
        }

        private struct ButtonAppearance
        {
            public Color BackColor;
            public Color ForeColor;
            public Font Font;
        }

    }
}
