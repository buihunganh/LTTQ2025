using System;
using BTL_LTTQ.DAL;

namespace BTL_LTTQ.BLL
{
    public class UserService : IDisposable
    {
        private readonly DataProcesser _dataProcesser;

        public UserService()
        {
            _dataProcesser = new DataProcesser();
        }

        /// <summary>
        /// Đổi mật khẩu cho user
        /// </summary>
        /// <param name="employeeId">ID nhân viên</param>
        /// <param name="oldPassword">Mật khẩu cũ</param>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// <param name="confirmPassword">Xác nhận mật khẩu mới</param>
        /// <param name="errorMessage">Thông báo lỗi nếu có</param>
        /// <returns>True nếu thành công, False nếu thất bại</returns>
        public bool UpdatePassword(int employeeId, string oldPassword, string newPassword, string confirmPassword, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                errorMessage = "Vui lòng nhập mật khẩu cũ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                errorMessage = "Vui lòng nhập mật khẩu mới.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                errorMessage = "Vui lòng xác nhận mật khẩu mới.";
                return false;
            }

            if (newPassword.Length < 6)
            {
                errorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return false;
            }

            if (newPassword != confirmPassword)
            {
                errorMessage = "Mật khẩu mới và xác nhận không khớp.";
                return false;
            }

            if (oldPassword == newPassword)
            {
                errorMessage = "Mật khẩu mới phải khác mật khẩu cũ.";
                return false;
            }

            try
            {
                bool success = _dataProcesser.ChangePassword(employeeId, oldPassword, newPassword);
                if (!success)
                {
                    errorMessage = "Mật khẩu cũ không đúng.";
                }
                return success;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin profile của user
        /// </summary>
        public bool UpdateProfile(int employeeId, string fullName, string phone, string email, string address, string avatarPath, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                errorMessage = "Vui lòng nhập họ tên.";
                return false;
            }

            try
            {
                bool success = _dataProcesser.UpdateEmployeeProfile(employeeId, fullName, phone, email, address, avatarPath);
                if (!success)
                {
                    errorMessage = "Không thể cập nhật thông tin. Vui lòng thử lại.";
                }
                return success;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi: {ex.Message}";
                return false;
            }
        }

        public void Dispose()
        {
            _dataProcesser?.Dispose();
        }
    }
}
