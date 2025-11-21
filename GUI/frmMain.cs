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
            SetupAvatarCircular(); // Setup avatar hình tròn
            ApplyUserContext();
            ConfigureMenuButtons();
            _reportService = IsInDesignMode() ? null : new ReportService();
        }

        private void SetupAvatarCircular()
        {
            // Tạo hình tròn cho avatar
            if (picAvatar != null)
            {
                picAvatar.Paint += (s, e) =>
                {
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                        picAvatar.Region = new Region(path);

                        // Vẽ viền
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        using (var pen = new Pen(Color.FromArgb(232, 90, 79), 3))
                        {
                            e.Graphics.DrawEllipse(pen, 1, 1, picAvatar.Width - 3, picAvatar.Height - 3);
                        }
                    }
                };

                // Tạo avatar mặc định với chữ cái đầu tên người dùng
                CreateDefaultAvatar();
            }
        }

        private void CreateDefaultAvatar()
        {
            if (picAvatar == null) return;

            int size = picAvatar.Width;
            Bitmap bitmap = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Gradient background
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.FromArgb(102, 106, 148),
                    Color.FromArgb(58, 60, 92),
                    45f))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }

                // Chữ cái đầu của tên
                string initial = "A"; // Mặc định
                if (_currentUser != null && !string.IsNullOrEmpty(_currentUser.FullName))
                {
                    var parts = _currentUser.FullName.Trim().Split(' ');
                    initial = parts.Length > 0 ? parts[parts.Length - 1].Substring(0, 1).ToUpper() : "A";
                }

                using (var font = new Font("Segoe UI", size / 2.5f, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(initial, font, brush, size / 2f, size / 2f, format);
                }
            }

            picAvatar.Image = bitmap;
        }

        private void ApplyUserContext()
        {
            var displayName = _currentUser?.FullName ?? "Không xác định";
            var roleLabel = _currentUser == null
                ? "---"
                : $"{(_currentUser.IsAdmin ? "Quản trị viên" : "Nhân viên")}";

            lblUser.Text = displayName;
            lblRole.Text = roleLabel;

            // Cập nhật avatar
            CreateDefaultAvatar();
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
        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            // 1. Hiệu ứng đổi màu nút đang chọn
            if (sender is Button btn)
            {
                SetActiveMenuButton(btn);
            }

            // 2. Mở form Quản Lý Hóa Đơn (Danh sách)
            // Lưu ý: Đảm bảo bạn đã tạo file frmQuanLyHoaDon.cs rồi nhé
            ShowContentForm(new BTL_LTTQ.GUI.frmQuanLyHoaDon());
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

        private void panelRevenueToday_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuanLyKho_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                SetActiveMenuButton(btn);
            }

            // 2. Gọi form Nhập hàng hiện lên màn hình chính
            // (Lưu ý: namespace BTL_LTTQ.GUI là nơi chứa form frmNhapHang)
            ShowContentForm(new BTL_LTTQ.GUI.frmNhapHang());
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                SetActiveMenuButton(btn);
            }
            ShowContentForm(new BTL_LTTQ.GUI.frmBanHang());
        }
    }
}
