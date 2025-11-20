using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ.BLL
{
    public static class PhienDangNhap
    {
        public static NhanVienDTO NhanVienHienTai { get; private set; }

        public static void DangNhap(NhanVienDTO nhanVien)
        {
            NhanVienHienTai = nhanVien;
        }

        public static void DangXuat()
        {
            NhanVienHienTai = null;
        }
    }
}
