using BTL_LTTQ.DAL;
using System;
using System.Data;

namespace BTL_LTTQ.BLL
{
    public class SalesBLL
    {
        public DataTable GetSanPhamBanHang()
        {
            using (var dal = new DataProcesser())
            {
                string sql = @"SELECT ct.MaCTSP, 
                                      sp.TenGiay + ' (' + sz.KichCo + ' - ' + ms.TenMau + ')' AS TenHienThi, 
                                      ct.GiaBan,
                                      ct.SoLuongTon
                               FROM ChiTietSanPham ct 
                               JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               JOIN SizeGiay sz ON ct.MaSize = sz.MaSize
                               JOIN MauSac ms ON ct.MaMau = ms.MaMau
                               WHERE ct.TrangThai = 1 AND ct.SoLuongTon > 0";
                return dal.ExecuteQuery(sql);
            }
        }
        public int QuickAddCustomer(string ten, string sdt)
        {
            using (var dal = new DataProcesser())
            {
                return dal.ThemKhachHangNhanh(ten, sdt);
            }
        }
        public DataTable GetInvoiceDetail(int maHD)
        {
            using (var dal = new DataProcesser()) return dal.GetChiTietHoaDon(maHD);
        }

        public DataTable GetInvoiceGeneral(int maHD)
        {
            using (var dal = new DataProcesser()) return dal.GetThongTinChungHoaDon(maHD);
        }
        public DataTable GetKhachHang()
        {
            using (var dal = new DataProcesser())
            {
                return dal.ExecuteQuery("SELECT MaKH, HoTen, SoDienThoai, ISNULL(DiaChi, '') AS DiaChi FROM KhachHang WHERE TrangThai = 1");
            }
        }

        public int ThanhToan(string maHoaDon, int maKH, int maNV, decimal tongTien, decimal giamGia, decimal thanhToan, DataTable dtChiTiet)
        {
            using (var dal = new DataProcesser())
            {
                // Trả về int ID từ DAL
                return dal.BanHangTransaction(maHoaDon, maKH, maNV, tongTien, giamGia, thanhToan, dtChiTiet);
            }
        }
        public DataTable FindInvoices(DateTime from, DateTime to, string nv, string kh)
        {
            using (var dal = new DataProcesser())
            {
                return dal.TimKiemHoaDon(from, to, nv, kh);
            }
        }

        public bool HuyHoaDon(int maHD)
        {
            using (var dal = new DataProcesser())
            {
                return dal.HuyHoaDonTransaction(maHD);
            }
        }

        public DataTable GetDanhSachMaHoaDon()
        {
            using (var dal = new DataProcesser())
            {
                return dal.ExecuteQuery("SELECT MaHD, MaHoaDon FROM HoaDon ORDER BY NgayLap DESC");
            }
        }

        public bool CapNhatHoaDon(int maHD, int maKH, decimal tongTien, decimal giamGia, decimal thanhToan, DataTable dtChiTiet)
        {
            using (var dal = new DataProcesser())
            {
                return dal.CapNhatHoaDonTransaction(maHD, maKH, tongTien, giamGia, thanhToan, dtChiTiet);
            }
        }

        public bool UpdateKhachHangInfo(int maKH, string sdt, string diaChi)
        {
            using (var dal = new DataProcesser())
            {
                string sql = $@"UPDATE KhachHang 
                                SET SoDienThoai = '{sdt}', 
                                    DiaChi = N'{diaChi}'
                                WHERE MaKH = {maKH}";
                return dal.ExecuteNonQuery(sql) > 0;
            }
        }
    }
}
