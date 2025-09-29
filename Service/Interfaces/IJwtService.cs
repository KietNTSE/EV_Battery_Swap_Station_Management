using BusinessObject.Models;
using BusinessObject.Dtos.UserDtos;
using System.Security.Claims;

namespace Service.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generate access token for user
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>JWT access token</returns>
        string GenerateAccessToken(User user);

        /// <summary>
        /// Generate access token with detailed token information
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>Tuple of token and token details</returns>
        (string token, TokenInfo tokenInfo) GenerateAccessTokenWithDetails(User user);

        /// <summary>
        /// Generate refresh token
        /// </summary>
        /// <returns>Refresh token string</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Validate JWT token
        /// </summary>
        /// <param name="token">JWT token to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidateToken(string token);

        /// <summary>
        /// Get claims principal from expired token
        /// </summary>
        /// <param name="token">Expired JWT token</param>
        /// <returns>Claims principal</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        /// <summary>
        /// Extract user ID from token
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <returns>User ID</returns>
        string GetUserIdFromToken(string token);

        /// <summary>
        /// Check if token is expired
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <returns>True if expired, false otherwise</returns>
        bool IsTokenExpired(string token);
    }
}