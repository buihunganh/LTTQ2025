using System;
using System.Data;
using BTL_LTTQ.DTO;
using BTL_LTTQ.DAL;
using BTL_LTTQ.BLL;

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

        public KhachHangDTO GetKhachHangByInfo(string ten, string sdt)
        {
            string sql = $"SELECT * FROM KhachHang WHERE HoTen = N'{ten}' AND SoDienThoai = '{sdt}' AND TrangThai = 1";
            DataTable dt = db.ExecuteQuery(sql);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new KhachHangDTO
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    TongChiTieu = Convert.ToDecimal(row["TongChiTieu"]),
                    HangThanhVien = row["HangThanhVien"].ToString()
                };
            }
            return null;
        }

        public bool UpdateKhachHang(KhachHangDTO kh)
        {
            string sql = $@"UPDATE KhachHang 
                    SET HoTen = N'{kh.HoTen}', 
                        SoDienThoai = '{kh.SoDienThoai}',
                        TongChiTieu = {kh.TongChiTieu},  
                        HangThanhVien = N'{kh.HangThanhVien}'
                    WHERE MaKH = {kh.MaKH}";

            return db.ExecuteNonQuery(sql) > 0;
        }

        public DataTable SearchKhachHang(string keyword, string rank)
        {
            string sql = $@"SELECT MaKH, HoTen, SoDienThoai, TongChiTieu, HangThanhVien 
                    FROM KhachHang 
                    WHERE TrangThai = 1 AND (HoTen LIKE N'%{keyword}%' OR SoDienThoai LIKE '%{keyword}%')";

            if (!string.IsNullOrEmpty(rank) && rank != "Tất cả")
            {
                sql += $" AND HangThanhVien = N'{rank}'";
            }

            return db.ExecuteQuery(sql);
        }

        public bool AddKhachHang(KhachHangDTO kh)
        {
            string sql = $@"INSERT INTO KhachHang(HoTen, SoDienThoai, TongChiTieu, HangThanhVien) 
                            VALUES (N'{kh.HoTen}', '{kh.SoDienThoai}', '{kh.TongChiTieu}', N'{kh.HangThanhVien}')";
            return db.ExecuteNonQuery(sql) > 0;
        }
        public bool DeleteKhachHang(int maKH)
        {
            string sql = $"DELETE FROM KhachHang WHERE MaKH = {maKH}";
            return db.ExecuteNonQuery(sql) > 0;
        }
    }
}