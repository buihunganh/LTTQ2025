using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using BTL_LTTQ.DTO;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ
{
    public partial class frmMain : Form
    {
        private readonly LoginResult _currentUser;
        private readonly Dictionary<Button, ButtonAppearance> _menuButtonStyles = new Dictionary<Button, ButtonAppearance>();
        private Form _activeContentForm;
        private Button _activeMenuButton;
        private readonly ReportService _reportService;
        private readonly string _projectRootPath;
        private DateTime? _lastCheckIn;
        private DateTime? _lastCheckOut;

        public frmMain() : this(null)
        {
        }

        public frmMain(LoginResult user)
        {
            InitializeComponent();
            _currentUser = user;
            _projectRootPath = GetProjectRootPath();
            SetupAvatarCircular();
            ApplyUserContext();
            ConfigureMenuButtons();
            _reportService = IsInDesignMode() ? null : new ReportService();
            UpdateCheckStatusLabel();
            LoadRealtimeNotifications();
        }

        private void SetupAvatarCircular()
        {

            if (picAvatar != null)
            {
                picAvatar.Paint += (s, e) =>
                {
                    using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                        picAvatar.Region = new Region(path);


                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        using (var pen = new Pen(Color.FromArgb(232, 90, 79), 3))
                        {
                            e.Graphics.DrawEllipse(pen, 1, 1, picAvatar.Width - 3, picAvatar.Height - 3);
                        }
                    }
                };


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


                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.FromArgb(102, 106, 148),
                    Color.FromArgb(58, 60, 92),
                    45f))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }

                string initial = "A";
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


            bool isAdmin = _currentUser?.IsAdmin ?? false;
            if (btnStaff != null)
            {
                btnStaff.Visible = isAdmin;
                btnStaff.Enabled = isAdmin;
            }


            LoadAvatarFromFile();
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

        private void btnProduct_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                SetActiveMenuButton(btn);
            }
            ShowContentForm(new frmSanpham());
        }

        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {

            if (sender is Button btn)
            {
                SetActiveMenuButton(btn);
            }


            ShowContentForm(new BTL_LTTQ.GUI.frmQuanLyHoaDon(_currentUser));
        }

        /// <summary>
        /// Hiển thị một form con trong panelContent
        /// </summary>
        private void ShowContentForm(Form contentForm)
        {

            if (_activeContentForm != null)
            {
                _activeContentForm.Close();
                _activeContentForm.Dispose();
            }


            _activeContentForm = contentForm;
            contentForm.TopLevel = false;
            contentForm.FormBorderStyle = FormBorderStyle.None;
            contentForm.Dock = DockStyle.Fill;


            panelContent.Controls.Clear();
            panelContent.Controls.Add(contentForm);
            contentForm.Show();
        }

        /// <summary>
        /// Hiển thị nội dung Dashboard (trang chủ)
        /// </summary>
        private void ShowHomeContent()
        {

            if (_activeContentForm != null)
            {
                _activeContentForm.Close();
                _activeContentForm.Dispose();
                _activeContentForm = null;
            }


            panelContent.Controls.Clear();
            panelContent.Controls.Add(panelDashboard);
            panelContent.Controls.Add(panelQuickActions);
            panelContent.Controls.Add(panelRealtime);
            panelContent.Controls.Add(panelGreeting);
            panelContent.Controls.Add(lblContentSubtitle);
            panelContent.Controls.Add(lblContentTitle);


            lblContentTitle.Visible = true;
            lblContentSubtitle.Visible = true;
            panelDashboard.Visible = true;
            panelQuickActions.Visible = true;
            panelRealtime.Visible = true;
            panelGreeting.Visible = true;


            lblContentTitle.BringToFront();
            lblContentSubtitle.BringToFront();
            panelDashboard.BringToFront();
            panelQuickActions.BringToFront();
            panelRealtime.BringToFront();
            panelGreeting.BringToFront();


            UpdateDashboardOverview();
        }

        private void ConfigureMenuButtons()
        {

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


            lblRevenueTodayValue.Text = $"{overview.TodayRevenue:N0} đ";
            lblOrdersTodayValue.Text = overview.TodayOrders.ToString();
            lblTopProductValue.Text = overview.TopProductName;
            lblLowStockValue.Text = overview.LowStockAlert;

            LoadRealtimeNotifications();


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


            ResetMenuButton(_activeMenuButton);


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
            var settingsForm = new frmSettings(_currentUser);
            ShowContentForm(settingsForm);


            settingsForm.FormClosed += (s, args) =>
            {
                LoadAvatarFromFile();
            };
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            _lastCheckIn = DateTime.Now;
            UpdateCheckStatusLabel();
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
            _lastCheckOut = DateTime.Now;
            UpdateCheckStatusLabel();
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

            if (_currentUser == null || !_currentUser.IsAdmin)
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập chức năng này.\nChỉ quản trị viên mới có thể quản lý nhân viên.",
                    "Không có quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            SetActiveMenuButton(btnStaff);
            ShowContentForm(new GUI.frnNhanVien());
        }

        private void btnQuickInvoice_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnPos);
            ShowContentForm(new BTL_LTTQ.GUI.frmBanHang(_currentUser));
        }

        private void btnQuickImport_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnInventory);
            ShowContentForm(new BTL_LTTQ.GUI.frmNhapHang());
        }

        private void btnQuickReport_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnReport);
            ShowContentForm(new frmReport());
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


            ShowContentForm(new BTL_LTTQ.GUI.frmNhapHang());
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                SetActiveMenuButton(btn);
            }
            ShowContentForm(new BTL_LTTQ.GUI.frmBanHang(_currentUser));
        }

        /// <summary>
        /// Load avatar từ file path (đã upload trong form Settings)
        /// </summary>
        private void LoadAvatarFromFile()
        {
            try
            {

                using (var dataProcesser = new DataProcesser())
                {
                    var profile = dataProcesser.GetEmployeeProfile(_currentUser?.EmployeeId ?? 0);

                    if (profile != null && !string.IsNullOrEmpty(profile.AvatarPath))
                    {
                        string fullPath = GetAvatarFullPath(profile.AvatarPath);
                        if (!string.IsNullOrEmpty(fullPath) && System.IO.File.Exists(fullPath))
                        {

                            using (var img = Image.FromFile(fullPath))
                            {
                                picAvatar.Image = new Bitmap(img);
                            }
                            return;
                        }
                    }
                }
            }
            catch
            {

            }


            CreateDefaultAvatar();
        }

        private string GetAvatarFullPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return null;
            }


            var projectPath = System.IO.Path.Combine(_projectRootPath, relativePath);
            if (System.IO.File.Exists(projectPath))
            {
                return projectPath;
            }


            var startupPath = System.IO.Path.Combine(Application.StartupPath, relativePath);
            if (System.IO.File.Exists(startupPath))
            {
                return startupPath;
            }

            return projectPath;
        }

        private static string GetProjectRootPath()
        {
            string root = Application.StartupPath;

            try
            {
                root = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.StartupPath, @"..\.."));
            }
            catch
            {

            }

            return root;
        }

        private void UpdateCheckStatusLabel()
        {
            if (lblCheckStatus == null)
            {
                return;
            }

            string message;
            if (_lastCheckOut.HasValue && (!_lastCheckIn.HasValue || _lastCheckOut.Value >= _lastCheckIn.Value))
            {
                message = $"Trạng thái chấm công: Đã check-out lúc {_lastCheckOut:HH:mm:ss}";
            }
            else if (_lastCheckIn.HasValue)
            {
                message = $"Trạng thái chấm công: Đã check-in lúc {_lastCheckIn:HH:mm:ss}";
            }
            else
            {
                message = "Trạng thái chấm công: Chưa check-in";
            }

            lblCheckStatus.Text = message;
        }

        private void LoadRealtimeNotifications()
        {
            if (IsInDesignMode() || lstRealtime == null)
            {
                return;
            }

            try
            {
                lstRealtime.Items.Clear();
                using (var dataProcesser = new DataProcesser())
                {
                    var table = dataProcesser.ExecuteQuery(@"
                        SELECT TOP 5 hd.MaHD, hd.NgayLap, ISNULL(kh.HoTen, N'Khách lẻ') AS KhachHang,
                               ISNULL(hd.ThanhToan, 0) AS ThanhToan
                        FROM HoaDon hd
                        LEFT JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                        ORDER BY hd.NgayLap DESC");

                    if (table.Rows.Count == 0)
                    {
                        lstRealtime.Items.Add("Chưa có giao dịch nào hôm nay.");
                        return;
                    }

                    foreach (System.Data.DataRow row in table.Rows)
                    {
                        var createdAt = row.Field<DateTime>("NgayLap");
                        var customer = row.Field<string>("KhachHang");
                        var amount = row.Field<decimal>("ThanhToan");
                        var invoiceId = row.Field<int>("MaHD");

                        var line = $"{createdAt:HH:mm dd/MM} • HĐ #{invoiceId} • {customer} • {amount:N0} đ";
                        lstRealtime.Items.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                lstRealtime.Items.Clear();
                lstRealtime.Items.Add($"Không thể tải thông báo: {ex.Message}");
            }
        }
    }
}
