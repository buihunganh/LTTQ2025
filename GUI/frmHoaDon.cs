using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using ClosedXML.Excel;

namespace BTL_LTTQ.GUI
{
    public partial class frmHoaDon : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private BTL_LTTQ.DTO.LoginResult _currentUser;

        // Business variables
        private string _maHDString = "";
        private string _ngayBan = "";
        private string _tenNV = "";
        private int _maNV = 0;
        private string _tenKH = "";
        private string _sdt = "";
        private string _diaChi = "";
        private decimal _tongTienSo = 0;

        private DataTable _dtChiTiet;
        private int _idHoaDonVuaLuu = 0;
        private bool _isEditMode = false;

        // Constructor for new invoice from POS
        public frmHoaDon(DataTable gioHangTuPOS, BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _dtChiTiet = gioHangTuPOS.Copy();

            string datePart = DateTime.Now.ToString("ddMMyyyy");
            int sequence = GetNextInvoiceSequence(datePart);
            _maHDString = $"HDB_{datePart}0{sequence:D3}";
            _ngayBan = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            
            if (_currentUser != null)
            {
                _tenNV = _currentUser.FullName;
                _maNV = _currentUser.EmployeeId;
            }
            else
            {
                _tenNV = "Không xác định";
                _maNV = 1;
            }

            LoadInitData(isViewOnly: false);
            CalculateTotal();
        }

        // Constructor for viewing existing invoice
        public frmHoaDon(int maHD, BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;

            DataTable dtChung = _bll.GetInvoiceGeneral(maHD);
            DataTable dtChiTiet = _bll.GetInvoiceDetail(maHD);

            if (dtChung.Rows.Count > 0)
            {
                DataRow r = dtChung.Rows[0];
                _maHDString = r["MaHoaDon"].ToString();
                _ngayBan = Convert.ToDateTime(r["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                _tenNV = r["NhanVien"].ToString();
                _tenKH = r["KhachHang"].ToString();
                _sdt = r["SoDienThoai"].ToString();
                _diaChi = r["DiaChi"].ToString();
                _tongTienSo = r["ThanhToan"] != DBNull.Value ? Convert.ToDecimal(r["ThanhToan"]) : 0;
                _idHoaDonVuaLuu = maHD;
            }

            _dtChiTiet = dtChiTiet;
            LoadInitData(isViewOnly: true);
        }

        private int GetNextInvoiceSequence(string datePart)
        {
            try
            {
                using (var dal = new BTL_LTTQ.DAL.DataProcesser())
                {
                    string sql = @"SELECT COUNT(*) + 1 
                                   FROM HoaDon 
                                   WHERE MaHoaDon LIKE @Pattern";
                    var result = dal.ExecuteQuery(sql, System.Data.CommandType.Text,
                        new System.Data.SqlClient.SqlParameter("@Pattern", $"HDB_{datePart}0%"));
                    if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
                    {
                        return Convert.ToInt32(result.Rows[0][0]);
                    }
                }
            }
            catch { }
            return 1;
        }

        private void LoadInitData(bool isViewOnly)
        {
            txtMaHD.Text = _maHDString;
            txtNgayBan.Text = _ngayBan;
            txtNhanVien.Text = _tenNV;

            try
            {
                cboKhachHang.DataSource = _bll.GetKhachHang();
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";

                if (isViewOnly)
                {
                    cboKhachHang.Text = _tenKH;
                    cboKhachHang.Enabled = false;
                    txtSDT.Text = _sdt;
                    txtDiaChi.Text = _diaChi;
                    lblTongTien.Text = _tongTienSo.ToString("N0") + " VNĐ";

                    btnLuu.Visible = false;
                    btnIn.Enabled = true;
                    btnThemKhach.Visible = false;
                    numGiamGia.Enabled = false;
                    dgvChiTiet.ReadOnly = true;

                    // Only admin can edit and cancel invoices
                    bool isAdmin = _currentUser != null && _currentUser.IsAdmin;
                    btnSua.Visible = isAdmin;
                    btnHuy.Enabled = isAdmin;
                    if (!isAdmin)
                    {
                        btnHuy.Text = "Đóng";
                    }
                }
                else
                {
                    cboKhachHang.SelectedIndex = -1;
                }
            }
            catch { }

            dgvChiTiet.DataSource = _dtChiTiet;
            if (dgvChiTiet.Columns.Contains("MaCTSP")) dgvChiTiet.Columns["MaCTSP"].Visible = false;
            if (dgvChiTiet.Columns.Contains("DonGia")) dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvChiTiet.Columns.Contains("ThanhTien")) dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void CalculateTotal()
        {
            decimal tongTienHang = 0;
            foreach (DataRow r in _dtChiTiet.Rows) tongTienHang += Convert.ToDecimal(r["ThanhTien"]);

            decimal giamGia = tongTienHang * (numGiamGia.Value / 100);
            decimal thanhToan = tongTienHang - giamGia;

            lblTongTien.Text = thanhToan.ToString("N0") + " VNĐ";
        }

        private void CboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboKhachHang.SelectedItem;
                txtSDT.Text = drv["SoDienThoai"].ToString();
                txtDiaChi.Text = drv["DiaChi"] != DBNull.Value && !string.IsNullOrEmpty(drv["DiaChi"].ToString()) 
                    ? drv["DiaChi"].ToString() 
                    : "Khách tại quầy";
            }
        }

        private void txtSDTInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox txtBox = sender as TextBox;
                if (txtBox != null)
                {
                    TimHoacThemKhachTheoSDT(txtBox.Text.Trim());
                }
            }
        }

        private void TimHoacThemKhachTheoSDT(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!");
                return;
            }

            try
            {
                var khachHang = _bll.GetKhachHang();
                foreach (DataRow row in khachHang.Rows)
                {
                    if (row["SoDienThoai"].ToString().Trim() == sdt.Trim())
                    {
                        cboKhachHang.SelectedValue = row["MaKH"];
                        MessageBox.Show($"Đã tìm thấy khách hàng: {row["HoTen"]}");
                        return;
                    }
                }

                if (MessageBox.Show($"Không tìm thấy khách hàng với SĐT: {sdt}\nBạn có muốn thêm khách hàng mới không?",
                    "Thêm khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form f = new Form { Text = "Thêm khách mới", Size = new Size(350, 220), StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(58, 60, 92), ForeColor = Color.White, FormBorderStyle = FormBorderStyle.FixedToolWindow };
                    Label l1 = new Label { Text = "Họ tên:", Location = new Point(20, 20), AutoSize = true };
                    TextBox t1 = new TextBox { Location = new Point(20, 45), Width = 280 };
                    Label l2 = new Label { Text = "SĐT:", Location = new Point(20, 85), AutoSize = true };
                    TextBox t2 = new TextBox { Location = new Point(20, 110), Width = 280, Text = sdt, ReadOnly = true };
                    Button b = new Button { Text = "LƯU", Location = new Point(150, 145), Width = 100, DialogResult = DialogResult.OK, BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

                    f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
                    f.AcceptButton = b;

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (string.IsNullOrWhiteSpace(t1.Text)) return;
                        int newID = _bll.QuickAddCustomer(t1.Text, sdt);
                        cboKhachHang.DataSource = _bll.GetKhachHang();
                        cboKhachHang.SelectedValue = newID;
                        MessageBox.Show("Đã thêm khách hàng: " + t1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnThemKhach_Click(object sender, EventArgs e)
        {
            Form f = new Form { Text = "Thêm khách mới", Size = new Size(350, 220), StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(58, 60, 92), ForeColor = Color.White, FormBorderStyle = FormBorderStyle.FixedToolWindow };
            Label l1 = new Label { Text = "Họ tên:", Location = new Point(20, 20), AutoSize = true };
            TextBox t1 = new TextBox { Location = new Point(20, 45), Width = 280 };
            Label l2 = new Label { Text = "SĐT:", Location = new Point(20, 85), AutoSize = true };
            TextBox t2 = new TextBox { Location = new Point(20, 110), Width = 280 };
            Button b = new Button { Text = "LƯU", Location = new Point(150, 145), Width = 100, DialogResult = DialogResult.OK, BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
            f.AcceptButton = b;

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(t1.Text)) return;
                try
                {
                    int newID = _bll.QuickAddCustomer(t1.Text, t2.Text);
                    cboKhachHang.DataSource = _bll.GetKhachHang();
                    cboKhachHang.SelectedValue = newID;
                    MessageBox.Show("Đã thêm khách hàng: " + t1.Text);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void numGiamGia_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedIndex == -1 || cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }

            if (_dtChiTiet == null || _dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có hàng để lưu!");
                return;
            }

            try
            {
                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                int maNV = _maNV > 0 ? _maNV : (_currentUser?.EmployeeId ?? 1);

                decimal tongHang = 0;
                foreach (DataRow r in _dtChiTiet.Rows) tongHang += Convert.ToDecimal(r["ThanhTien"]);
                decimal tienGiam = tongHang * (numGiamGia.Value / 100);
                decimal thanhToan = tongHang - tienGiam;

                if (_isEditMode && _idHoaDonVuaLuu > 0)
                {
                    DataTable dtChiTietMoi = new DataTable();
                    dtChiTietMoi.Columns.Add("MaCTSP", typeof(int));
                    dtChiTietMoi.Columns.Add("TenSP", typeof(string));
                    dtChiTietMoi.Columns.Add("SoLuong", typeof(int));
                    dtChiTietMoi.Columns.Add("DonGia", typeof(decimal));
                    dtChiTietMoi.Columns.Add("GiamGia", typeof(int));
                    dtChiTietMoi.Columns.Add("ThanhTien", typeof(decimal));

                    foreach (DataGridViewRow row in dgvChiTiet.Rows)
                    {
                        if (row.IsNewRow) continue;
                        int maCTSP = Convert.ToInt32(row.Cells["MaCTSP"].Value);
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                        int giamGiaSP = row.Cells["GiamGia"] != null && row.Cells["GiamGia"].Value != DBNull.Value 
                            ? Convert.ToInt32(row.Cells["GiamGia"].Value) : 0;
                        decimal thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);

                        var sanPham = _bll.GetSanPhamBanHang();
                        bool found = false;
                        foreach (DataRow spRow in sanPham.Rows)
                        {
                            if (Convert.ToInt32(spRow["MaCTSP"]) == maCTSP)
                            {
                                int tonKho = Convert.ToInt32(spRow["SoLuongTon"]);
                                if (soLuong > tonKho)
                                {
                                    MessageBox.Show($"Sản phẩm {row.Cells["TenSP"].Value} chỉ còn {tonKho} trong kho!");
                                    return;
                                }
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            MessageBox.Show($"Không tìm thấy sản phẩm với mã {maCTSP}!");
                            return;
                        }

                        dtChiTietMoi.Rows.Add(maCTSP, row.Cells["TenSP"].Value, soLuong, donGia, giamGiaSP, thanhTien);
                    }

                    bool success = _bll.CapNhatHoaDon(_idHoaDonVuaLuu, maKH, tongHang, tienGiam, thanhToan, dtChiTietMoi);
                    if (success)
                    {
                        MessageBox.Show("Cập nhật hóa đơn thành công!");
                        _dtChiTiet = dtChiTietMoi;
                        _isEditMode = false;
                        btnLuu.Visible = false;
                        btnLuu.Text = "Lưu Hóa Đơn";
                        btnLuu.BackColor = Color.Green;
                        btnSua.Visible = true;
                        btnIn.Enabled = true;
                        cboKhachHang.Enabled = false;
                        numGiamGia.Enabled = false;
                        dgvChiTiet.ReadOnly = true;
                        btnThemKhach.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật hóa đơn! Vui lòng kiểm tra lại dữ liệu.");
                    }
                }
                else
                {
                    _idHoaDonVuaLuu = _bll.ThanhToan(maKH, maNV, tongHang, tienGiam, thanhToan, _dtChiTiet);

                    if (_idHoaDonVuaLuu > 0)
                    {
                        MessageBox.Show("Lưu thành công! Mã HĐ: " + _idHoaDonVuaLuu);
                        btnLuu.Enabled = false;
                        btnIn.Enabled = true;
                        cboKhachHang.Enabled = false;
                        numGiamGia.Enabled = false;
                        txtMaHD.Text = "HD" + _idHoaDonVuaLuu;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (_idHoaDonVuaLuu == 0) { MessageBox.Show("Phải Lưu hóa đơn trước khi in!"); return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            sfd.FileName = "HoaDon_" + txtMaHD.Text + ".xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("HoaDon");

                        worksheet.Cell(1, 1).Value = "HÓA ĐƠN BÁN HÀNG";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range(1, 1, 1, 4).Merge();

                        worksheet.Cell(3, 1).Value = "Mã HĐ:";
                        worksheet.Cell(3, 2).Value = txtMaHD.Text;
                        worksheet.Cell(4, 1).Value = "Ngày:";
                        worksheet.Cell(4, 2).Value = txtNgayBan.Text;
                        worksheet.Cell(5, 1).Value = "Nhân viên:";
                        worksheet.Cell(5, 2).Value = txtNhanVien.Text;
                        worksheet.Cell(6, 1).Value = "Khách hàng:";
                        worksheet.Cell(6, 2).Value = cboKhachHang.Text;
                        worksheet.Cell(7, 1).Value = "SĐT:";
                        worksheet.Cell(7, 2).Value = txtSDT.Text;
                        worksheet.Cell(8, 1).Value = "Địa chỉ:";
                        worksheet.Cell(8, 2).Value = txtDiaChi.Text;

                        int rowStart = 10;
                        worksheet.Cell(rowStart, 1).Value = "Tên Hàng";
                        worksheet.Cell(rowStart, 2).Value = "Số Lượng";
                        worksheet.Cell(rowStart, 3).Value = "Đơn Giá";
                        worksheet.Cell(rowStart, 4).Value = "Thành Tiền";
                        worksheet.Range(rowStart, 1, rowStart, 4).Style.Font.Bold = true;
                        worksheet.Range(rowStart, 1, rowStart, 4).Style.Fill.BackgroundColor = XLColor.LightGray;

                        int row = rowStart + 1;
                        foreach (DataGridViewRow dgvRow in dgvChiTiet.Rows)
                        {
                            if (dgvRow.IsNewRow) continue;
                            worksheet.Cell(row, 1).Value = dgvRow.Cells["TenSP"].Value?.ToString();
                            worksheet.Cell(row, 2).Value = Convert.ToInt32(dgvRow.Cells["SoLuong"].Value);
                            worksheet.Cell(row, 3).Value = Convert.ToDecimal(dgvRow.Cells["DonGia"].Value);
                            worksheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(row, 4).Value = Convert.ToDecimal(dgvRow.Cells["ThanhTien"].Value);
                            worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0";
                            row++;
                        }

                        worksheet.Cell(row, 3).Value = "TỔNG THANH TOÁN:";
                        worksheet.Cell(row, 3).Style.Font.Bold = true;
                        worksheet.Cell(row, 4).Value = lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "");
                        worksheet.Cell(row, 4).Style.Font.Bold = true;
                        worksheet.Cell(row, 4).Style.Font.FontColor = XLColor.Red;
                        worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0";

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Xuất hóa đơn thành công!");
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message);
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            // Check admin permission before editing invoice
            bool isAdmin = _currentUser != null && _currentUser.IsAdmin;
            if (!isAdmin)
            {
                MessageBox.Show("Chỉ quản trị viên mới có quyền sửa hóa đơn!", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_idHoaDonVuaLuu == 0)
            {
                MessageBox.Show("Không có hóa đơn để sửa!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn sửa hóa đơn này không?\nLưu ý: Số lượng trong kho sẽ được điều chỉnh tự động.",
                "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _isEditMode = true;
                cboKhachHang.Enabled = true;
                numGiamGia.Enabled = true;
                btnThemKhach.Visible = true;
                dgvChiTiet.ReadOnly = false;
                btnSua.Visible = false;
                btnLuu.Visible = true;
                btnLuu.Text = "Cập nhật";
                btnLuu.BackColor = Color.Blue;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            if (_idHoaDonVuaLuu > 0)
            {
                // Check admin permission before canceling invoice
                bool isAdmin = _currentUser != null && _currentUser.IsAdmin;
                if (!isAdmin)
                {
                    MessageBox.Show("Chỉ quản trị viên mới có quyền hủy hóa đơn!", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                if (MessageBox.Show("Bạn có muốn hủy hóa đơn này và cộng lại số lượng vào kho không?",
                    "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (_bll.HuyHoaDon(_idHoaDonVuaLuu))
                        {
                            MessageBox.Show("Đã hủy hóa đơn và cộng lại số lượng vào kho!");
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không thể hủy hóa đơn!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}