using System;
using System.Data;
using BTL_LTTQ.DTO;
using BTL_LTTQ.DAL; 

namespace BTL_LTTQ.DAL
{
    public class NhanVienDAL
    {
        private DataProcesser db = new DataProcesser();

        public DataTable GetAllNhanVien()
        {
            string sql = "SELECT MaNV, HoTen, TaiKhoan, IsAdmin, TrangThai, SoDienThoai, Email, DiaChi, NgayVaoLam FROM NhanVien";
            return db.ExecuteQuery(sql);
        }

        public DataTable SearchNhanVien(string keyword, int status)
        {
            string sql = $@"SELECT MaNV, HoTen, TaiKhoan, IsAdmin, TrangThai, SoDienThoai, Email, DiaChi, NgayVaoLam 
                    FROM NhanVien 
                    WHERE (HoTen LIKE N'%{keyword}%' OR SoDienThoai LIKE '%{keyword}%')";

            if (status != -1)
            {
                sql += $" AND TrangThai = {status}";
            }

            return db.ExecuteQuery(sql);
        }

        public bool AddNhanVien(NhanVienDTO nv)
        {
            string ngayVL = nv.NgayVaoLam.HasValue ? $"'{nv.NgayVaoLam.Value.ToString("yyyy-MM-dd")}'" : "NULL";

            string sql = $@"INSERT INTO NhanVien(HoTen, TaiKhoan, MatKhau, IsAdmin, TrangThai, SoDienThoai, Email, DiaChi, NgayVaoLam) 
                            VALUES (N'{nv.HoTen}', '{nv.TaiKhoan}', '{nv.MatKhau}', '{Convert.ToByte(nv.IsAdmin)}', '{Convert.ToByte(nv.TrangThai)}', '{nv.SoDienThoai}', '{nv.Email}', N'{nv.DiaChi}', {ngayVL})";

            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool UpdateNhanVien(NhanVienDTO nv)
        {
            string ngayVL = nv.NgayVaoLam.HasValue ? $"'{nv.NgayVaoLam.Value.ToString("yyyy-MM-dd")}'" : "NULL";

            string sql = $@"UPDATE NhanVien 
                            SET HoTen = N'{nv.HoTen}', 
                                IsAdmin = '{Convert.ToByte(nv.IsAdmin)}', 
                                TrangThai = '{Convert.ToByte(nv.TrangThai)}',
                                SoDienThoai = '{nv.SoDienThoai}',
                                Email = '{nv.Email}',
                                DiaChi = N'{nv.DiaChi}',
                                NgayVaoLam = {ngayVL}
                            WHERE MaNV = {nv.MaNV}";

            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool UpdateMatKhau(int maNV, string matKhauMoi)
        {
            string sql = $@"UPDATE NhanVien 
                            SET MatKhau = '{matKhauMoi}'
                            WHERE MaNV = {maNV}";
            return db.ExecuteNonQuery(sql) > 0;
        }

        public string GetMatKhau(int maNV)
        {
            string sql = $"SELECT MatKhau FROM NhanVien WHERE MaNV = {maNV}";
            DataTable dt = db.ExecuteQuery(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0]["MatKhau"] != DBNull.Value)
            {
                return dt.Rows[0]["MatKhau"].ToString();
            }
            return string.Empty;
        }

        public bool DisableNhanVien(int maNV)
        {
            string sql = $"UPDATE NhanVien SET TrangThai = 0 WHERE MaNV = {maNV}";
            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool DeleteNhanVien(int maNV)
        {
            // Kiểm tra xem nhân viên có đang được sử dụng trong hóa đơn không
            string sqlCheck = "SELECT COUNT(*) FROM HoaDon WHERE MaNV = @MaNV";
            var checkParam = new System.Data.SqlClient.SqlParameter("@MaNV", maNV);
            var count = db.ExecuteScalar(sqlCheck, System.Data.CommandType.Text, checkParam);
            
            bool hasHoaDon = Convert.ToInt32(count) > 0;

            // Kiểm tra xem nhân viên có đang được sử dụng trong phiếu nhập không
            string sqlCheckPN = "SELECT COUNT(*) FROM PhieuNhap WHERE MaNV = @MaNV";
            var countPN = db.ExecuteScalar(sqlCheckPN, System.Data.CommandType.Text, checkParam);
            
            bool hasPhieuNhap = Convert.ToInt32(countPN) > 0;

            // Nếu có hóa đơn hoặc phiếu nhập => chỉ vô hiệu hóa (soft delete)
            if (hasHoaDon || hasPhieuNhap)
            {
                string sql = "UPDATE NhanVien SET TrangThai = 0 WHERE MaNV = @MaNV";
                return db.ExecuteNonQuery(sql, System.Data.CommandType.Text, checkParam) > 0;
            }

            // Nếu không có ràng buộc => xóa hẳn (hard delete)
            string sqlDelete = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            return db.ExecuteNonQuery(sqlDelete, System.Data.CommandType.Text, checkParam) > 0;
        }

        public bool CheckTaiKhoanExists(string taiKhoan)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE TaiKhoan = @TaiKhoan";
            var result = db.ExecuteScalar(sql, System.Data.CommandType.Text, 
                new System.Data.SqlClient.SqlParameter("@TaiKhoan", taiKhoan));
            return Convert.ToInt32(result) > 0;
        }

        public NhanVienDTO CheckLogin(string taiKhoan, string matKhau)
        {
            string sql = $"SELECT * FROM NhanVien WHERE TaiKhoan = '{taiKhoan}' AND MatKhau = '{matKhau}' AND TrangThai = 1";
            DataTable dt = db.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new NhanVienDTO
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString(),
                    TaiKhoan = row["TaiKhoan"].ToString(),
                    IsAdmin = Convert.ToBoolean(row["IsAdmin"]),
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                };
            }
            return null;
        }
    }
}