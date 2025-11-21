using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ.DAL
{
    public class DataProcesser : IDisposable
    {
        private readonly string _connectionString;

        public DataProcesser()
        {
            var connectionSetting = ConfigurationManager.ConnectionStrings["StoreDb"];
            if (connectionSetting == null || string.IsNullOrWhiteSpace(connectionSetting.ConnectionString))
            {
                throw new InvalidOperationException("Không tìm thấy cấu hình chuỗi kết nối 'StoreDb' trong App.config.");
            }
            _connectionString = connectionSetting.ConnectionString;
        }

        private SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        private static SqlCommand BuildCommand(SqlConnection connection, string query, CommandType commandType, SqlParameter[] parameters)
        {
            var command = new SqlCommand(query, connection) { CommandType = commandType };
            if (parameters != null && parameters.Length > 0) command.Parameters.AddRange(parameters);
            return command;
        }

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

        /// <summary>
        /// Đổi mật khẩu cho nhân viên
        /// </summary>
        public bool ChangePassword(int employeeId, string oldPassword, string newPassword)
        {
            const string sql = @"UPDATE NhanVien SET MatKhau = @newPassword
                                 WHERE MaNV = @employeeId AND MatKhau = @oldPassword AND ISNULL(TrangThai, 1) = 1";

            var parameters = new[]
            {
                new SqlParameter("@employeeId", SqlDbType.Int) { Value = employeeId },
                new SqlParameter("@oldPassword", SqlDbType.VarChar, 255) { Value = oldPassword },
                new SqlParameter("@newPassword", SqlDbType.VarChar, 255) { Value = newPassword }
            };

            int rowsAffected = ExecuteNonQuery(sql, CommandType.Text, parameters);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Lấy thông tin profile đầy đủ của nhân viên
        /// </summary>
        public EmployeeProfile GetEmployeeProfile(int employeeId)
        {
            const string sql = @"SELECT MaNV, TaiKhoan, HoTen, SoDienThoai, Email, DiaChi, NgayVaoLam, AnhDaiDien, ISNULL(IsAdmin, 0) AS IsAdmin
                                 FROM NhanVien WHERE MaNV = @employeeId AND ISNULL(TrangThai, 1) = 1";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@employeeId", SqlDbType.Int).Value = employeeId;

                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read()) return null;

                    return new EmployeeProfile
                    {
                        EmployeeId = reader.GetInt32(reader.GetOrdinal("MaNV")),
                        Username = reader.GetString(reader.GetOrdinal("TaiKhoan")),
                        FullName = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? "" : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                        Address = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString(reader.GetOrdinal("DiaChi")),
                        HireDate = reader.IsDBNull(reader.GetOrdinal("NgayVaoLam")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("NgayVaoLam")),
                        AvatarPath = reader.IsDBNull(reader.GetOrdinal("AnhDaiDien")) ? null : reader.GetString(reader.GetOrdinal("AnhDaiDien")),
                        IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
                    };
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin profile của nhân viên
        /// </summary>
        public bool UpdateEmployeeProfile(int employeeId, string fullName, string phone, string email, string address, string avatarPath)
        {
            const string sql = @"UPDATE NhanVien SET HoTen = @fullName, SoDienThoai = @phone, Email = @email, 
                                 DiaChi = @address, AnhDaiDien = @avatarPath
                                 WHERE MaNV = @employeeId AND ISNULL(TrangThai, 1) = 1";

            var parameters = new[]
            {
                new SqlParameter("@employeeId", SqlDbType.Int) { Value = employeeId },
                new SqlParameter("@fullName", SqlDbType.NVarChar, 100) { Value = (object)fullName ?? DBNull.Value },
                new SqlParameter("@phone", SqlDbType.VarChar, 15) { Value = (object)phone ?? DBNull.Value },
                new SqlParameter("@email", SqlDbType.VarChar, 100) { Value = (object)email ?? DBNull.Value },
                new SqlParameter("@address", SqlDbType.NVarChar, 255) { Value = (object)address ?? DBNull.Value },
                new SqlParameter("@avatarPath", SqlDbType.NVarChar, 255) { Value = (object)avatarPath ?? DBNull.Value }
            };

            try
            {
                int rowsAffected = ExecuteNonQuery(sql, CommandType.Text, parameters);
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Invalid column name 'AnhDaiDien'"))
                {
                    if (EnsureAvatarColumnExists())
                    {
                        int rowsAffected = ExecuteNonQuery(sql, CommandType.Text, parameters);
                        return rowsAffected > 0;
                    }
                }
                throw;
            }
        }

        private bool EnsureAvatarColumnExists()
        {
            const string sql = @"IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[NhanVien]') AND name = 'AnhDaiDien')
                                 BEGIN ALTER TABLE [dbo].[NhanVien] ADD [AnhDaiDien] NVARCHAR(255) NULL END";
            try
            {
                ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }
        }

        public int ThemKhachHangNhanh(string hoTen, string sdt)
        {
            string sql = @"INSERT INTO KhachHang(HoTen, SoDienThoai, DiemTichLuy, TrangThai) 
                           VALUES (@Ten, @SDT, 0, 1); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Ten", hoTen);
                command.Parameters.AddWithValue("@SDT", sdt);
                return (int)command.ExecuteScalar();
            }
        }

        public DataTable GetChiTietHoaDon(int maHD)
        {
            string sql = @"SELECT cthd.MaCTSP, sp.TenGiay + ' (' + sz.KichCo + ' - ' + ms.TenMau + ')' AS TenSP, 
                                  cthd.SoLuong, cthd.DonGia, ISNULL(cthd.GiamGia, 0) AS GiamGia, cthd.ThanhTien
                           FROM ChiTietHoaDon cthd
                           JOIN ChiTietSanPham ctsp ON cthd.MaCTSP = ctsp.MaCTSP
                           JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                           JOIN SizeGiay sz ON ctsp.MaSize = sz.MaSize
                           JOIN MauSac ms ON ctsp.MaMau = ms.MaMau
                           WHERE cthd.MaHD = @MaHD";
            return ExecuteQuery(sql, CommandType.Text, new SqlParameter("@MaHD", maHD));
        }

        public DataTable GetThongTinChungHoaDon(int maHD)
        {
            string sql = @"SELECT hd.MaHoaDon, hd.NgayLap, hd.TongTien, hd.GiamGia, hd.ThanhToan, 
                          ISNULL(nv.HoTen, N'Không xác định') AS NhanVien, 
                          ISNULL(kh.HoTen, N'Khách lẻ') AS KhachHang, 
                          ISNULL(kh.SoDienThoai, '') AS SoDienThoai, 
                          ISNULL(kh.DiaChi, '') AS DiaChi
                           FROM HoaDon hd
                           LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                           LEFT JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                           WHERE hd.MaHD = @MaHD";
            return ExecuteQuery(sql, CommandType.Text, new SqlParameter("@MaHD", maHD));
        }

        public bool NhapHangTransaction(int maNCC, int maNV, decimal tongTien, DataTable dtChiTiet)
        {
            using (var connection = CreateConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sqlPhieu = @"INSERT INTO PhieuNhap(MaPhieuNhap, NgayNhap, MaNV, MaNCC, TongTien, TrangThai) 
                                        VALUES (@MaCode, GETDATE(), @MaNV, @MaNCC, @TongTien, N'Đã nhập');
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    SqlCommand cmdPhieu = new SqlCommand(sqlPhieu, connection, transaction);
                    cmdPhieu.Parameters.AddWithValue("@MaCode", "PN" + DateTime.Now.ToString("yyyyMMddHHmmss"));
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

                        // Cập nhật số lượng tồn kho VÀ giá nhập mới nhất
                        string sqlUpd = @"UPDATE ChiTietSanPham 
                                          SET SoLuongTon = ISNULL(SoLuongTon, 0) + @SL, 
                                              GiaNhap = @GiaNhap 
                                          WHERE MaCTSP = @MaCTSP";
                        SqlCommand cmdUpd = new SqlCommand(sqlUpd, connection, transaction);
                        cmdUpd.Parameters.AddWithValue("@SL", r["SoLuong"]);
                        cmdUpd.Parameters.AddWithValue("@GiaNhap", r["GiaNhap"]);
                        cmdUpd.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                        cmdUpd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return true;
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        public int BanHangTransaction(int maKH, int maNV, decimal tongTien, decimal giamGiaTong, decimal thanhToan, DataTable dtChiTiet)
        {
            using (var connection = CreateConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string maHDCode = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sqlHD = @"INSERT INTO HoaDon(MaHoaDon, NgayLap, MaNV, MaKH, MaKM, TongTien, GiamGia, ThanhToan, PhuongThucThanhToan, TrangThai) 
                                     VALUES (@MaCode, GETDATE(), @MaNV, @MaKH, 1, @TongTien, 0, @ThanhToan, N'Tiền mặt', N'Hoàn thành');
                                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmdHD = new SqlCommand(sqlHD, connection, transaction);
                    cmdHD.Parameters.AddWithValue("@MaCode", maHDCode);
                    cmdHD.Parameters.AddWithValue("@MaNV", maNV);
                    cmdHD.Parameters.AddWithValue("@MaKH", maKH);
                    cmdHD.Parameters.AddWithValue("@TongTien", tongTien);
                    cmdHD.Parameters.AddWithValue("@ThanhToan", thanhToan);

                    int newInvoiceID = Convert.ToInt32(cmdHD.ExecuteScalar());

                    foreach (DataRow r in dtChiTiet.Rows)
                    {
                        string sqlCT = @"INSERT INTO ChiTietHoaDon(MaHD, MaCTSP, SoLuong, DonGia, GiamGia, ThanhTien) 
                                         VALUES (@MaHD, @MaCTSP, @SoLuong, @DonGia, @GiamGia, @ThanhTien)";

                        SqlCommand cmdCT = new SqlCommand(sqlCT, connection, transaction);
                        cmdCT.Parameters.AddWithValue("@MaHD", newInvoiceID);
                        cmdCT.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                        cmdCT.Parameters.AddWithValue("@SoLuong", r["SoLuong"]);
                        cmdCT.Parameters.AddWithValue("@DonGia", r["DonGia"]);
                        cmdCT.Parameters.AddWithValue("@GiamGia", r["GiamGia"]);
                        cmdCT.Parameters.AddWithValue("@ThanhTien", r["ThanhTien"]);
                        cmdCT.ExecuteNonQuery();

                        string sqlUpd = "UPDATE ChiTietSanPham SET SoLuongTon = SoLuongTon - @SL WHERE MaCTSP = @MaCTSP";
                        SqlCommand cmdUpd = new SqlCommand(sqlUpd, connection, transaction);
                        cmdUpd.Parameters.AddWithValue("@SL", r["SoLuong"]);
                        cmdUpd.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                        cmdUpd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return newInvoiceID;
                }
                catch { transaction.Rollback(); throw; }
            }
        }
        public DataTable TimKiemHoaDon(DateTime tuNgay, DateTime denNgay, string tenNV, string tenKH)
        {
            string sql = @"SELECT hd.MaHD, hd.MaHoaDon, hd.NgayLap, 
                                  ISNULL(nv.HoTen, N'Không rõ') AS TenNhanVien, 
                                  ISNULL(kh.HoTen, N'Khách lẻ') AS TenKhachHang, 
                                  hd.TongTien, hd.TrangThai
                           FROM HoaDon hd
                           LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                           LEFT JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                           WHERE (hd.NgayLap BETWEEN @From AND @To)
                             AND (@TenNV = '' OR nv.HoTen LIKE N'%' + @TenNV + '%')
                             AND (@TenKH = '' OR kh.HoTen LIKE N'%' + @TenKH + '%')
                           ORDER BY hd.NgayLap DESC";

            // Chuyển đổi ngày để lấy trọn vẹn khoảng thời gian (00:00:00 đến 23:59:59)
            string fromDate = tuNgay.ToString("yyyy-MM-dd 00:00:00");
            string toDate = denNgay.ToString("yyyy-MM-dd 23:59:59");

            return ExecuteQuery(sql, CommandType.Text,
                new SqlParameter("@From", fromDate),
                new SqlParameter("@To", toDate),
                new SqlParameter("@TenNV", tenNV),
                new SqlParameter("@TenKH", tenKH)
            );
        }
        public bool HuyHoaDonTransaction(int maHD)
        {
            using (var connection = CreateConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sqlGetDetail = @"SELECT MaCTSP, SoLuong FROM ChiTietHoaDon WHERE MaHD = @MaHD";
                    SqlCommand cmdGet = new SqlCommand(sqlGetDetail, connection, transaction);
                    cmdGet.Parameters.AddWithValue("@MaHD", maHD);
                    DataTable dtChiTiet = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmdGet))
                    {
                        adapter.Fill(dtChiTiet);
                    }

                    // Cộng lại số lượng vào kho
                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        string sqlUpdate = "UPDATE ChiTietSanPham SET SoLuongTon = SoLuongTon + @SL WHERE MaCTSP = @MaCTSP";
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, connection, transaction);
                        cmdUpdate.Parameters.AddWithValue("@SL", row["SoLuong"]);
                        cmdUpdate.Parameters.AddWithValue("@MaCTSP", row["MaCTSP"]);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // Xóa chi tiết hóa đơn
                    string sqlDeleteCT = "DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD";
                    SqlCommand cmdDeleteCT = new SqlCommand(sqlDeleteCT, connection, transaction);
                    cmdDeleteCT.Parameters.AddWithValue("@MaHD", maHD);
                    cmdDeleteCT.ExecuteNonQuery();

                    // Xóa hóa đơn
                    string sqlDeleteHD = "DELETE FROM HoaDon WHERE MaHD = @MaHD";
                    SqlCommand cmdDeleteHD = new SqlCommand(sqlDeleteHD, connection, transaction);
                    cmdDeleteHD.Parameters.AddWithValue("@MaHD", maHD);
                    cmdDeleteHD.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool CapNhatHoaDonTransaction(int maHD, int maKH, decimal tongTien, decimal giamGiaTong, decimal thanhToan, DataTable dtChiTiet)
        {
            using (var connection = CreateConnection())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // 1. Lấy chi tiết hóa đơn cũ để cộng lại số lượng vào kho
                    string sqlGetOld = @"SELECT MaCTSP, SoLuong FROM ChiTietHoaDon WHERE MaHD = @MaHD";
                    SqlCommand cmdGetOld = new SqlCommand(sqlGetOld, connection, transaction);
                    cmdGetOld.Parameters.AddWithValue("@MaHD", maHD);
                    DataTable dtOld = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmdGetOld))
                    {
                        adapter.Fill(dtOld);
                    }

                    // 2. Cộng lại số lượng vào kho cho các sản phẩm cũ
                    foreach (DataRow row in dtOld.Rows)
                    {
                        string sqlUpdate = "UPDATE ChiTietSanPham SET SoLuongTon = SoLuongTon + @SL WHERE MaCTSP = @MaCTSP";
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, connection, transaction);
                        cmdUpdate.Parameters.AddWithValue("@SL", row["SoLuong"]);
                        cmdUpdate.Parameters.AddWithValue("@MaCTSP", row["MaCTSP"]);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    // 3. Xóa chi tiết hóa đơn cũ
                    string sqlDeleteCT = "DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD";
                    SqlCommand cmdDeleteCT = new SqlCommand(sqlDeleteCT, connection, transaction);
                    cmdDeleteCT.Parameters.AddWithValue("@MaHD", maHD);
                    cmdDeleteCT.ExecuteNonQuery();

                    // 4. Cập nhật thông tin hóa đơn
                    string sqlUpdateHD = @"UPDATE HoaDon 
                                           SET MaKH = @MaKH, TongTien = @TongTien, GiamGia = @GiamGia, ThanhToan = @ThanhToan
                                           WHERE MaHD = @MaHD";
                    SqlCommand cmdUpdateHD = new SqlCommand(sqlUpdateHD, connection, transaction);
                    cmdUpdateHD.Parameters.AddWithValue("@MaHD", maHD);
                    cmdUpdateHD.Parameters.AddWithValue("@MaKH", maKH);
                    cmdUpdateHD.Parameters.AddWithValue("@TongTien", tongTien);
                    cmdUpdateHD.Parameters.AddWithValue("@GiamGia", giamGiaTong);
                    cmdUpdateHD.Parameters.AddWithValue("@ThanhToan", thanhToan);
                    cmdUpdateHD.ExecuteNonQuery();

                    // 5. Thêm chi tiết mới và trừ số lượng trong kho
                    foreach (DataRow r in dtChiTiet.Rows)
                    {
                        string sqlCT = @"INSERT INTO ChiTietHoaDon(MaHD, MaCTSP, SoLuong, DonGia, GiamGia, ThanhTien) 
                                         VALUES (@MaHD, @MaCTSP, @SoLuong, @DonGia, @GiamGia, @ThanhTien)";

                        SqlCommand cmdCT = new SqlCommand(sqlCT, connection, transaction);
                        cmdCT.Parameters.AddWithValue("@MaHD", maHD);
                        cmdCT.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                        cmdCT.Parameters.AddWithValue("@SoLuong", r["SoLuong"]);
                        cmdCT.Parameters.AddWithValue("@DonGia", r["DonGia"]);
                        cmdCT.Parameters.AddWithValue("@GiamGia", r["GiamGia"]);
                        cmdCT.Parameters.AddWithValue("@ThanhTien", r["ThanhTien"]);
                        cmdCT.ExecuteNonQuery();

                        string sqlUpd = "UPDATE ChiTietSanPham SET SoLuongTon = SoLuongTon - @SL WHERE MaCTSP = @MaCTSP";
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
                    // Log lỗi để debug
                    System.Diagnostics.Debug.WriteLine($"Lỗi cập nhật hóa đơn: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        public void Dispose() { }
    }
}
