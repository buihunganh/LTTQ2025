using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using Excel = Microsoft.Office.Interop.Excel;

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
            if (!string.IsNullOrEmpty(cleanMoney))
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
                    worksheet.Name = "KhachHang";

                    DataTable dt = dgvKhachHang.DataSource as DataTable;
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất!");
                        return;
                    }

                    // Title
                    int colCount = dt.Columns.Count;
                    string lastCol = GetExcelColumnName(colCount);
                    Excel.Range titleRange = worksheet.Range["A1", $"{lastCol}1"];
                    titleRange.Merge();
                    titleRange.Value2 = "DANH SÁCH KHÁCH HÀNG";
                    titleRange.Font.Bold = true;
                    titleRange.Font.Size = 16;
                    titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                    titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ReleaseObject(titleRange);

                    // Headers
                    int headerRow = 3;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        worksheet.Cells[headerRow, i + 1] = dt.Columns[i].ColumnName;
                    }

                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[headerRow, colCount]];
                    headerRange.Font.Bold = true;
                    headerRange.Font.Size = 11;
                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ReleaseObject(headerRange);

                    // Data rows
                    int row = headerRow + 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            var value = dt.Rows[i][j];
                            if (value != null && value != DBNull.Value)
                            {
                                worksheet.Cells[row, j + 1] = value.ToString();

                                // Format currency columns
                                if (dt.Columns[j].ColumnName == "TongChiTieu")
                                {
                                    ((Excel.Range)worksheet.Cells[row, j + 1]).NumberFormat = "#,##0";
                                    ((Excel.Range)worksheet.Cells[row, j + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                }
                            }
                        }
                        row++;
                    }

                    // Add borders to data range
                    Excel.Range dataRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[row - 1, colCount]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);

                    // Auto-fit columns
                    worksheet.Columns.AutoFit();
                    worksheet.UsedRange.WrapText = false;

                    // Adjust column widths for better appearance
                    for (int i = 1; i <= colCount; i++)
                    {
                        Excel.Range col = (Excel.Range)worksheet.Columns[i];
                        col.ColumnWidth = Math.Max((double)col.ColumnWidth * 1.1, 12);
                        ReleaseObject(col);
                    }

                    workbook.SaveAs(sfd.FileName);

                    MessageBox.Show("Xuất file thành công!");
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

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
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