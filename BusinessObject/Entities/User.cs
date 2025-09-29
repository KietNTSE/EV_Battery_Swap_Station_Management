using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using BusinessObject.Enums;

namespace BusinessObject.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;

        [Required]
        public UserStatus Status { get; set; } = UserStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        // Helper properties for display
        public string RoleDisplayName => Role.ToString();
        public string StatusDisplayName => Status.ToString();
        public int RoleValue => (int)Role;
        public int StatusValue => (int)Status;
    }
}