namespace BusinessObject.Dtos.UserDtos
{
    public class UserStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int SuspendedUsers { get; set; }
        public int CancelledUsers { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalStaff { get; set; }
        public int TotalAdmins { get; set; }
        public int NewUsersThisMonth { get; set; }
        public int NewUsersToday { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}