using System;
using System.Data;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmQuanLyHoaDon : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private BTL_LTTQ.DTO.LoginResult _currentUser;

        public frmQuanLyHoaDon(BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            
            // Initialize date filters
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;
        }

        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSachMaHoaDon();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _bll.FindInvoices(dtpFrom.Value, dtpTo.Value, txtTenNV.Text.Trim(), txtTenKH.Text.Trim());
                dgvHoaDon.DataSource = dt;

                // Hide ID column, only show display code
                if (dgvHoaDon.Columns.Contains("MaHD")) dgvHoaDon.Columns["MaHD"].Visible = false;

                // Format currency
                if (dgvHoaDon.Columns.Contains("TongTien"))
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";

                // Rename columns for better display
                dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã HĐ";
                dgvHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvHoaDon.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
                dgvHoaDon.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnTim_Click(object sender, EventArgs e) => LoadData();

        private void DgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maHD = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value);
                frmHoaDon f = new frmHoaDon(maHD, _currentUser);
                f.ShowDialog();
                LoadData(); // Refresh after closing invoice form
            }
        }

        private void LoadDanhSachMaHoaDon()
        {
            try
            {
                var dt = _bll.GetDanhSachMaHoaDon();
                cboMaHD.DataSource = dt;
                cboMaHD.DisplayMember = "MaHoaDon";
                cboMaHD.ValueMember = "MaHD";
            }
            catch { }
        }

        private void cboMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if SelectedValue is not null and is actually an integer (not a DataRowView during binding)
            if (cboMaHD.SelectedValue != null && cboMaHD.SelectedValue is int)
            {
                int maHD = (int)cboMaHD.SelectedValue;
                frmHoaDon f = new frmHoaDon(maHD, _currentUser);
                f.ShowDialog();
                LoadData(); // Refresh after closing invoice form
            }
        }
    }
}
