using System;
using System.Data;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ.DAL
{
    public class ChamCongDAL
    {
        private DataProcesser db = new DataProcesser();

        public bool CheckIn(int maNV)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @MaNV AND CAST(NgayLam AS DATE) = CAST(GETDATE() AS DATE))
                           BEGIN
                               UPDATE ChamCong SET GioCheckIn = GETDATE() 
                               WHERE MaNV = @MaNV AND CAST(NgayLam AS DATE) = CAST(GETDATE() AS DATE) AND GioCheckIn IS NULL
                           END
                           ELSE
                           BEGIN
                               INSERT INTO ChamCong(MaNV, NgayLam, GioCheckIn, NgayTao)
                               VALUES (@MaNV, CAST(GETDATE() AS DATE), GETDATE(), GETDATE())
                           END";
            
            return db.ExecuteNonQuery(sql, System.Data.CommandType.Text,
                new System.Data.SqlClient.SqlParameter("@MaNV", maNV)) > 0;
        }

        public bool CheckOut(int maNV)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @MaNV AND CAST(NgayLam AS DATE) = CAST(GETDATE() AS DATE))
                           BEGIN
                               UPDATE ChamCong SET GioCheckOut = GETDATE() 
                               WHERE MaNV = @MaNV AND CAST(NgayLam AS DATE) = CAST(GETDATE() AS DATE) AND GioCheckOut IS NULL
                           END
                           ELSE
                           BEGIN
                               INSERT INTO ChamCong(MaNV, NgayLam, GioCheckOut, NgayTao)
                               VALUES (@MaNV, CAST(GETDATE() AS DATE), GETDATE(), GETDATE())
                           END";
            
            return db.ExecuteNonQuery(sql, System.Data.CommandType.Text,
                new System.Data.SqlClient.SqlParameter("@MaNV", maNV)) > 0;
        }

        public DataRow GetChamCongToday(int maNV)
        {
            string sql = @"SELECT GioCheckIn, GioCheckOut
                         FROM ChamCong 
                         WHERE MaNV = @MaNV 
                         AND CAST(NgayLam AS DATE) = CAST(GETDATE() AS DATE)";
            
            DataTable dt = db.ExecuteQuery(sql, System.Data.CommandType.Text,
                new System.Data.SqlClient.SqlParameter("@MaNV", maNV));
            
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
}

