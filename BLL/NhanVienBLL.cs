using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LTTQ.BLL
{
    public class NhanVienBLL
    {
        private NhanVienDAL dalNhanVien = new NhanVienDAL();

        public bool KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                return false;
            }

            NhanVienDTO nv = dalNhanVien.CheckLogin(taiKhoan, matKhau);

            if (nv != null)
            {
                PhienDangNhap.DangNhap(nv);
                return true;
            }
            return false;
        }

        public DataTable GetNhanVienList()
        {
            return dalNhanVien.GetAllNhanVien();
        }

        public bool CreateNhanVien(string hoTen, string taiKhoan, string matKhau, bool isAdmin)
        {
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return false;
            }

            NhanVienDTO nv = new NhanVienDTO
            {
                HoTen = hoTen,
                TaiKhoan = taiKhoan,
                MatKhau = matKhau, 
                IsAdmin = isAdmin,
                TrangThai = true 
            };

            return dalNhanVien.AddNhanVien(nv);
        }

        public bool EditNhanVien(int maNV, string hoTen, bool isAdmin, bool trangThai)
        {
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên không được để trống.");
                return false;
            }

            NhanVienDTO nv = new NhanVienDTO
            {
                MaNV = maNV,
                HoTen = hoTen,
                IsAdmin = isAdmin,
                TrangThai = trangThai
            };

            return dalNhanVien.UpdateNhanVien(nv);
        }

        public bool DeleteNhanVien(int maNV)
        {
            return dalNhanVien.DisableNhanVien(maNV);
        }
    }
}