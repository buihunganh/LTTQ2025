using System.Data;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ.BLL
{
    public class SalesBLL
    {
        // 1. Lấy danh sách sản phẩm để bán (Chỉ lấy cái nào còn tồn kho > 0)
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
        // 2. Lấy danh sách khách hàng
        public DataTable GetKhachHang()
        {
            using (var dal = new DataProcesser())
            {
                return dal.ExecuteQuery("SELECT MaKH, HoTen, SoDienThoai FROM KhachHang WHERE TrangThai = 1");
            }
        }

        // Mở file SalesBLL.cs
      

        public int ThanhToan(int maKH, int maNV, decimal tongTien, decimal giamGia, decimal thanhToan, DataTable dtChiTiet)
        {
            using (var dal = new DataProcesser())
            {
                // Trả về int ID từ DAL
                return dal.BanHangTransaction(maKH, maNV, tongTien, giamGia, thanhToan, dtChiTiet);
            }
        }
    }
}