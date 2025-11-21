namespace BTL_LTTQ.DTO
{
    public class SanPhamDTO
    {
        public int MaSP { get; set; }
        public string TenGiay { get; set; }
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public int MaThuongHieu { get; set; }
        public string TenThuongHieu { get; set; }
        public string MoTa { get; set; }
        public string HinhAnhChung { get; set; }
        public bool TrangThai { get; set; }
    }

    public class ChiTietSanPhamDTO
    {
        public int MaCTSP { get; set; }
        public int MaSP { get; set; }
        public string TenGiay { get; set; }
        public int MaSize { get; set; }
        public string KichCo { get; set; }
        public int MaMau { get; set; }
        public string TenMau { get; set; }
        public string MaSKU { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public bool TrangThai { get; set; }
        public string HinhAnhChung { get; set; }
        public string TenLoai { get; set; }
    }
}

