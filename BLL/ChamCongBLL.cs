using System;
using System.Data;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ.BLL
{
    public class ChamCongBLL
    {
        private ChamCongDAL dalChamCong = new ChamCongDAL();

        public bool CheckIn(int maNV)
        {
            return dalChamCong.CheckIn(maNV);
        }

        public bool CheckOut(int maNV)
        {
            return dalChamCong.CheckOut(maNV);
        }

        public DataRow GetChamCongToday(int maNV)
        {
            return dalChamCong.GetChamCongToday(maNV);
        }
    }
}

