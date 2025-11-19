using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BTL_LTTQ.DTO; // Đảm bảo namespace này chứa class LoginResult

namespace BTL_LTTQ.DAL
{
    public class DataProcesser : IDisposable
    {
        private readonly string _connectionString;

        public DataProcesser()
        {
            // Đọc chuỗi kết nối từ App.config
            var connectionSetting = ConfigurationManager.ConnectionStrings["StoreDb"];
            if (connectionSetting == null || string.IsNullOrWhiteSpace(connectionSetting.ConnectionString))
            {
                throw new InvalidOperationException(
                    "Không tìm thấy cấu hình chuỗi kết nối 'StoreDb' trong App.config.");
            }

            _connectionString = connectionSetting.ConnectionString;
        }

        // --- CÁC HÀM CƠ BẢN (SELECT, INSERT, UPDATE, DELETE) ---

        public DataTable ExecuteQuery(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (var connection = CreateConnection())
            using (var command = BuildCommand(connection, query, commandType, parameters))
            using (var adapter = new SqlDataAdapter(command))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (var connection = CreateConnection())
            using (var command = BuildCommand(connection, query, commandType, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query, CommandType commandType = CommandType.Text, params SqlParameter[] parameters)
        {
            using (var connection = CreateConnection())
            using (var command = BuildCommand(connection, query, commandType, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        

        public LoginResult AuthenticateUser(string username, string password)
        {
            const string sql = @"
                SELECT TOP 1 MaNV, HoTen, TaiKhoan, ISNULL(IsAdmin, 0) AS IsAdmin
                FROM NhanVien
                WHERE TaiKhoan = @username
                      AND MatKhau = @password
                      AND ISNULL(TrangThai, 1) = 1";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@username", SqlDbType.VarChar, 50).Value = username;
                command.Parameters.Add("@password", SqlDbType.VarChar, 255).Value = password;

                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new LoginResult
                    {
                        EmployeeId = reader.GetInt32(reader.GetOrdinal("MaNV")),
                        FullName = reader.GetString(reader.GetOrdinal("HoTen")),
                        Username = reader.GetString(reader.GetOrdinal("TaiKhoan")),
                        IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                    };
                }
            }
        }

        

        public bool NhapHangTransaction(int maNCC, int maNV, decimal tongTien, DataTable dtChiTiet)
        {
            using (var connection = CreateConnection()) 
            {
                using (var transaction = connection.BeginTransaction()) 
                {
                    try
                    {
                        
                        string sqlPhieu = @"INSERT INTO PhieuNhap(MaPhieuNhap, NgayNhap, MaNV, MaNCC, TongTien, TrangThai) 
                                            VALUES (@MaCode, GETDATE(), @MaNV, @MaNCC, @TongTien, N'Đã nhập');
                                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        SqlCommand cmdPhieu = new SqlCommand(sqlPhieu, connection, transaction);

                        string maPhieuCode = "PN" + DateTime.Now.ToString("yyyyMMddHHmmss");

                        cmdPhieu.Parameters.AddWithValue("@MaCode", maPhieuCode);
                        cmdPhieu.Parameters.AddWithValue("@MaNV", maNV);
                        cmdPhieu.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmdPhieu.Parameters.AddWithValue("@TongTien", tongTien);

                        int maPN = (int)cmdPhieu.ExecuteScalar();

                        
                        foreach (DataRow r in dtChiTiet.Rows)
                        {
                            
                            string sqlCT = @"INSERT INTO ChiTietPhieuNhap(MaPN, MaCTSP, SoLuong, GiaNhap, ThanhTien) 
                                             VALUES (@MaPN, @MaCTSP, @SoLuong, @GiaNhap, @ThanhTien)";

                            SqlCommand cmdCT = new SqlCommand(sqlCT, connection, transaction);
                            cmdCT.Parameters.AddWithValue("@MaPN", maPN);
                            cmdCT.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]); 
                            cmdCT.Parameters.AddWithValue("@SoLuong", r["SoLuong"]);
                            cmdCT.Parameters.AddWithValue("@GiaNhap", r["GiaNhap"]);
                            cmdCT.Parameters.AddWithValue("@ThanhTien", r["ThanhTien"]);
                            cmdCT.ExecuteNonQuery();

                            string sqlUpd = "UPDATE ChiTietSanPham SET SoLuongTon = ISNULL(SoLuongTon, 0) + @SL WHERE MaCTSP = @MaCTSP";
                            SqlCommand cmdUpd = new SqlCommand(sqlUpd, connection, transaction);
                            cmdUpd.Parameters.AddWithValue("@SL", r["SoLuong"]);
                            cmdUpd.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                            cmdUpd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Lỗi Transaction Nhập Hàng: " + ex.Message);
                    }
                }
            }
        }


        private SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        private static SqlCommand BuildCommand(SqlConnection connection, string query, CommandType commandType, SqlParameter[] parameters)
        {
            var command = new SqlCommand(query, connection)
            {
                CommandType = commandType
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        public void Dispose()
        {
        }
    }
}