using BusinessObject.Models;
using BusinessObject.Dtos.UserDtos;
using BusinessObject.Enums;

namespace Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users with pagination
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paginated user list</returns>
        Task<PaginatedResult<UserProfileDto>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Get users by role
        /// </summary>
        /// <param name="role">User role</param>
        /// <returns>List of users with specified role</returns>
        Task<List<UserProfileDto>> GetUsersByRoleAsync(UserRole role);

        /// <summary>
        /// Get users by status
        /// </summary>
        /// <param name="status">User status</param>
        /// <returns>List of users with specified status</returns>
        Task<List<UserProfileDto>> GetUsersByStatusAsync(UserStatus status);

        /// <summary>
        /// Search users by name or email
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <returns>List of matching users</returns>
        Task<List<UserProfileDto>> SearchUsersAsync(string searchTerm);

        /// <summary>
        /// Update user role (Admin only)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newRole">New role</param>
        /// <returns>Success status</returns>
        Task<bool> UpdateUserRoleAsync(string userId, UserRole newRole);

        /// <summary>
        /// Update user status (Admin only)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newStatus">New status</param>
        /// <returns>Success status</returns>
        Task<bool> UpdateUserStatusAsync(string userId, UserStatus newStatus);

        /// <summary>
        /// Get user statistics
        /// </summary>
        /// <returns>User statistics</returns>
        Task<UserStatisticsDto> GetUserStatisticsAsync();

        /// <summary>
        /// Delete user (Admin only)
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Success status</returns>
        Task<bool> DeleteUserAsync(string userId);

        /// <summary>
        /// Check if user exists
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>True if exists, false otherwise</returns>
        Task<bool> UserExistsAsync(string email);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User profile or null</returns>
        Task<UserProfileDto?> GetUserByEmailAsync(string email);
    }
}