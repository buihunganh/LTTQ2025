using System;
using System.Data;
using System.Data.SqlClient;
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

        // 7. Lấy lịch sử nhập hàng theo MaCTSP (từ bảng LoHang để có số lượng còn lại)
        public DataTable GetLichSuNhapByMaCTSP(int maCTSP)
        {
            using (var dal = new DataProcesser())
            {
                string sql = @"SELECT lh.SoLuongBanDau AS SoLuong,
                                      lh.GiaNhap, 
                                      lh.NgayNhap,
                                      lh.SoLuongConLai,
                                      ISNULL(ncc.TenNCC, N'Không xác định') AS NhaCungCap
                               FROM LoHang lh
                               LEFT JOIN ChiTietPhieuNhap ctpn ON lh.MaCTPN = ctpn.MaCTPN
                               LEFT JOIN PhieuNhap pn ON lh.MaPN = pn.MaPN
                               LEFT JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                               WHERE lh.MaCTSP = @MaCTSP
                                 AND lh.MaPN IS NOT NULL
                               ORDER BY lh.NgayNhap DESC";
                return dal.ExecuteQuery(sql, CommandType.Text, 
                    new System.Data.SqlClient.SqlParameter("@MaCTSP", maCTSP));
            }
        }

        // 8. Lấy số lượng tồn kho hiện tại
        public int GetSoLuongTon(int maCTSP)
        {
            using (var dal = new DataProcesser())
            {
                string sql = @"SELECT SoLuongTon FROM ChiTietSanPham WHERE MaCTSP = @MaCTSP";
                var result = dal.ExecuteQuery(sql, CommandType.Text, 
                    new System.Data.SqlClient.SqlParameter("@MaCTSP", maCTSP));
                
                if (result != null && result.Rows.Count > 0 && result.Rows[0]["SoLuongTon"] != DBNull.Value)
                {
                    return Convert.ToInt32(result.Rows[0]["SoLuongTon"]);
                }
                return 0;
            }
        }
    }
}