using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using Excel = Microsoft.Office.Interop.Excel;

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
        private int _maKH = 0;
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

            _maHDString = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
                _maNV = r["MaNV"] != DBNull.Value ? Convert.ToInt32(r["MaNV"]) : 0;
                _tenNV = r["NhanVien"].ToString();
                _maKH = r["MaKH"] != DBNull.Value ? Convert.ToInt32(r["MaKH"]) : 0;
                _tenKH = r["KhachHang"].ToString();
                _sdt = r["SoDienThoai"].ToString();
                _diaChi = r["DiaChi"].ToString();
                _tongTienSo = r["ThanhToan"] != DBNull.Value ? Convert.ToDecimal(r["ThanhToan"]) : 0;
                _idHoaDonVuaLuu = maHD;
            }

            _dtChiTiet = dtChiTiet;
            LoadInitData(isViewOnly: true);
        }



        private void LoadInitData(bool isViewOnly)
        {
            txtMaHD.Text = _maHDString;
            txtNgayBan.Text = _ngayBan;
            txtMaNV.Text = _maNV.ToString();
            txtNhanVien.Text = _tenNV;

            try
            {
                cboKhachHang.DataSource = _bll.GetKhachHang();
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";

                if (isViewOnly)
                {
                    txtMaKH.Text = _maKH > 0 ? _maKH.ToString() : "";
                    cboKhachHang.Text = _tenKH;
                    cboKhachHang.Enabled = false;
                    txtSDT.Text = _sdt;
                    txtDiaChi.Text = _diaChi;
                    txtSDT.ReadOnly = true;
                    txtDiaChi.ReadOnly = true;
                    lblTongTien.Text = _tongTienSo.ToString("N0") + " VNĐ";

                    btnLuu.Visible = false;
                    btnIn.Enabled = true;
                    btnThemKhach.Visible = false;
                    dgvChiTiet.ReadOnly = true;

                    // Only admin can cancel invoices
                    bool isAdmin = _currentUser != null && _currentUser.IsAdmin;
                    btnSua.Visible = isAdmin;
                    btnHuyHoaDon.Visible = isAdmin;
                }
                else
                {
                    cboKhachHang.SelectedIndex = -1;
                    txtSDT.ReadOnly = false;
                    txtDiaChi.ReadOnly = false;
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

            lblTongTien.Text = tongTienHang.ToString("N0") + " VNĐ";
        }

        private void CboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboKhachHang.SelectedItem;
                txtMaKH.Text = drv["MaKH"].ToString();
                txtSDT.Text = drv["SoDienThoai"].ToString();
                txtDiaChi.Text = drv["DiaChi"] != DBNull.Value && !string.IsNullOrEmpty(drv["DiaChi"].ToString())
                    ? drv["DiaChi"].ToString()
                    : "Khách tại quầy";
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
                decimal thanhToan = tongHang; // No discount

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

                    // Update customer info before updating invoice
                    string newSDT = txtSDT.Text.Trim();
                    string newDiaChi = txtDiaChi.Text.Trim();
                    _bll.UpdateKhachHangInfo(maKH, newSDT, newDiaChi);

                    bool success = _bll.CapNhatHoaDon(_idHoaDonVuaLuu, maKH, tongHang, 0, thanhToan, dtChiTietMoi);
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
                        txtSDT.ReadOnly = true;
                        txtDiaChi.ReadOnly = true;
                        cboKhachHang.Enabled = false;
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
                    _idHoaDonVuaLuu = _bll.ThanhToan(_maHDString, maKH, maNV, tongHang, 0, thanhToan, _dtChiTiet);

                    if (_idHoaDonVuaLuu > 0)
                    {
                        MessageBox.Show("Lưu thành công! Mã HĐ: " + txtMaHD.Text);
                        btnLuu.Enabled = false;
                        btnIn.Enabled = true;
                        cboKhachHang.Enabled = false;
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
                Excel.Application excelApp = null;
                Excel.Workbook workbook = null;
                Excel.Worksheet worksheet = null;

                try
                {
                    excelApp = new Excel.Application();
                    excelApp.Visible = false;
                    excelApp.DisplayAlerts = false;

                    workbook = excelApp.Workbooks.Add();
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    worksheet.Name = "HoaDon";

                    // Title
                    Excel.Range titleRange = worksheet.Range["A1", "D1"];
                    titleRange.Merge();
                    titleRange.Value2 = "HÓA ĐƠN BÁN HÀNG";
                    titleRange.Font.Bold = true;
                    titleRange.Font.Size = 16;
                    titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                    titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ReleaseObject(titleRange);

                    // Invoice information
                    worksheet.Cells[3, 1] = "Mã HĐ:";
                    worksheet.Cells[3, 2] = txtMaHD.Text;
                    worksheet.Cells[4, 1] = "Ngày:";
                    worksheet.Cells[4, 2] = txtNgayBan.Text;
                    worksheet.Cells[5, 1] = "Nhân viên:";
                    worksheet.Cells[5, 2] = txtNhanVien.Text;
                    worksheet.Cells[6, 1] = "Khách hàng:";
                    worksheet.Cells[6, 2] = cboKhachHang.Text;
                    worksheet.Cells[7, 1] = "SĐT:";
                    worksheet.Cells[7, 2] = txtSDT.Text;
                    worksheet.Cells[8, 1] = "Địa chỉ:";
                    worksheet.Cells[8, 2] = txtDiaChi.Text;

                    // Table header
                    int rowStart = 10;
                    worksheet.Cells[rowStart, 1] = "Tên Hàng";
                    worksheet.Cells[rowStart, 2] = "Số Lượng";
                    worksheet.Cells[rowStart, 3] = "Đơn Giá";
                    worksheet.Cells[rowStart, 4] = "Thành Tiền";

                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[rowStart, 1], worksheet.Cells[rowStart, 4]];
                    headerRange.Font.Bold = true;
                    headerRange.Font.Size = 11;
                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ReleaseObject(headerRange);

                    // Detail rows
                    int row = rowStart + 1;
                    foreach (DataGridViewRow dgvRow in dgvChiTiet.Rows)
                    {
                        if (dgvRow.IsNewRow) continue;
                        worksheet.Cells[row, 1] = dgvRow.Cells["TenSP"].Value?.ToString();
                        worksheet.Cells[row, 2] = Convert.ToInt32(dgvRow.Cells["SoLuong"].Value);
                        ((Excel.Range)worksheet.Cells[row, 2]).NumberFormat = "#,##0";
                        ((Excel.Range)worksheet.Cells[row, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                        worksheet.Cells[row, 3] = Convert.ToDecimal(dgvRow.Cells["DonGia"].Value);
                        ((Excel.Range)worksheet.Cells[row, 3]).NumberFormat = "#,##0";
                        ((Excel.Range)worksheet.Cells[row, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                        worksheet.Cells[row, 4] = Convert.ToDecimal(dgvRow.Cells["ThanhTien"].Value);
                        ((Excel.Range)worksheet.Cells[row, 4]).NumberFormat = "#,##0";
                        ((Excel.Range)worksheet.Cells[row, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        row++;
                    }

                    // Add borders to data range
                    Excel.Range dataRange = worksheet.Range[worksheet.Cells[rowStart, 1], worksheet.Cells[row - 1, 4]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);

                    // Total row
                    worksheet.Cells[row, 3] = "TỔNG THANH TOÁN:";
                    ((Excel.Range)worksheet.Cells[row, 3]).Font.Bold = true;
                    ((Excel.Range)worksheet.Cells[row, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    string tongTienStr = lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "");
                    worksheet.Cells[row, 4] = decimal.Parse(tongTienStr);
                    Excel.Range totalCell = (Excel.Range)worksheet.Cells[row, 4];
                    totalCell.Font.Bold = true;
                    totalCell.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    totalCell.NumberFormat = "#,##0";
                    totalCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ReleaseObject(totalCell);

                    // Auto-fit columns
                    worksheet.Columns.AutoFit();
                    worksheet.UsedRange.WrapText = false;

                    // Adjust column widths for better appearance
                    Excel.Range col1 = (Excel.Range)worksheet.Columns[1];
                    col1.ColumnWidth = Math.Max((double)col1.ColumnWidth * 1.1, 25);
                    ReleaseObject(col1);

                    Excel.Range col2 = (Excel.Range)worksheet.Columns[2];
                    col2.ColumnWidth = Math.Max((double)col2.ColumnWidth * 1.1, 12);
                    ReleaseObject(col2);

                    Excel.Range col3 = (Excel.Range)worksheet.Columns[3];
                    col3.ColumnWidth = Math.Max((double)col3.ColumnWidth * 1.1, 15);
                    ReleaseObject(col3);

                    Excel.Range col4 = (Excel.Range)worksheet.Columns[4];
                    col4.ColumnWidth = Math.Max((double)col4.ColumnWidth * 1.1, 15);
                    ReleaseObject(col4);

                    workbook.SaveAs(sfd.FileName);

                    MessageBox.Show("Xuất hóa đơn thành công!");
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message);
                }
                finally
                {
                    if (worksheet != null) ReleaseObject(worksheet);
                    if (workbook != null)
                    {
                        workbook.Close(false);
                        ReleaseObject(workbook);
                    }
                    if (excelApp != null)
                    {
                        excelApp.Quit();
                        ReleaseObject(excelApp);
                    }
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
                btnThemKhach.Visible = true;
                txtSDT.ReadOnly = false;
                txtDiaChi.ReadOnly = false;
                dgvChiTiet.ReadOnly = false;
                btnSua.Visible = false;
                btnLuu.Visible = true;
                btnLuu.Text = "Cập nhật";
                btnLuu.BackColor = Color.Blue;
            }
        }

        private void BtnHuyHoaDon_Click(object sender, EventArgs e)
        {
            // Check admin permission before canceling invoice
            bool isAdmin = _currentUser != null && _currentUser.IsAdmin;
            if (!isAdmin)
            {
                MessageBox.Show("Chỉ quản trị viên mới có quyền hủy hóa đơn!", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_idHoaDonVuaLuu == 0)
            {
                MessageBox.Show("Không có hóa đơn để hủy!");
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

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TxtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits, backspace, and control characters
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the character
            }
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
