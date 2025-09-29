namespace Service.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Send welcome email to new user
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="fullName">User full name</param>
        /// <returns>Success status</returns>
        Task<bool> SendWelcomeEmailAsync(string email, string fullName);

        /// <summary>
        /// Send password reset email
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="resetToken">Password reset token</param>
        /// <returns>Success status</returns>
        Task<bool> SendPasswordResetEmailAsync(string email, string resetToken);

        /// <summary>
        /// Send email verification
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="verificationToken">Email verification token</param>
        /// <returns>Success status</returns>
        Task<bool> SendEmailVerificationAsync(string email, string verificationToken);

        /// <summary>
        /// Send account suspension notification
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="reason">Suspension reason</param>
        /// <returns>Success status</returns>
        Task<bool> SendAccountSuspensionEmailAsync(string email, string reason);
    }
}
