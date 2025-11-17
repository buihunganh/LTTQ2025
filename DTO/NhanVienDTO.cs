using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
