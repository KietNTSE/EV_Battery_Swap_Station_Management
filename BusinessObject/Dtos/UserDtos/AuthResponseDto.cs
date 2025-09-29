using BusinessObject.Enums;

namespace BusinessObject.Dtos.UserDtos
{
    public class AuthResponseDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Enum values and display names
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public int RoleValue => (int)Role;
        public int StatusValue => (int)Status;
        public string RoleDisplayName => Role.ToString();
        public string StatusDisplayName => Status.ToString();

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiresAt { get; set; }
        public DateTime LoginAt { get; set; }

        // Token Information for Display
        public TokenInfo TokenDetails { get; set; }
    }

    public class TokenInfo
    {
        public string EncodedToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresInMinutes { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}