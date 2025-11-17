using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ
{
    public partial class frmReport : Form
    {
        private readonly ReportService _reportService;

        public frmReport()
        {
            InitializeComponent();
            _reportService = IsInDesignMode() ? null : new ReportService();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode())
            {
                return;
            }

            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.Value = DateTime.Today;
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

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var periodDays = (toDate - fromDate).Days + 1;
                var previousEndDate = fromDate.AddDays(-1);
                var previousStartDate = previousEndDate.AddDays(-periodDays + 1);

                var currentFrom = fromDate;
                var currentTo = toDate.AddDays(1).AddTicks(-1);
                var previousFrom = previousStartDate;
                var previousTo = previousEndDate.AddDays(1).AddTicks(-1);

                var currentSummary = _reportService.GetDashboardSummary(currentFrom, currentTo);
                var previousSummary = _reportService.GetDashboardSummary(previousFrom, previousTo);

                UpdateSummary(currentSummary, previousSummary);

                var trendTable = _reportService.GetRevenueTrend(currentFrom, currentTo);
                BindTrend(trendTable);

                var topProductsTable = _reportService.GetTopProducts(currentFrom, currentTo);
                BindTopProducts(topProductsTable);

                var topCustomersTable = _reportService.GetTopCustomers(currentFrom, currentTo);
                BindTopCustomers(topCustomersTable);
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

        private void UpdateSummary(DashboardSummary current, DashboardSummary previous)
        {
            var safePrevious = previous ?? new DashboardSummary();

            lblRevenueValue.Text = FormatCurrency(current.TotalRevenue);
            lblRevenueSub.Text = $"Giá trị TB: {FormatCurrency(current.AverageOrderValue)}";
            lblRevenueDelta.Text = BuildDeltaText(current.TotalRevenue, safePrevious.TotalRevenue, FormatCurrency);

            lblProfitValue.Text = FormatCurrency(current.TotalProfit);
            var margin = current.TotalRevenue == 0 ? 0 : current.TotalProfit / current.TotalRevenue * 100;
            lblProfitSub.Text = $"Tỉ suất: {margin:0.##}%";
            lblProfitDelta.Text = BuildDeltaText(current.TotalProfit, safePrevious.TotalProfit, FormatCurrency);

            lblOrderValue.Text = current.TotalOrders.ToString("N0", CultureInfo.CurrentCulture);
            lblOrderSub.Text = $"Sản phẩm bán: {current.TotalItems:N0}";
            lblOrderDelta.Text = BuildDeltaText(current.TotalOrders, safePrevious.TotalOrders, FormatNumber);

            lblCustomerValue.Text = current.TotalCustomers.ToString("N0", CultureInfo.CurrentCulture);
            lblCustomerSub.Text = $"Khách mua: {current.TotalCustomers:N0}";
            lblCustomerDelta.Text = BuildDeltaText(current.TotalCustomers, safePrevious.TotalCustomers, FormatNumber);
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

        private static string BuildDeltaText(decimal current, decimal previous, Func<decimal, string> formatter)
        {
            var diff = current - previous;

            if (diff == 0 && previous == 0)
            {
                return "So với kỳ trước: Không đổi";
            }

            var sign = diff >= 0 ? "+" : "-";
            var formattedDiff = formatter(Math.Abs(diff));

            if (previous == 0)
            {
                return $"So với kỳ trước: {sign}{formattedDiff} (N/A)";
            }

            var percent = diff / previous;
            return $"So với kỳ trước: {sign}{formattedDiff} ({percent:+0.0%;-0.0%;0%})";
        }

        private static string FormatCurrency(decimal value)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:N0} đ", value);
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

