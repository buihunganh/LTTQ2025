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
        public DataTable Search(string keyword, string rank)
        {
            return dal.SearchKhachHang(keyword, rank);
        }

        public bool Add(string ten, string sdt, decimal soTienMoi)
        {
            KhachHangDTO khachCu = dal.GetKhachHangByInfo(ten, sdt);

            if (khachCu != null)
            {

                decimal tongTienMoi = khachCu.TongChiTieu + soTienMoi;
                string hangMoi = TinhHangThanhVien(tongTienMoi);
                khachCu.TongChiTieu = tongTienMoi;
                khachCu.HangThanhVien = hangMoi;
                return dal.UpdateKhachHang(khachCu);
            }
            else
            {

                string hangThanhVien = TinhHangThanhVien(soTienMoi);

                KhachHangDTO khMoi = new KhachHangDTO
                {
                    HoTen = ten,
                    SoDienThoai = sdt,
                    TongChiTieu = soTienMoi,
                    HangThanhVien = hangThanhVien
                };

                return dal.AddKhachHang(khMoi);
            }
        }

        public bool Edit(int ma, string ten, string sdt)
        {
            return dal.UpdateKhachHang(new KhachHangDTO { MaKH = ma, HoTen = ten, SoDienThoai = sdt });
        }

        public bool Delete(int ma) => dal.DeleteKhachHang(ma);

        public DataTable GetPurchaseHistory(int maKH, int limit = 20)
        {
            return dal.GetPurchaseHistory(maKH, limit);
        }

        public string TinhHangThanhVien(decimal tongTien)
        {
            if (tongTien >= 20000000) return "Kim cương";
            if (tongTien >= 10000000) return "Vàng";  
            if (tongTien >= 5000000) return "Bạc";  
            return "Thành viên";
        }
    }
}