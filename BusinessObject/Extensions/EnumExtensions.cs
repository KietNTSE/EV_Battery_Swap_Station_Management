using BusinessObject.Enums;

namespace BusinessObject.Extensions
{
    public static class EnumExtensions
    {
        public static string GetRoleDescription(this UserRole role)
        {
            return role switch
            {
                UserRole.Customer => "Customer - Can book battery swaps and manage subscriptions",
                UserRole.Staff => "Staff - Can manage station operations and assist customers",
                UserRole.Admin => "Admin - Full system access and user management",
                _ => "Unknown Role"
            };
        }

        public static string GetStatusDescription(this UserStatus status)
        {
            return status switch
            {
                UserStatus.Active => "Active - User can access all features",
                UserStatus.Inactive => "Inactive - User account is temporarily disabled",
                UserStatus.Suspended => "Suspended - User account is suspended due to violations",
                _ => "Unknown Status"
            };
        }

        public static bool CanLogin(this UserStatus status)
        {
            return status == UserStatus.Active;
        }

        public static bool HasAdminAccess(this UserRole role)
        {
            return role == UserRole.Admin;
        }

        public static bool HasStaffAccess(this UserRole role)
        {
            return role == UserRole.Admin || role == UserRole.Staff;
        }
    }
}