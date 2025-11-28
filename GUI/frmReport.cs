using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using BTL_LTTQ.DTO;
using Excel = Microsoft.Office.Interop.Excel;

namespace BTL_LTTQ
{
    public partial class frmReport : Form
    {
        private readonly ReportService _reportService;
        private DashboardSummary _currentSummary;
        private DashboardSummary _previousSummary;
        private DataTable _trendTable;
        private DataTable _topProductsTable;
        private DataTable _topCustomersTable;
        private DateTime _lastFromDate;
        private DateTime _lastToDate;
        private TrendGrouping _lastTrendGrouping = TrendGrouping.Day;

        private enum CompareMode
        {
            Disabled,
            PreviousPeriod,
            YearOverYear
        }

        public frmReport()
        {
            InitializeComponent();
            _reportService = IsInDesignMode() ? null : new ReportService();
            InitializeFilterDefaults();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode())
            {
                return;
            }

            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.Value = DateTime.Today;
            cboQuickRange.SelectedItem = "Tháng này";
            LoadReports();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void LoadReports()
        {
            if (_reportService == null || IsInDesignMode())
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var fromDate = dtpFrom.Value.Date;
                var toDate = dtpTo.Value.Date;
                var topCount = (int)nudTopCount.Value;
                var grouping = GetSelectedTrendGrouping();
                var compareMode = GetSelectedCompareMode();
                var comparisonLabel = GetComparisonLabel(compareMode);

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var currentFrom = fromDate;
                var currentTo = toDate.AddDays(1).AddTicks(-1);
                DateTime? previousFrom = null;
                DateTime? previousTo = null;

                switch (compareMode)
                {
                    case CompareMode.PreviousPeriod:
                        var periodDays = (toDate - fromDate).Days + 1;
                        var prevEnd = fromDate.AddDays(-1);
                        var prevStart = prevEnd.AddDays(-periodDays + 1);
                        previousFrom = prevStart;
                        previousTo = prevEnd.AddDays(1).AddTicks(-1);
                        break;
                    case CompareMode.YearOverYear:
                        previousFrom = fromDate.AddYears(-1);
                        previousTo = toDate.AddYears(-1).AddDays(1).AddTicks(-1);
                        break;
                }

                var currentSummary = _reportService.GetDashboardSummary(currentFrom, currentTo);
                DashboardSummary previousSummary = null;

                if (previousFrom.HasValue && previousTo.HasValue)
                {
                    previousSummary = _reportService.GetDashboardSummary(previousFrom.Value, previousTo.Value);
                }

                _trendTable = _reportService.GetRevenueTrend(currentFrom, currentTo, grouping);
                _topProductsTable = _reportService.GetTopProducts(currentFrom, currentTo, topCount);
                _topCustomersTable = _reportService.GetTopCustomers(currentFrom, currentTo, topCount);
                _currentSummary = currentSummary;
                _previousSummary = previousSummary;
                _lastFromDate = fromDate;
                _lastToDate = toDate;
                _lastTrendGrouping = grouping;

                UpdateSummary(currentSummary, previousSummary, compareMode != CompareMode.Disabled, comparisonLabel);
                BindTrend(_trendTable);
                BindTopProducts(_topProductsTable);
                BindTopCustomers(_topCustomersTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải báo cáo.\nChi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateSummary(DashboardSummary current, DashboardSummary previous, bool showComparison, string comparisonLabel)
        {
            var safePrevious = previous ?? new DashboardSummary();

            lblRevenueValue.Text = FormatCurrency(current.TotalRevenue);
            lblRevenueSub.Text = $"Giá trị TB: {FormatCurrency(current.AverageOrderValue)}";
            lblRevenueDelta.Text = showComparison
                ? BuildDeltaText(current.TotalRevenue, safePrevious.TotalRevenue, FormatCurrency, comparisonLabel)
                : BuildComparisonDisabledText(comparisonLabel);

            lblProfitValue.Text = FormatCurrency(current.TotalProfit);
            var margin = current.TotalRevenue == 0 ? 0 : current.TotalProfit / current.TotalRevenue * 100;
            lblProfitSub.Text = $"Tỉ suất: {margin:0.##}%";
            lblProfitDelta.Text = showComparison
                ? BuildDeltaText(current.TotalProfit, safePrevious.TotalProfit, FormatCurrency, comparisonLabel)
                : BuildComparisonDisabledText(comparisonLabel);

            lblOrderValue.Text = current.TotalOrders.ToString("N0", CultureInfo.CurrentCulture);
            lblOrderSub.Text = $"Sản phẩm bán: {current.TotalItems:N0}";
            lblOrderDelta.Text = showComparison
                ? BuildDeltaText(current.TotalOrders, safePrevious.TotalOrders, FormatNumber, comparisonLabel)
                : BuildComparisonDisabledText(comparisonLabel);

            lblCustomerValue.Text = current.TotalCustomers.ToString("N0", CultureInfo.CurrentCulture);
            lblCustomerSub.Text = $"Khách mua: {current.TotalCustomers:N0}";
            lblCustomerDelta.Text = showComparison
                ? BuildDeltaText(current.TotalCustomers, safePrevious.TotalCustomers, FormatNumber, comparisonLabel)
                : BuildComparisonDisabledText(comparisonLabel);
        }

        private void BindTrend(DataTable table)
        {
            chartRevenue.DataSource = table;
            chartRevenue.Series["Doanh thu"].XValueMember = "Ngay";
            chartRevenue.Series["Doanh thu"].YValueMembers = "DoanhThu";
            chartRevenue.Series["Lợi nhuận"].XValueMember = "Ngay";
            chartRevenue.Series["Lợi nhuận"].YValueMembers = "LoiNhuan";
            chartRevenue.DataBind();
        }

        private void BindTopProducts(DataTable table)
        {
            dgvTopProducts.DataSource = table;
            if (dgvTopProducts.Columns.Count >= 3)
            {
                dgvTopProducts.Columns[0].HeaderText = "Sản phẩm";
                dgvTopProducts.Columns[1].HeaderText = "Số lượng";
                dgvTopProducts.Columns[2].HeaderText = "Doanh thu";
                dgvTopProducts.Columns[1].DefaultCellStyle.Format = "N0";
                dgvTopProducts.Columns[2].DefaultCellStyle.Format = "N0";
            }
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BindTopCustomers(DataTable table)
        {
            dgvTopCustomers.DataSource = table;
            if (dgvTopCustomers.Columns.Count >= 3)
            {
                dgvTopCustomers.Columns[0].HeaderText = "Khách hàng";
                dgvTopCustomers.Columns[1].HeaderText = "Số đơn";
                dgvTopCustomers.Columns[2].HeaderText = "Chi tiêu";
                dgvTopCustomers.Columns[1].DefaultCellStyle.Format = "N0";
                dgvTopCustomers.Columns[2].DefaultCellStyle.Format = "N0";
            }
            dgvTopCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private static string BuildDeltaText(decimal current, decimal previous, Func<decimal, string> formatter, string labelPrefix)
        {
            var diff = current - previous;

            if (diff == 0 && previous == 0)
            {
                return $"{labelPrefix}: Không đổi";
            }

            var sign = diff >= 0 ? "+" : "-";
            var formattedDiff = formatter(Math.Abs(diff));

            if (previous == 0)
            {
                return $"{labelPrefix}: {sign}{formattedDiff} (N/A)";
            }

            var percent = diff / previous;
            return $"{labelPrefix}: {sign}{formattedDiff} ({percent:+0.0%;-0.0%;0%})";
        }

        private static string BuildComparisonDisabledText(string labelPrefix)
        {
            return $"{labelPrefix}: Đang tắt";
        }

        private static string FormatCurrency(decimal value)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:N0} đ", value);
        }

        private void cboQuickRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsInDesignMode())
            {
                return;
            }

            ApplyQuickRangeSelection();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (IsInDesignMode())
            {
                return;
            }

            if (_currentSummary == null)
            {
                LoadReports();
                if (_currentSummary == null)
                {
                    MessageBox.Show("Chưa có dữ liệu để xuất báo cáo.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"BaoCao_{_lastFromDate:yyyyMMdd}-{_lastToDate:yyyyMMdd}.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    ExportReportToExcel(saveDialog.FileName);
                    MessageBox.Show("Xuất báo cáo thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xuất báo cáo.\nChi tiết: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeFilterDefaults()
        {
            if (cboQuickRange.Items.Count == 0)
            {
                cboQuickRange.Items.AddRange(new object[]
                {
                    "Tùy chỉnh",
                    "Hôm nay",
                    "7 ngày gần nhất",
                    "Tháng này",
                    "Quý này",
                    "Năm nay"
                });
            }

            if (cboGrouping.Items.Count == 0)
            {
                cboGrouping.Items.AddRange(new object[]
                {
                    new ComboItem("Theo ngày", TrendGrouping.Day),
                    new ComboItem("Theo tuần", TrendGrouping.Week),
                    new ComboItem("Theo tháng", TrendGrouping.Month)
                });
            }

            if (cboCompareMode.Items.Count == 0)
            {
                cboCompareMode.Items.AddRange(new object[]
                {
                    new ComboItem("Không so sánh", CompareMode.Disabled),
                    new ComboItem("So với kỳ trước", CompareMode.PreviousPeriod),
                    new ComboItem("So với cùng kỳ năm trước", CompareMode.YearOverYear)
                });
            }

            if (cboQuickRange.SelectedIndex < 0)
            {
                cboQuickRange.SelectedIndex = 2; // 7 ngày gần nhất
            }

            if (cboGrouping.SelectedIndex < 0)
            {
                cboGrouping.SelectedIndex = 0;
            }

            if (cboCompareMode.SelectedIndex < 0)
            {
                cboCompareMode.SelectedIndex = 1;
            }
        }

        private void ApplyQuickRangeSelection()
        {
            var selection = cboQuickRange.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selection) || selection.Equals("Tùy chỉnh", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var today = DateTime.Today;
            DateTime from = today;
            DateTime to = today;

            switch (selection)
            {
                case "Hôm nay":
                    from = today;
                    to = today;
                    break;
                case "7 ngày gần nhất":
                    from = today.AddDays(-6);
                    to = today;
                    break;
                case "Tháng này":
                    from = new DateTime(today.Year, today.Month, 1);
                    to = from.AddMonths(1).AddDays(-1);
                    break;
                case "Quý này":
                    var quarterStart = GetQuarterStart(today);
                    from = quarterStart;
                    to = quarterStart.AddMonths(3).AddDays(-1);
                    break;
                case "Năm nay":
                    from = new DateTime(today.Year, 1, 1);
                    to = new DateTime(today.Year, 12, 31);
                    break;
                default:
                    return;
            }

            dtpFrom.Value = from;
            dtpTo.Value = to;
        }

        private static DateTime GetQuarterStart(DateTime date)
        {
            var quarterIndex = (date.Month - 1) / 3;
            var startMonth = quarterIndex * 3 + 1;
            return new DateTime(date.Year, startMonth, 1);
        }

        private TrendGrouping GetSelectedTrendGrouping()
        {
            if (cboGrouping.SelectedItem is ComboItem item && item.Value is TrendGrouping grouping)
            {
                return grouping;
            }

            return TrendGrouping.Day;
        }

        private CompareMode GetSelectedCompareMode()
        {
            if (cboCompareMode.SelectedItem is ComboItem item && item.Value is CompareMode mode)
            {
                return mode;
            }

            return CompareMode.PreviousPeriod;
        }

        private static string GetComparisonLabel(CompareMode mode)
        {
            switch (mode)
            {
                case CompareMode.YearOverYear:
                    return "So với cùng kỳ năm trước";
                case CompareMode.Disabled:
                    return "So sánh";
                default:
                    return "So với kỳ trước";
            }
        }

        private void ExportReportToExcel(string filePath)
        {
            if (_currentSummary == null)
            {
                throw new InvalidOperationException("Không có dữ liệu để xuất.");
            }

            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add();

                BuildSummarySheet(workbook);
                BuildTrendSheet(workbook);
                BuildTopProductsSheet(workbook);
                BuildTopCustomersSheet(workbook);

                while (workbook.Worksheets.Count > 4)
                {
                    Excel.Worksheet defaultSheet = (Excel.Worksheet)workbook.Worksheets[workbook.Worksheets.Count];
                    defaultSheet.Delete();
                    ReleaseObject(defaultSheet);
                }

                workbook.SaveAs(filePath);
            }
            finally
            {
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

        private void BuildSummarySheet(Excel.Workbook workbook)
        {
            Excel.Worksheet ws = null;

            try
            {
                ws = (Excel.Worksheet)workbook.Worksheets.Add();
                ws.Name = "Tổng quan";

                Excel.Range titleRange = ws.Range["A1", "B1"];
                titleRange.Merge();
                titleRange.Value2 = "BÁO CÁO THỐNG KÊ BÁN HÀNG";
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(titleRange);

                ws.Cells[3, 1] = "Khoảng thời gian:";
                ws.Cells[3, 2] = $"{_lastFromDate:dd/MM/yyyy} - {_lastToDate:dd/MM/yyyy}";
                ws.Cells[4, 1] = "Nhóm xu hướng:";
                ws.Cells[4, 2] = GetTrendGroupingText(_lastTrendGrouping);

                int row = 6;
                Excel.Range headerRange = ws.Range[ws.Cells[row, 1], ws.Cells[row, 2]];
                headerRange.Font.Bold = true;
                headerRange.Font.Size = 11;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.Cells[row, 1] = "Chỉ số";
                ws.Cells[row, 2] = "Giá trị";
                ReleaseObject(headerRange);

                row++;
                ws.Cells[row, 1] = "Doanh thu";
                ws.Cells[row, 2] = _currentSummary.TotalRevenue;
                FormatCurrency(ws.Cells[row, 2]);

                row++;
                ws.Cells[row, 1] = "Lợi nhuận";
                ws.Cells[row, 2] = _currentSummary.TotalProfit;
                FormatCurrency(ws.Cells[row, 2]);

                row++;
                ws.Cells[row, 1] = "Đơn hàng";
                ws.Cells[row, 2] = _currentSummary.TotalOrders;
                FormatNumber(ws.Cells[row, 2]);

                row++;
                ws.Cells[row, 1] = "Khách hàng";
                ws.Cells[row, 2] = _currentSummary.TotalCustomers;
                FormatNumber(ws.Cells[row, 2]);

                if (_previousSummary != null)
                {
                    row += 2;
                    Excel.Range compareHeaderRange = ws.Range[ws.Cells[row, 1], ws.Cells[row, 2]];
                    compareHeaderRange.Font.Bold = true;
                    compareHeaderRange.Font.Size = 11;
                    compareHeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    compareHeaderRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.Cells[row, 1] = "So sánh kỳ";
                    ws.Cells[row, 2] = "Giá trị";
                    ReleaseObject(compareHeaderRange);

                    row++;
                    ws.Cells[row, 1] = "Doanh thu so sánh";
                    ws.Cells[row, 2] = _previousSummary.TotalRevenue;
                    FormatCurrency(ws.Cells[row, 2]);

                    row++;
                    ws.Cells[row, 1] = "Lợi nhuận so sánh";
                    ws.Cells[row, 2] = _previousSummary.TotalProfit;
                    FormatCurrency(ws.Cells[row, 2]);

                    row++;
                    ws.Cells[row, 1] = "Đơn hàng so sánh";
                    ws.Cells[row, 2] = _previousSummary.TotalOrders;
                    FormatNumber(ws.Cells[row, 2]);

                    row++;
                    ws.Cells[row, 1] = "Khách hàng so sánh";
                    ws.Cells[row, 2] = _previousSummary.TotalCustomers;
                    FormatNumber(ws.Cells[row, 2]);
                }

                Excel.Range dataRange = ws.Range[ws.Cells[6, 1], ws.Cells[row, 2]];
                dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                ReleaseObject(dataRange);

                ws.Columns.AutoFit();
                ws.UsedRange.WrapText = false;

                Excel.Range col1 = (Excel.Range)ws.Columns[1];
                col1.ColumnWidth = Math.Max((double)col1.ColumnWidth * 1.1, 20);
                ReleaseObject(col1);

                Excel.Range col2 = (Excel.Range)ws.Columns[2];
                col2.ColumnWidth = Math.Max((double)col2.ColumnWidth * 1.1, 15);
                ReleaseObject(col2);
            }
            finally
            {
                ReleaseObject(ws);
            }
        }

        private void BuildTrendSheet(Excel.Workbook workbook)
        {
            Excel.Worksheet ws = null;

            try
            {
                ws = (Excel.Worksheet)workbook.Worksheets.Add();
                ws.Name = "Xu hướng";

                var data = _trendTable ?? CreateEmptyTrendTable();

                Excel.Range titleRange = ws.Range["A1", "C1"];
                titleRange.Merge();
                titleRange.Value2 = "XU HƯỚNG DOANH THU VÀ LỢI NHUẬN";
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 14;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(titleRange);

                int headerRow = 3;
                ws.Cells[headerRow, 1] = "Ngày";
                ws.Cells[headerRow, 2] = "Doanh thu";
                ws.Cells[headerRow, 3] = "Lợi nhuận";

                Excel.Range headerRange = ws.Range[ws.Cells[headerRow, 1], ws.Cells[headerRow, 3]];
                headerRange.Font.Bold = true;
                headerRange.Font.Size = 11;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(headerRange);

                int row = headerRow + 1;
                foreach (DataRow dr in data.Rows)
                {
                    ws.Cells[row, 1] = Convert.ToDateTime(dr["Ngay"]).ToString("dd/MM/yyyy");
                    ws.Cells[row, 2] = Convert.ToDecimal(dr["DoanhThu"]);
                    FormatCurrency(ws.Cells[row, 2]);
                    ws.Cells[row, 3] = Convert.ToDecimal(dr["LoiNhuan"]);
                    FormatCurrency(ws.Cells[row, 3]);
                    row++;
                }

                if (data.Rows.Count > 0)
                {
                    Excel.Range dataRange = ws.Range[ws.Cells[headerRow, 1], ws.Cells[row - 1, 3]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);
                }

                ws.Columns.AutoFit();
                ws.UsedRange.WrapText = false;

                Excel.Range col1 = (Excel.Range)ws.Columns[1];
                col1.ColumnWidth = Math.Max((double)col1.ColumnWidth * 1.1, 15);
                ReleaseObject(col1);

                Excel.Range col2 = (Excel.Range)ws.Columns[2];
                col2.ColumnWidth = Math.Max((double)col2.ColumnWidth * 1.1, 18);
                ReleaseObject(col2);

                Excel.Range col3 = (Excel.Range)ws.Columns[3];
                col3.ColumnWidth = Math.Max((double)col3.ColumnWidth * 1.1, 18);
                ReleaseObject(col3);
            }
            finally
            {
                ReleaseObject(ws);
            }
        }

        private void BuildTopProductsSheet(Excel.Workbook workbook)
        {
            Excel.Worksheet ws = null;

            try
            {
                ws = (Excel.Worksheet)workbook.Worksheets.Add();
                ws.Name = "Top sản phẩm";

                var data = _topProductsTable ?? new DataTable();

                Excel.Range titleRange = ws.Range["A1", "C1"];
                titleRange.Merge();
                titleRange.Value2 = "TOP SẢN PHẨM BÁN CHẠY";
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 14;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(titleRange);

                int headerRow = 3;
                ws.Cells[headerRow, 1] = "Sản phẩm";
                ws.Cells[headerRow, 2] = "Số lượng bán";
                ws.Cells[headerRow, 3] = "Doanh thu";

                Excel.Range headerRange = ws.Range[ws.Cells[headerRow, 1], ws.Cells[headerRow, 3]];
                headerRange.Font.Bold = true;
                headerRange.Font.Size = 11;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(headerRange);

                int row = headerRow + 1;
                foreach (DataRow dr in data.Rows)
                {
                    ws.Cells[row, 1] = dr["SanPham"].ToString();
                    ws.Cells[row, 2] = Convert.ToDecimal(dr["SoLuongBan"]);
                    FormatNumber(ws.Cells[row, 2]);
                    ws.Cells[row, 3] = Convert.ToDecimal(dr["DoanhThu"]);
                    FormatCurrency(ws.Cells[row, 3]);
                    row++;
                }

                if (data.Rows.Count > 0)
                {
                    Excel.Range dataRange = ws.Range[ws.Cells[headerRow, 1], ws.Cells[row - 1, 3]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);
                }

                ws.Columns.AutoFit();
                ws.UsedRange.WrapText = false;

                Excel.Range col1 = (Excel.Range)ws.Columns[1];
                col1.ColumnWidth = Math.Max((double)col1.ColumnWidth * 1.1, 25);
                ReleaseObject(col1);

                Excel.Range col2 = (Excel.Range)ws.Columns[2];
                col2.ColumnWidth = Math.Max((double)col2.ColumnWidth * 1.1, 18);
                ReleaseObject(col2);

                Excel.Range col3 = (Excel.Range)ws.Columns[3];
                col3.ColumnWidth = Math.Max((double)col3.ColumnWidth * 1.1, 18);
                ReleaseObject(col3);
            }
            finally
            {
                ReleaseObject(ws);
            }
        }

        private void BuildTopCustomersSheet(Excel.Workbook workbook)
        {
            Excel.Worksheet ws = null;

            try
            {
                ws = (Excel.Worksheet)workbook.Worksheets.Add();
                ws.Name = "Top khách hàng";

                var data = _topCustomersTable ?? new DataTable();

                // Title
                Excel.Range titleRange = ws.Range["A1", "C1"];
                titleRange.Merge();
                titleRange.Value2 = "TOP KHÁCH HÀNG";
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 14;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(titleRange);

                int headerRow = 3;
                ws.Cells[headerRow, 1] = "Khách hàng";
                ws.Cells[headerRow, 2] = "Số đơn";
                ws.Cells[headerRow, 3] = "Tổng chi tiêu";

                Excel.Range headerRange = ws.Range[ws.Cells[headerRow, 1], ws.Cells[headerRow, 3]];
                headerRange.Font.Bold = true;
                headerRange.Font.Size = 11;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(headerRange);

                int row = headerRow + 1;
                foreach (DataRow dr in data.Rows)
                {
                    ws.Cells[row, 1] = dr["KhachHang"].ToString();
                    ws.Cells[row, 2] = Convert.ToDecimal(dr["SoDon"]);
                    FormatNumber(ws.Cells[row, 2]);
                    ws.Cells[row, 3] = Convert.ToDecimal(dr["TongChi"]);
                    FormatCurrency(ws.Cells[row, 3]);
                    row++;
                }

                if (data.Rows.Count > 0)
                {
                    Excel.Range dataRange = ws.Range[ws.Cells[headerRow, 1], ws.Cells[row - 1, 3]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);
                }

                ws.Columns.AutoFit();
                ws.UsedRange.WrapText = false;

                Excel.Range col1 = (Excel.Range)ws.Columns[1];
                col1.ColumnWidth = Math.Max((double)col1.ColumnWidth * 1.1, 25);
                ReleaseObject(col1);

                Excel.Range col2 = (Excel.Range)ws.Columns[2];
                col2.ColumnWidth = Math.Max((double)col2.ColumnWidth * 1.1, 15);
                ReleaseObject(col2);

                Excel.Range col3 = (Excel.Range)ws.Columns[3];
                col3.ColumnWidth = Math.Max((double)col3.ColumnWidth * 1.1, 20);
                ReleaseObject(col3);
            }
            finally
            {
                ReleaseObject(ws);
            }
        }

        private void FormatCurrency(Excel.Range cell)
        {
            cell.NumberFormat = "#,##0";
            cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        private void FormatNumber(Excel.Range cell)
        {
            cell.NumberFormat = "#,##0";
            cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        private string GetTrendGroupingText(TrendGrouping grouping)
        {
            switch (grouping)
            {
                case TrendGrouping.Day:
                    return "Theo ngày";
                case TrendGrouping.Week:
                    return "Theo tuần";
                case TrendGrouping.Month:
                    return "Theo tháng";
                default:
                    return grouping.ToString();
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

        private static DataTable CreateEmptyTrendTable()
        {
            var table = new DataTable();
            table.Columns.Add("Ngay", typeof(DateTime));
            table.Columns.Add("DoanhThu", typeof(decimal));
            table.Columns.Add("LoiNhuan", typeof(decimal));
            return table;
        }

        private sealed class ComboItem
        {
            public string Text { get; }
            public object Value { get; }

            public ComboItem(string text, object value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private static string FormatNumber(decimal value)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:N0}", value);
        }

        /// <summary>
        /// Kiểm tra xem form có đang chạy trong Visual Studio Designer không
        /// </summary>
        private static bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}

