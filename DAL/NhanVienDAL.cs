using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_LTTQ.DTO; 
using BTL_LTTQ.DAL; 
using System.Data;

namespace BTL_LTTQ.DAL
{
    public class NhanVienDAL
    {
        private DataProcesser db = new DataProcesser();

        public NhanVienDTO CheckLogin(string taiKhoan, string matKhau)
        {
            string sql = $"SELECT * FROM NhanVien WHERE TaiKhoan = '{taiKhoan}' AND MatKhau = '{matKhau}' AND TrangThai = 1";


            DataTable dt = db.ExecuteQuery(sql);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString(),
                    TaiKhoan = row["TaiKhoan"].ToString(),
                    IsAdmin = Convert.ToBoolean(row["IsAdmin"]),
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                };
                return nv;
            }
            return null;
        }

        public DataTable GetAllNhanVien()
        {
            string sql = "SELECT MaNV, HoTen, TaiKhoan, IsAdmin, TrangThai FROM NhanVien";
            return db.ExecuteQuery(sql);
        }

        public bool AddNhanVien(NhanVienDTO nv)
        {

            string sql = $"INSERT INTO NhanVien(HoTen, TaiKhoan, MatKhau, IsAdmin, TrangThai) VALUES (N'{nv.HoTen}', '{nv.TaiKhoan}', '{nv.MatKhau}', '{Convert.ToByte(nv.IsAdmin)}', '{Convert.ToByte(nv.TrangThai)}')";

            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool UpdateNhanVien(NhanVienDTO nv)
        {
            string sql = $"UPDATE NhanVien SET HoTen = N'{nv.HoTen}', IsAdmin = '{Convert.ToByte(nv.IsAdmin)}', TrangThai = '{Convert.ToByte(nv.TrangThai)}' WHERE MaNV = {nv.MaNV}";

            return db.ExecuteNonQuery(sql) > 0;
        }

        public bool DisableNhanVien(int maNV)
        {
            string sql = $"UPDATE NhanVien SET TrangThai = 0 WHERE MaNV = {maNV}";

            return db.ExecuteNonQuery(sql) > 0;
        }
    }
}