using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

        public void Dispose()
        {
        }
    }

    public class LoginResult
    {
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }
    }
}

