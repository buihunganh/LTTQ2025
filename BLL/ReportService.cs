using System;
using System.Data;
using System.Data.SqlClient;
using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ.BLL
{
    public class ReportService
    {
        public DashboardSummary GetDashboardSummary(DateTime fromDate, DateTime toDate)
        {
            const string summarySql = @"
                SELECT
                    COUNT(DISTINCT hd.MaHD) AS TotalOrders,
                    ISNULL(SUM(hd.ThanhToan), 0) AS TotalRevenue,
                    ISNULL(SUM(cthd.SoLuong), 0) AS TotalItems,
                    COUNT(DISTINCT hd.MaKH) AS TotalCustomers,
                    ISNULL(SUM((cthd.DonGia - ISNULL(cthd.GiaVon, 0)) * cthd.SoLuong), 0) AS TotalProfit
                FROM HoaDon hd
                LEFT JOIN ChiTietHoaDon cthd ON hd.MaHD = cthd.MaHD
                WHERE hd.NgayLap BETWEEN @fromDate AND @toDate";

            using (var db = new DataProcesser())
            {
                var result = db.ExecuteQuery(summarySql, CommandType.Text,
                    new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = fromDate },
                    new SqlParameter("@toDate", SqlDbType.DateTime) { Value = toDate });

                if (result.Rows.Count == 0)
                {
                    return new DashboardSummary();
                }

                var row = result.Rows[0];

                return new DashboardSummary
                {
                    TotalOrders = row.Field<int?>("TotalOrders") ?? 0,
                    TotalRevenue = row.Field<decimal?>("TotalRevenue") ?? 0,
                    TotalItems = row.Field<int?>("TotalItems") ?? 0,
                    TotalCustomers = row.Field<int?>("TotalCustomers") ?? 0,
                    TotalProfit = row.Field<decimal?>("TotalProfit") ?? 0
                };
            }
        }

        public DataTable GetRevenueTrend(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
                SELECT
                    CAST(hd.NgayLap AS DATE) AS Ngay,
                    ISNULL(SUM(hd.ThanhToan), 0) AS DoanhThu,
                    ISNULL(SUM((cthd.DonGia - ISNULL(cthd.GiaVon, 0)) * cthd.SoLuong), 0) AS LoiNhuan
                FROM HoaDon hd
                LEFT JOIN ChiTietHoaDon cthd ON hd.MaHD = cthd.MaHD
                WHERE hd.NgayLap BETWEEN @fromDate AND @toDate
                GROUP BY CAST(hd.NgayLap AS DATE)
                ORDER BY Ngay";

            using (var db = new DataProcesser())
            {
                return db.ExecuteQuery(sql, CommandType.Text,
                    new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = fromDate },
                    new SqlParameter("@toDate", SqlDbType.DateTime) { Value = toDate });
            }
        }

        public DataTable GetTopProducts(DateTime fromDate, DateTime toDate, int top = 5)
        {
            const string sql = @"
                SELECT TOP (@top)
                    sp.TenGiay AS SanPham,
                    ISNULL(SUM(cthd.SoLuong), 0) AS SoLuongBan,
                    ISNULL(SUM(cthd.ThanhTien), 0) AS DoanhThu
                FROM ChiTietHoaDon cthd
                INNER JOIN HoaDon hd ON cthd.MaHD = hd.MaHD
                INNER JOIN ChiTietSanPham ctsp ON cthd.MaCTSP = ctsp.MaCTSP
                INNER JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                WHERE hd.NgayLap BETWEEN @fromDate AND @toDate
                GROUP BY sp.TenGiay
                ORDER BY SoLuongBan DESC, DoanhThu DESC";

            using (var db = new DataProcesser())
            {
                return db.ExecuteQuery(sql, CommandType.Text,
                    new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = fromDate },
                    new SqlParameter("@toDate", SqlDbType.DateTime) { Value = toDate },
                    new SqlParameter("@top", SqlDbType.Int) { Value = top });
            }
        }

        public DataTable GetTopCustomers(DateTime fromDate, DateTime toDate, int top = 5)
        {
            const string sql = @"
                SELECT TOP (@top)
                    kh.HoTen AS KhachHang,
                    COUNT(DISTINCT hd.MaHD) AS SoDon,
                    ISNULL(SUM(hd.ThanhToan), 0) AS TongChi
                FROM HoaDon hd
                INNER JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                WHERE hd.NgayLap BETWEEN @fromDate AND @toDate
                GROUP BY kh.HoTen
                ORDER BY TongChi DESC";

            using (var db = new DataProcesser())
            {
                return db.ExecuteQuery(sql, CommandType.Text,
                    new SqlParameter("@fromDate", SqlDbType.DateTime) { Value = fromDate },
                    new SqlParameter("@toDate", SqlDbType.DateTime) { Value = toDate },
                    new SqlParameter("@top", SqlDbType.Int) { Value = top });
            }
        }

        public DashboardOverview GetTodayOverview(DateTime today)
        {
            var startOfDay = today.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            using (var db = new DataProcesser())
            {
                var revenue = db.ExecuteScalar(@"
                    SELECT ISNULL(SUM(ThanhToan), 0)
                    FROM HoaDon
                    WHERE NgayLap BETWEEN @from AND @to",
                    CommandType.Text,
                    new SqlParameter("@from", SqlDbType.DateTime) { Value = startOfDay },
                    new SqlParameter("@to", SqlDbType.DateTime) { Value = endOfDay });

                var orders = db.ExecuteScalar(@"
                    SELECT COUNT(*)
                    FROM HoaDon
                    WHERE NgayLap BETWEEN @from AND @to",
                    CommandType.Text,
                    new SqlParameter("@from", SqlDbType.DateTime) { Value = startOfDay },
                    new SqlParameter("@to", SqlDbType.DateTime) { Value = endOfDay });

                var topProductTable = db.ExecuteQuery(@"
                    SELECT TOP 1 sp.TenGiay
                    FROM ChiTietHoaDon cthd
                    INNER JOIN HoaDon hd ON hd.MaHD = cthd.MaHD
                    INNER JOIN ChiTietSanPham ctsp ON cthd.MaCTSP = ctsp.MaCTSP
                    INNER JOIN SanPham sp ON sp.MaSP = ctsp.MaSP
                    WHERE hd.NgayLap BETWEEN @from AND @to
                    GROUP BY sp.TenGiay
                    ORDER BY SUM(cthd.SoLuong) DESC",
                    CommandType.Text,
                    new SqlParameter("@from", SqlDbType.DateTime) { Value = startOfDay },
                    new SqlParameter("@to", SqlDbType.DateTime) { Value = endOfDay });

                var lowStockTable = db.ExecuteQuery(@"
                    SELECT TOP 1 sp.TenGiay
                    FROM ChiTietSanPham ctsp
                    INNER JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                    WHERE ISNULL(ctsp.SoLuongTon, 0) <= 5
                    ORDER BY ctsp.SoLuongTon ASC",
                    CommandType.Text);

                return new DashboardOverview
                {
                    TodayRevenue = revenue == null ? 0 : Convert.ToDecimal(revenue),
                    TodayOrders = orders == null ? 0 : Convert.ToInt32(orders),
                    TopProductName = topProductTable.Rows.Count > 0
                        ? topProductTable.Rows[0][0].ToString()
                        : "Chưa có dữ liệu",
                    LowStockAlert = lowStockTable.Rows.Count > 0
                        ? lowStockTable.Rows[0][0].ToString()
                        : "Kho ổn định"
                };
            }
        }
    }
}

