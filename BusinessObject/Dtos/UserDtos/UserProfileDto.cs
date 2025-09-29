using BusinessObject.Enums;

namespace BusinessObject.Dtos.UserDtos
{
    public class UserProfileDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public int RoleValue => (int)Role;
        public int StatusValue => (int)Status;
        public string RoleDisplayName => Role.ToString();
        public string StatusDisplayName => Status.ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive => Status == UserStatus.Active;
    }
}