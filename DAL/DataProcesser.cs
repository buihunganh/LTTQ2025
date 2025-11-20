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

        // --- CÁC HÀM HELPER CƠ BẢN ---
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


        // --- CÁC HÀM THỰC THI SQL ---
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

        // --- ĐĂNG NHẬP ---
        public LoginResult AuthenticateUser(string username, string password)
        {
            const string sql = @"SELECT TOP 1 MaNV, HoTen, TaiKhoan, ISNULL(IsAdmin, 0) AS IsAdmin 
                                 FROM NhanVien WHERE TaiKhoan = @username AND MatKhau = @password AND ISNULL(TrangThai, 1) = 1";
            using (var connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read()) return null;
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

        // --- THÊM KHÁCH HÀNG NHANH ---
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


        // --- LẤY CHI TIẾT HÓA ĐƠN ---
        public DataTable GetChiTietHoaDon(int maHD)
        {
            string sql = @"SELECT sp.TenGiay, sz.KichCo, ms.TenMau, 
                                  cthd.SoLuong, cthd.DonGia, cthd.ThanhTien
                           FROM ChiTietHoaDon cthd
                           JOIN ChiTietSanPham ctsp ON cthd.MaCTSP = ctsp.MaCTSP
                           JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                           JOIN SizeGiay sz ON ctsp.MaSize = sz.MaSize
                           JOIN MauSac ms ON ctsp.MaMau = ms.MaMau
                           WHERE cthd.MaHD = @MaHD";
            return ExecuteQuery(sql, CommandType.Text, new SqlParameter("@MaHD", maHD));
        }

        // --- LẤY THÔNG TIN CHUNG HÓA ĐƠN (LEFT JOIN FIX) ---
        // Dán đè hàm này vào DataProcesser.cs
        public DataTable GetThongTinChungHoaDon(int maHD)
        {
            // Dùng LEFT JOIN để dù thiếu Nhân viên hay Khách hàng thì vẫn hiện Hóa đơn
            string sql = @"SELECT hd.MaHoaDon, hd.NgayLap, hd.TongTien, hd.GiamGia, hd.ThanhToan, 
                          ISNULL(nv.HoTen, N'Không xác định') AS NhanVien, 
                          ISNULL(kh.HoTen, N'Khách lẻ') AS KhachHang, 
                          ISNULL(kh.SoDienThoai, '') AS SoDienThoai, 
                          ISNULL(kh.DiaChi, '') AS DiaChi
                   FROM HoaDon hd
                   LEFT JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                   LEFT JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                   WHERE hd.MaHD = @MaHD";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MaHD", maHD);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- TRANSACTION NHẬP HÀNG ---
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

                        string sqlUpd = "UPDATE ChiTietSanPham SET SoLuongTon = ISNULL(SoLuongTon, 0) + @SL WHERE MaCTSP = @MaCTSP";
                        SqlCommand cmdUpd = new SqlCommand(sqlUpd, connection, transaction);
                        cmdUpd.Parameters.AddWithValue("@SL", r["SoLuong"]);
                        cmdUpd.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                        cmdUpd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    return true;
                }
                catch { transaction.Rollback(); throw; }
            }
        }

        // --- TRANSACTION BÁN HÀNG (Trả về INT ID) ---
        // --- [ĐÃ SỬA] LƯU GIẢM GIÁ CHO TỪNG SẢN PHẨM ---
        public int BanHangTransaction(int maKH, int maNV, decimal tongTien, decimal giamGiaTong, decimal thanhToan, DataTable dtChiTiet)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. INSERT HÓA ĐƠN (Tổng tiền lúc này là tổng thực trả)
                        string maHDCode = "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        string sqlHD = @"INSERT INTO HoaDon(MaHoaDon, NgayLap, MaNV, MaKH, MaKM, TongTien, GiamGia, ThanhToan, PhuongThucThanhToan, TrangThai) 
                                         VALUES (@MaCode, GETDATE(), @MaNV, @MaKH, 1, @TongTien, 0, @ThanhToan, N'Tiền mặt', N'Hoàn thành');
                                         SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        // Lưu ý: GiamGia ở bảng HoaDon để 0 vì đã giảm chi tiết từng món rồi

                        SqlCommand cmdHD = new SqlCommand(sqlHD, connection, transaction);
                        cmdHD.Parameters.AddWithValue("@MaCode", maHDCode);
                        cmdHD.Parameters.AddWithValue("@MaNV", maNV);
                        cmdHD.Parameters.AddWithValue("@MaKH", maKH);
                        cmdHD.Parameters.AddWithValue("@TongTien", tongTien);
                        cmdHD.Parameters.AddWithValue("@ThanhToan", thanhToan);

                        int newInvoiceID = Convert.ToInt32(cmdHD.ExecuteScalar());

                        // 2. INSERT CHI TIẾT (CÓ THÊM CỘT GIAMGIA)
                        foreach (DataRow r in dtChiTiet.Rows)
                        {
                            string sqlCT = @"INSERT INTO ChiTietHoaDon(MaHD, MaCTSP, SoLuong, DonGia, GiamGia, ThanhTien) 
                                             VALUES (@MaHD, @MaCTSP, @SoLuong, @DonGia, @GiamGia, @ThanhTien)";

                            SqlCommand cmdCT = new SqlCommand(sqlCT, connection, transaction);
                            cmdCT.Parameters.AddWithValue("@MaHD", newInvoiceID);
                            cmdCT.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                            cmdCT.Parameters.AddWithValue("@SoLuong", r["SoLuong"]);
                            cmdCT.Parameters.AddWithValue("@DonGia", r["DonGia"]);
                            cmdCT.Parameters.AddWithValue("@GiamGia", r["GiamGia"]); // <--- MỚI
                            cmdCT.Parameters.AddWithValue("@ThanhTien", r["ThanhTien"]); // Tiền này đã trừ chiết khấu
                            cmdCT.ExecuteNonQuery();

                            // Trừ kho
                            string sqlUpd = "UPDATE ChiTietSanPham SET SoLuongTon = SoLuongTon - @SL WHERE MaCTSP = @MaCTSP";
                            SqlCommand cmdUpd = new SqlCommand(sqlUpd, connection, transaction);
                            cmdUpd.Parameters.AddWithValue("@SL", r["SoLuong"]);
                            cmdUpd.Parameters.AddWithValue("@MaCTSP", r["MaCTSP"]);
                            cmdUpd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return newInvoiceID;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        public void Dispose() { }
    }
}