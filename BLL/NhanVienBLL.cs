using System;
using System.Data;
using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ.BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL dalNhanVien = new NhanVienDAL();

        public DataTable GetNhanVienList()
        {
            return dalNhanVien.GetAllNhanVien();
        }

        public DataTable FindNhanVien(string keyword, int status)
        {
            return dalNhanVien.SearchNhanVien(keyword, status);
        }

        public bool CreateNhanVien(string hoTen, string taiKhoan, string matKhau, bool isAdmin, string sdt, string email, string diaChi, DateTime ngayVaoLam)
        {
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan)) return false;

            NhanVienDTO nv = new NhanVienDTO
            {
                HoTen = hoTen,
                TaiKhoan = taiKhoan,
                MatKhau = matKhau,
                IsAdmin = isAdmin,
                TrangThai = true,
                SoDienThoai = sdt,
                Email = email,
                DiaChi = diaChi,
                NgayVaoLam = ngayVaoLam
            };
            return dalNhanVien.AddNhanVien(nv);
        }

        // 4. Sửa nhân viên
        public bool EditNhanVien(int maNV, string hoTen, bool isAdmin, bool trangThai, string sdt, string email, string diaChi, DateTime ngayVaoLam)
        {
            NhanVienDTO nv = new NhanVienDTO
            {
                MaNV = maNV,
                HoTen = hoTen,
                IsAdmin = isAdmin,
                TrangThai = trangThai,
                SoDienThoai = sdt,
                Email = email,
                DiaChi = diaChi,
                NgayVaoLam = ngayVaoLam
            };
            return dalNhanVien.UpdateNhanVien(nv);
        }

        // 5. Xóa nhân viên
        public bool DeleteNhanVien(int maNV)
        {
            return dalNhanVien.DisableNhanVien(maNV);
        }

        // 6. Login (Giữ nguyên)
        public bool KiemTraDangNhap(string u, string p)
        {
            NhanVienDTO nv = dalNhanVien.CheckLogin(u, p);
            if (nv != null)
            {
                PhienDangNhap.DangNhap(nv);
                return true;
            }
            return false;
        }
    }
}