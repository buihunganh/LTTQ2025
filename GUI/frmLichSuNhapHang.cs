using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ
{
    public partial class frmLichSuNhapHang : Form
    {
        private readonly InventoryBLL _bll;
        private readonly int _maCTSP;

        public frmLichSuNhapHang(int maCTSP, string tenSanPham)
        {
            InitializeComponent();
            _bll = new InventoryBLL();
            _maCTSP = maCTSP;
            this.Text = $"Lịch sử nhập hàng - {tenSanPham}";
            LoadLichSuNhap();
        }

        private void LoadLichSuNhap()
        {
            try
            {
                var data = _bll.GetLichSuNhapByMaCTSP(_maCTSP);
                
                if (data == null || data.Rows.Count == 0)
                {
                    MessageBox.Show("Sản phẩm này chưa có lịch sử nhập hàng.\n\nLưu ý: Số lượng tồn kho có thể bao gồm số lượng ban đầu khi thêm sản phẩm mới (không qua phiếu nhập).", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvLichSu.AutoGenerateColumns = false;
                dgvLichSu.Columns.Clear();

                dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoLuong",
                    HeaderText = "Số lượng",
                    Name = "SoLuong",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoLuongConLai",
                    HeaderText = "Số lượng còn lại",
                    Name = "SoLuongConLai",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        ForeColor = Color.FromArgb(0, 150, 0)
                    }
                });

                dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "GiaNhap",
                    HeaderText = "Giá nhập",
                    Name = "GiaNhap",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayNhap",
                    HeaderText = "Ngày nhập",
                    Name = "NgayNhap",
                    Width = 200,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
                });

                dgvLichSu.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NhaCungCap",
                    HeaderText = "Nhà cung cấp",
                    Name = "NhaCungCap",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                dgvLichSu.DataSource = data;

                int tongNhap = 0;
                int tongConLai = 0;
                foreach (DataRow row in data.Rows)
                {
                    if (row["SoLuong"] != DBNull.Value)
                    {
                        tongNhap += Convert.ToInt32(row["SoLuong"]);
                    }
                    if (row["SoLuongConLai"] != DBNull.Value)
                    {
                        tongConLai += Convert.ToInt32(row["SoLuongConLai"]);
                    }
                }

                var tonKho = _bll.GetSoLuongTon(_maCTSP);
                
                lblTitle.Text = $"Lịch sử nhập hàng (Tổng nhập: {tongNhap:N0} | Còn lại: {tongConLai:N0} | Tồn kho: {tonKho:N0})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử nhập hàng: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

