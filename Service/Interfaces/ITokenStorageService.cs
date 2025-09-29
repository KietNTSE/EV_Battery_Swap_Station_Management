namespace Service.Interfaces
{
    public interface ITokenStorageService
    {
        /// <summary>
        /// Store refresh token
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="refreshToken">Refresh token</param>
        /// <param name="expiryDate">Token expiry date</param>
        /// <returns>Success status</returns>
        Task<bool> StoreRefreshTokenAsync(string userId, string refreshToken, DateTime expiryDate);

        /// <summary>
        /// Validate refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>True if valid, false otherwise</returns>
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Revoke refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Success status</returns>
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Revoke all user tokens
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Success status</returns>
        Task<bool> RevokeAllUserTokensAsync(string userId);

        /// <summary>
        /// Get user ID from refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>User ID or null</returns>
        Task<string?> GetUserIdFromRefreshTokenAsync(string refreshToken);

        /// <summary>
        /// Clean expired tokens
        /// </summary>
        /// <returns>Number of cleaned tokens</returns>
        Task<int> CleanExpiredTokensAsync();
    }
}