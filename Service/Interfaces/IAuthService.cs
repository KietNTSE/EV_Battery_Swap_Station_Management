using BusinessObject.Dtos.UserDtos;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="registerDto">User registration data</param>
        /// <returns>Authentication response with token</returns>
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Authenticate user login
        /// </summary>
        /// <param name="loginDto">User login credentials</param>
        /// <returns>Authentication response with token</returns>
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="userId">User ID to logout</param>
        /// <returns>Success status</returns>
        Task<bool> LogoutAsync(string userId);

        /// <summary>
        /// Refresh access token using refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>New authentication response</returns>
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="currentPassword">Current password</param>
        /// <param name="newPassword">New password</param>
        /// <returns>Success status</returns>
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="updateDto">Updated user data</param>
        /// <returns>Success status</returns>
        Task<bool> UpdateProfileAsync(string userId, UpdateProfileDto updateDto);

        /// <summary>
        /// Get user profile by ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User profile data</returns>
        Task<UserProfileDto> GetUserProfileAsync(string userId);

        /// <summary>
        /// Deactivate user account
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Success status</returns>
        Task<bool> DeactivateUserAsync(string userId);

        /// <summary>
        /// Activate user account
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Success status</returns>
        Task<bool> ActivateUserAsync(string userId);
    }
}
