using System;
using System.Data;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmBanHang : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private DataTable _dtGioHang;

        public frmBanHang()
        {
            InitializeComponent();
            InitGioHang();
        }

        private void InitGioHang()
        {
            _dtGioHang = new DataTable();
            _dtGioHang.Columns.Add("MaCTSP", typeof(int));
            _dtGioHang.Columns.Add("TenSP", typeof(string));
            _dtGioHang.Columns.Add("SoLuong", typeof(int));
            _dtGioHang.Columns.Add("DonGia", typeof(decimal));
            _dtGioHang.Columns.Add("GiamGia", typeof(int));
            _dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

            dgvGioHang.DataSource = _dtGioHang;
            if (dgvGioHang.Columns["MaCTSP"] != null)
            {
                dgvGioHang.Columns["MaCTSP"].Visible = false;
            }
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            cboSanPham.DataSource = _bll.GetSanPhamBanHang();
            cboSanPham.DisplayMember = "TenHienThi";
            cboSanPham.ValueMember = "MaCTSP";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;

            DataRowView drv = (DataRowView)cboSanPham.SelectedItem;
            int tonKho = Convert.ToInt32(drv["SoLuongTon"]);
            decimal giaBan = Convert.ToDecimal(drv["GiaBan"]);
            int slMua = (int)numSoLuong.Value;
            int giamGia = (int)numGiamGiaSP.Value;

            if (slMua > tonKho)
            {
                MessageBox.Show($"Kho chỉ còn {tonKho}!");
                return;
            }

            // Tính tiền từng món
            decimal tienGiam = (giaBan * slMua) * giamGia / 100;
            decimal thanhTien = (giaBan * slMua) - tienGiam;

            // Check trùng
            foreach (DataRow r in _dtGioHang.Rows)
            {
                if ((int)r["MaCTSP"] == (int)cboSanPham.SelectedValue)
                {
                    r["SoLuong"] = (int)r["SoLuong"] + slMua;
                    r["ThanhTien"] = (decimal)r["ThanhTien"] + thanhTien;
                    return;
                }
            }

            _dtGioHang.Rows.Add(cboSanPham.SelectedValue, cboSanPham.Text, slMua, giaBan, giamGia, thanhTien);

            // Reset nhập
            numSoLuong.Value = 1;
            numGiamGiaSP.Value = 0;
        }

        private void DgvGioHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _dtGioHang.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnGoToInvoice_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!");
                return;
            }

            // Mở form Hóa Đơn và truyền Giỏ hàng sang
            frmHoaDon f = new frmHoaDon(_dtGioHang);
            f.ShowDialog();

            // Nếu bên kia Lưu thành công (DialogResult = OK) thì xóa giỏ hàng
            if (f.DialogResult == DialogResult.OK)
            {
                _dtGioHang.Rows.Clear();
                cboSanPham.SelectedIndex = -1;
                numSoLuong.Value = 1;
            }
        }
    }
}