using System;

namespace BTL_LTTQ.DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public bool IsAdmin { get; set; }
        public bool TrangThai { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgayVaoLam { get; set; }
    }
}