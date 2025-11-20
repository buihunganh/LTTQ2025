using System;

namespace BTL_LTTQ.DTO
{
    public class EmployeeProfile
    {
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? HireDate { get; set; }
        public string AvatarPath { get; set; }
        public bool IsAdmin { get; set; }
    }
}
