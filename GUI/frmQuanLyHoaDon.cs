using System;
using System.Data;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmQuanLyHoaDon : Form
    {
        private SalesBLL _bll = new SalesBLL();

        public frmQuanLyHoaDon()
        {
            InitializeComponent();
            LoadInvoiceCodesList();
        }

        private void frmQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadInvoiceCodesList()
        {
            try
            {
                cboMaHD.Items.Add(""); // Thêm mục trống đầu tiên
                DataTable dtMaHD = _bll.GetInvoiceCodesList();
                foreach (DataRow row in dtMaHD.Rows)
                {
                    cboMaHD.Items.Add(row["MaHoaDon"].ToString());
                }
                cboMaHD.SelectedIndex = 0; // Chọn mục trống
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách mã hóa đơn: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string selectedMaHD = cboMaHD.Text.Trim();
                DataTable dt = _bll.FindInvoices(dtpFrom.Value, dtpTo.Value, txtTenNV.Text.Trim(), txtTenKH.Text.Trim(), selectedMaHD);
                dgvHoaDon.DataSource = dt;

                // Ẩn cột ID, chỉ hiện Mã hiển thị
                if (dgvHoaDon.Columns.Contains("MaHD")) dgvHoaDon.Columns["MaHD"].Visible = false;

                // Format tiền
                if (dgvHoaDon.Columns.Contains("TongTien"))
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";

                // Đổi tên cột cho đẹp
                dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã HĐ";
                dgvHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvHoaDon.Columns["TenNhanVien"].HeaderText = "Nhân Viên";
                dgvHoaDon.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvHoaDon.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Lỗi: " + ex.Message); 
            }
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy ID hóa đơn từ dòng được chọn
                int maHD = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value);

                // Mở form chi tiết
                frmHoaDon f = new frmHoaDon(maHD);
                f.ShowDialog();
            }
        }
    }
}