using System;
using System.Data;
using BTL_LTTQ.DTO;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ.DAL
{
    public class KhachHangDAL
    {
        private DataProcesser db = new DataProcesser();

        public DataTable GetAllKhachHang()
        {
            string sql = "SELECT MaKH, HoTen, SoDienThoai, TongChiTieu, HangThanhVien FROM KhachHang";
            return db.ExecuteQuery(sql);
        }

        public DataTable SearchKhachHang(string keyword)
        {
            string sql = $@"SELECT MaKH, HoTen, SoDienThoai, TongChiTieu, HangThanhVien 
                            FROM KhachHang 
                            WHERE HoTen LIKE N'%{keyword}%' OR SoDienThoai LIKE '%{keyword}%'";
            return db.ExecuteQuery(sql);
        }

        public bool AddKhachHang(KhachHangDTO kh)
        {
            string sql = $@"INSERT INTO KhachHang(HoTen, SoDienThoai, TongChiTieu, HangThanhVien) 
                            VALUES (N'{kh.HoTen}', '{kh.SoDienThoai}', 0, 'New')";
            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool UpdateKhachHang(KhachHangDTO kh)
        {
            string sql = $@"UPDATE KhachHang 
                            SET HoTen = N'{kh.HoTen}', SoDienThoai = '{kh.SoDienThoai}' 
                            WHERE MaKH = {kh.MaKH}";
            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool DeleteKhachHang(int maKH)
        {
            string sql = $"DELETE FROM KhachHang WHERE MaKH = {maKH}";
            return db.ExecuteNonQuery(sql) > 0;
        }
    }
}