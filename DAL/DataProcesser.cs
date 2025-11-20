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
                throw new InvalidOperationException(
                    "Không tìm thấy cấu hình chuỗi kết nối 'StoreDb' trong App.config.");
            }

            _connectionString = connectionSetting.ConnectionString;
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

        /// <summary>
        /// Đổi mật khẩu cho nhân viên
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="oldPassword">Mật khẩu cũ</param>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// <returns>True nếu thành công, False nếu mật khẩu cũ không đúng</returns>
        public bool ChangePassword(int employeeId, string oldPassword, string newPassword)
        {
            const string sql = @"
                UPDATE NhanVien
                SET MatKhau = @newPassword
                WHERE MaNV = @employeeId
                      AND MatKhau = @oldPassword
                      AND ISNULL(TrangThai, 1) = 1";

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
            const string sql = @"
                SELECT MaNV, TaiKhoan, HoTen, SoDienThoai, Email, DiaChi, NgayVaoLam, AnhDaiDien, ISNULL(IsAdmin, 0) AS IsAdmin
                FROM NhanVien
                WHERE MaNV = @employeeId
                      AND ISNULL(TrangThai, 1) = 1";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@employeeId", SqlDbType.Int).Value = employeeId;

                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

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
            const string sql = @"
                UPDATE NhanVien
                SET HoTen = @fullName,
                    SoDienThoai = @phone,
                    Email = @email,
                    DiaChi = @address,
                    AnhDaiDien = @avatarPath
                WHERE MaNV = @employeeId
                      AND ISNULL(TrangThai, 1) = 1";

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
            const string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[NhanVien]') AND name = 'AnhDaiDien')
                BEGIN
                    ALTER TABLE [dbo].[NhanVien] ADD [AnhDaiDien] NVARCHAR(255) NULL
                END";

            try
            {
                ExecuteNonQuery(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
        }
    }
}

