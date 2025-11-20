using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ.BLL
{
    public class KhachHangBLL
    {
        private KhachHangDAL dal = new KhachHangDAL();

        public DataTable GetList() => dal.GetAllKhachHang();
        public DataTable Search(string keyword) => dal.SearchKhachHang(keyword);

        public bool Add(string ten, string sdt)
        {
            if (string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(sdt)) return false;
            return dal.AddKhachHang(new KhachHangDTO { HoTen = ten, SoDienThoai = sdt });
        }

        public bool Edit(int ma, string ten, string sdt)
        {
            return dal.UpdateKhachHang(new KhachHangDTO { MaKH = ma, HoTen = ten, SoDienThoai = sdt });
        }

        public bool Delete(int ma) => dal.DeleteKhachHang(ma);

        public string TinhHangThanhVien(decimal tongTien)
        {
            if (tongTien >= 10000000) return "Gold";  
            if (tongTien >= 5000000) return "Silver";  
            return "Member";
        }
    }
}