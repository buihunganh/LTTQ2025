using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(string keyword = "")
        {
            dgvKhachHang.DataSource = string.IsNullOrEmpty(keyword) ? bll.GetList() : bll.Search(keyword);

            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Tên Khách Hàng";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns["TongChiTieu"].HeaderText = "Tổng Chi Tiêu";
            dgvKhachHang.Columns["TongChiTieu"].DefaultCellStyle.Format = "N0";
            dgvKhachHang.Columns["HangThanhVien"].HeaderText = "Hạng Thành Viên";
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
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Nhập tên!"); return; }

            if (bll.Add(txtHoTen.Text, txtSDT.Text))
            {
                MessageBox.Show("Thêm thành công!");
                LoadData();
                btnLamMoi_Click(null, null);
            }
            else MessageBox.Show("Lỗi: Thiếu thông tin.");
        }

        private void btnLuu_Click(object sender, EventArgs e) // Nút Sửa/Lưu
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            if (bll.Edit(int.Parse(txtMaKH.Text), txtHoTen.Text, txtSDT.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData();
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
                    LoadData();
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
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
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
            if (string.IsNullOrEmpty(txtMaKH.Text)) { MessageBox.Show("Vui lòng chọn khách hàng."); return; }
            MessageBox.Show($"Lịch sử mua hàng của: {txtHoTen.Text}\n(Chức năng này chờ module Hóa Đơn)");
        }
    }
}