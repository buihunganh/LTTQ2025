using System;
using System.Windows.Forms;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ
{
    public partial class frmMain : Form
    {
        private readonly LoginResult _currentUser;

        public frmMain() : this(null)
        {
        }

        public frmMain(LoginResult user)
        {
            InitializeComponent();
            _currentUser = user;
            ApplyUserContext();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

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
    }
}
