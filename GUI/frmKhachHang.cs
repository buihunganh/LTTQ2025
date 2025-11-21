using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using ClosedXML.Excel;

namespace BTL_LTTQ.GUI
{
    public partial class frmKhachHang : Form
    {
        private KhachHangBLL bll = new KhachHangBLL();

        public frmKhachHang()
        {
            InitializeComponent();
            InitFilter(); 
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            if (cmbLocHang.Items.Count > 0) cmbLocHang.SelectedIndex = 0; 
        }

        private void InitFilter()
        {
            cmbLocHang.Items.Clear();
            cmbLocHang.Items.Add("Tất cả");
            cmbLocHang.Items.Add("Mới");
            cmbLocHang.Items.Add("Thành viên");
            cmbLocHang.Items.Add("Bạc");
            cmbLocHang.Items.Add("Vàng");
            cmbLocHang.Items.Add("Kim cương");

        }

        private void LoadData(string keyword = "")
        {
            string rankFilter = "";
            if (cmbLocHang.SelectedIndex > 0) 
            {
                rankFilter = cmbLocHang.SelectedItem.ToString();
            }

            dgvKhachHang.DataSource = bll.Search(keyword, rankFilter);

            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Tên Khách Hàng";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns["TongChiTieu"].HeaderText = "Tổng Chi Tiêu";
            dgvKhachHang.Columns["TongChiTieu"].DefaultCellStyle.Format = "N0";
            dgvKhachHang.Columns["HangThanhVien"].HeaderText = "Hạng Thành Viên";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void cmbLocHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtChiTieu.Text = string.Format("{0:N0} VND", row.Cells["TongChiTieu"].Value);
                txtHang.Text = row.Cells["HangThanhVien"].Value.ToString();
                txtChiTieu.ReadOnly = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtChiTieu.ReadOnly = false;

            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Nhập tên!"); return; }
            string cleanMoney = txtChiTieu.Text.Replace(" VND", "").Replace(",", "").Replace(".", "").Trim();
            decimal tongTien = 0;
            if(!string.IsNullOrEmpty(cleanMoney))
            {
                decimal.TryParse(cleanMoney, out tongTien);
            }
            if (bll.Add(txtHoTen.Text, txtSDT.Text, tongTien))
            {
                MessageBox.Show("Thêm thành công! Hạng thành viên đã được tính");
                LoadData();
                btnLamMoi_Click(null, null);
            }
            else MessageBox.Show("Lỗi: Thiếu thông tin.");
        }

        private void btnLuu_Click(object sender, EventArgs e) 
        {
            txtChiTieu.ReadOnly = false;
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            if (bll.Edit(int.Parse(txtMaKH.Text), txtHoTen.Text, txtSDT.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData(txtSearch.Text);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            if (MessageBox.Show("Xóa khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bll.Delete(int.Parse(txtMaKH.Text)))
                {
                    MessageBox.Show("Đã xóa.");
                    LoadData(txtSearch.Text);
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtChiTieu.Text = "";
            txtHang.Text = "";
            txtSearch.Text = "";
            if (cmbLocHang.Items.Count > 0) cmbLocHang.SelectedIndex = 0;
            LoadData();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel|*.xlsx", FileName = "KhachHang_" + DateTime.Now.ToString("ddMMyy") };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("KhachHang");
                    ws.Cell(1, 1).InsertTable(dgvKhachHang.DataSource as DataTable);
                    ws.Columns().AdjustToContents();
                    wb.SaveAs(sfd.FileName);
                }
                MessageBox.Show("Xuất file thành công!");
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(txtMaKH.Text, out var maKH))
            {
                MessageBox.Show("Không thể xác định mã khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var history = bll.GetPurchaseHistory(maKH, 30);
            if (history == null || history.Rows.Count == 0)
            {
                MessageBox.Show("Khách hàng chưa có hóa đơn nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowPurchaseHistory(history, txtHoTen.Text);
        }

        private void ShowPurchaseHistory(DataTable history, string customerName)
        {
            decimal totalSpent = history.AsEnumerable().Sum(r => r.Field<decimal>("TongTien"));
            int totalOrders = history.Rows.Count;

            var dialog = new Form
            {
                Text = $"Lịch sử mua hàng - {customerName}",
                StartPosition = FormStartPosition.CenterParent,
                Size = new Size(720, 440),
                MinimumSize = new Size(620, 380),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblSummary = new Label
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(12, 10, 12, 4),
                Text = $"Tổng đơn: {totalOrders} | Tổng chi tiêu: {totalSpent:N0} đ",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = history
            };

            if (grid.Columns.Contains("MaHD")) grid.Columns["MaHD"].Visible = false;
            if (grid.Columns.Contains("MaHoaDon")) grid.Columns["MaHoaDon"].HeaderText = "Mã hóa đơn";
            if (grid.Columns.Contains("NgayLap")) grid.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            if (grid.Columns.Contains("TongSanPham")) grid.Columns["TongSanPham"].HeaderText = "Số SP";
            if (grid.Columns.Contains("TongTien")) grid.Columns["TongTien"].HeaderText = "Tổng tiền";
            if (grid.Columns.Contains("TongTien")) grid.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            if (grid.Columns.Contains("ThanhToan")) grid.Columns["ThanhToan"].HeaderText = "Thanh toán";
            if (grid.Columns.Contains("ThanhToan")) grid.Columns["ThanhToan"].DefaultCellStyle.Format = "N0";
            if (grid.Columns.Contains("TrangThai")) grid.Columns["TrangThai"].HeaderText = "Trạng thái";

            dialog.Controls.Add(grid);
            dialog.Controls.Add(lblSummary);
            dialog.ShowDialog(this);
        }

        private void txtChiTieu_TextChanged(object sender, EventArgs e)
        {

        }
    }
}