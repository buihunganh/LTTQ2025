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
        private BTL_LTTQ.DTO.LoginResult _currentUser;

        public frmBanHang(BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            _currentUser = currentUser;
            InitializeComponent();
            this.Load += frmBanHang_Load;
            InitGioHang();
        }

        public frmBanHang() : this(null)
        {
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
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in _dtGioHang.Rows)
            {
                total += Convert.ToDecimal(row["ThanhTien"]);
            }
            lblTongTien.Text = $"Tổng tiền: {total:N0} VNĐ";
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
            
            if (dgvGioHang.Columns["MaCTSP"] != null) 
                dgvGioHang.Columns["MaCTSP"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null) return;

            DataRowView drv = (DataRowView)cboSanPham.SelectedItem;
            int tonKho = Convert.ToInt32(drv["SoLuongTon"]);
            decimal giaBan = Convert.ToDecimal(drv["GiaBan"]);
            int slMua = (int)numSoLuong.Value;
            int giamGia = (int)numGiamGiaSP.Value;

            if (slMua > tonKho) { MessageBox.Show($"Kho chỉ còn {tonKho}!"); return; }

            decimal tienGiam = (giaBan * slMua) * giamGia / 100;
            decimal thanhTien = (giaBan * slMua) - tienGiam;

            foreach (DataRow r in _dtGioHang.Rows)
            {
                if ((int)r["MaCTSP"] == (int)cboSanPham.SelectedValue && 
                    (int)r["GiamGia"] == giamGia)
                {
                    r["SoLuong"] = (int)r["SoLuong"] + slMua;
                    r["ThanhTien"] = (decimal)r["ThanhTien"] + thanhTien;
                    CalculateTotal();
                    return;
                }
            }

            _dtGioHang.Rows.Add(cboSanPham.SelectedValue, cboSanPham.Text, slMua, giaBan, giamGia, thanhTien);
            numSoLuong.Value = 1;
            numGiamGiaSP.Value = 0;
            
            CalculateTotal();
        }

        private void DgvGioHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _dtGioHang.Rows.RemoveAt(e.RowIndex);
                CalculateTotal();
            }
        }

        private void btnGoToInvoice_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0) { MessageBox.Show("Giỏ hàng trống!"); return; }

            frmHoaDon f = new frmHoaDon(_dtGioHang, _currentUser);
            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                _dtGioHang.Rows.Clear();
                cboSanPham.SelectedIndex = -1;
                numSoLuong.Value = 1;
            }
        }

    }
}
