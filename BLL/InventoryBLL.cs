using System;
using System.Data;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ.BLL
{
    public class InventoryBLL
    {
        // 1. Lấy danh sách Nhà Cung Cấp
        public DataTable GetNhaCungCap()
        {
            using (var dal = new DataProcesser())
            {
                return dal.ExecuteQuery("SELECT MaNCC, TenNCC FROM NhaCungCap WHERE TrangThai = 1");
            }
        }

        // 2. Lấy danh sách Sản phẩm (Kèm Size, Màu để hiển thị rõ)
        public DataTable GetSanPhamBienThe()
        {
            using (var dal = new DataProcesser())
            {
                string sql = @"SELECT ct.MaCTSP, 
                                      sp.TenGiay + ' (' + sz.KichCo + ' - ' + ms.TenMau + ')' AS TenHienThi, 
                                      ct.GiaNhap 
                               FROM ChiTietSanPham ct 
                               JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               JOIN SizeGiay sz ON ct.MaSize = sz.MaSize
                               JOIN MauSac ms ON ct.MaMau = ms.MaMau
                               WHERE ct.TrangThai = 1";
                return dal.ExecuteQuery(sql);
            }
        }

        // 3. Lấy cảnh báo tồn kho thấp (< 5)
        public DataTable GetCanhBaoTonKho()
        {
            using (var dal = new DataProcesser())
            {
                string sql = @"SELECT sp.TenGiay, sz.KichCo, ms.TenMau, ct.SoLuongTon 
                               FROM ChiTietSanPham ct 
                               JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               JOIN SizeGiay sz ON ct.MaSize = sz.MaSize 
                               JOIN MauSac ms ON ct.MaMau = ms.MaMau
                               WHERE ct.SoLuongTon < 5";
                return dal.ExecuteQuery(sql);
            }
        }

        // 4. Lấy lịch sử nhập hàng (50 phiếu gần nhất)
        public DataTable GetLichSuNhap()
        {
            using (var dal = new DataProcesser())
            {
                return dal.ExecuteQuery(@"SELECT TOP 50 pn.MaPhieuNhap, ncc.TenNCC, pn.NgayNhap, pn.TongTien 
                                          FROM PhieuNhap pn
                                          LEFT JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC 
                                          ORDER BY pn.NgayNhap DESC");
            }
        }

        // 5. Lấy tất cả tồn kho
        public DataTable GetAllInventory()
        {
            using (var dal = new DataProcesser())
            {
                string sql = @"SELECT ct.MaCTSP, 
                                      sp.TenGiay + ' (' + sz.KichCo + ' - ' + ms.TenMau + ')' AS TenSP, 
                                      ct.SoLuongTon 
                               FROM ChiTietSanPham ct 
                               JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               JOIN SizeGiay sz ON ct.MaSize = sz.MaSize
                               JOIN MauSac ms ON ct.MaMau = ms.MaMau
                               WHERE ct.TrangThai = 1
                               ORDER BY sp.TenGiay, sz.KichCo, ms.TenMau";
                return dal.ExecuteQuery(sql);
            }
        }

        // 6. Lưu phiếu nhập (Gọi Transaction bên DAL)
        public bool LuuPhieuNhap(int maNCC, int maNV, decimal tongTien, DataTable dtChiTiet)
        {
            using (var dal = new DataProcesser())
            {
                return dal.NhapHangTransaction(maNCC, maNV, tongTien, dtChiTiet);
            }
        }
    }
}