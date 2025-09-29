using BusinessObject.Dtos.UserDtos;
using BusinessObject.Enums;
using BusinessObject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Implementations;
using Service.Interfaces;
using System.Security.Claims;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Get available roles and statuses
        /// </summary>
        [HttpGet("enums")]
        public IActionResult GetEnums()
        {
            var roles = Enum.GetValues<UserRole>()
                .Select(r => new {
                    value = (int)r,
                    name = r.ToString(),
                    description = r.GetRoleDescription()
                });

            var statuses = Enum.GetValues<UserStatus>()
                .Select(s => new {
                    value = (int)s,
                    name = s.ToString(),
                    description = s.GetStatusDescription()
                });

            return Ok(new
            {
                success = true,
                data = new { roles, statuses }
            });
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });

                var result = await _authService.RegisterAsync(model);

                return Ok(new
                {
                    success = true,
                    message = "User registered successfully",
                    data = new
                    {
                        user = new
                        {
                            userId = result.UserId,
                            fullName = result.FullName,
                            email = result.Email,
                            phone = result.Phone,
                            role = new
                            {
                                value = result.RoleValue,
                                name = result.RoleDisplayName,
                                description = result.Role.GetRoleDescription()
                            },
                            status = new
                            {
                                value = result.StatusValue,
                                name = result.StatusDisplayName,
                                description = result.Status.GetStatusDescription()
                            }
                        },
                        authentication = new
                        {
                            accessToken = result.AccessToken,
                            refreshToken = result.RefreshToken,
                            expiresAt = result.TokenExpiresAt,
                            loginAt = result.LoginAt
                        },
                        tokenDetails = new
                        {
                            encodedToken = result.TokenDetails.EncodedToken,
                            tokenType = result.TokenDetails.TokenType,
                            expiresInMinutes = result.TokenDetails.ExpiresInMinutes,
                            issuedAt = result.TokenDetails.IssuedAt,
                            expiresAt = result.TokenDetails.ExpiresAt,
                            issuer = result.TokenDetails.Issuer,
                            audience = result.TokenDetails.Audience,
                            howToUse = $"Add to request headers: Authorization: {result.TokenDetails.TokenType} {result.TokenDetails.EncodedToken}"
                        }
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred during registration" });
            }
        }

        /// <summary>
        /// Login user and get JWT token
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });

                var result = await _authService.LoginAsync(model);

                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    data = new
                    {
                        user = new
                        {
                            userId = result.UserId,
                            fullName = result.FullName,
                            email = result.Email,
                            phone = result.Phone,
                            role = new
                            {
                                value = result.RoleValue,
                                name = result.RoleDisplayName,
                                description = result.Role.GetRoleDescription(),
                                permissions = new
                                {
                                    hasAdminAccess = result.Role.HasAdminAccess(),
                                    hasStaffAccess = result.Role.HasStaffAccess()
                                }
                            },
                            status = new
                            {
                                value = result.StatusValue,
                                name = result.StatusDisplayName,
                                description = result.Status.GetStatusDescription(),
                                canLogin = result.Status.CanLogin()
                            }
                        },
                        authentication = new
                        {
                            accessToken = result.AccessToken,
                            refreshToken = result.RefreshToken,
                            expiresAt = result.TokenExpiresAt,
                            loginAt = result.LoginAt
                        },
                        tokenDetails = new
                        {
                            encodedToken = result.TokenDetails.EncodedToken,
                            tokenType = result.TokenDetails.TokenType,
                            expiresInMinutes = result.TokenDetails.ExpiresInMinutes,
                            issuedAt = result.TokenDetails.IssuedAt,
                            expiresAt = result.TokenDetails.ExpiresAt,
                            issuer = result.TokenDetails.Issuer,
                            audience = result.TokenDetails.Audience,
                            howToUse = $"Add to request headers: Authorization: {result.TokenDetails.TokenType} {result.TokenDetails.EncodedToken}"
                        }
                    }
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred during login" });
            }
        }

        /// <summary>
        /// Logout current user
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { success = false, message = "User not authenticated" });

                await _authService.LogoutAsync(userId);
                return Ok(new
                {
                    success = true,
                    message = "Logged out successfully",
                    data = new
                    {
                        loggedOutAt = DateTime.UtcNow,
                        userId = userId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred during logout" });
            }
        }

        /// <summary>
        /// Get current user profile
        /// </summary>
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var fullName = User.FindFirst(ClaimTypes.Name)?.Value;
                var roleString = User.FindFirst(ClaimTypes.Role)?.Value;
                var roleValue = User.FindFirst("RoleValue")?.Value;
                var statusString = User.FindFirst("Status")?.Value;
                var statusValue = User.FindFirst("StatusValue")?.Value;
                var phone = User.FindFirst("Phone")?.Value;

                // Parse enums
                Enum.TryParse<UserRole>(roleString, out var role);
                Enum.TryParse<UserStatus>(statusString, out var status);

                return Ok(new
                {
                    success = true,
                    message = "Profile retrieved successfully",
                    data = new
                    {
                        userId,
                        fullName,
                        email,
                        phone,
                        role = new
                        {
                            value = roleValue,
                            name = roleString,
                            description = role.GetRoleDescription(),
                            permissions = new
                            {
                                hasAdminAccess = role.HasAdminAccess(),
                                hasStaffAccess = role.HasStaffAccess()
                            }
                        },
                        status = new
                        {
                            value = statusValue,
                            name = statusString,
                            description = status.GetStatusDescription(),
                            canLogin = status.CanLogin()
                        },
                        retrievedAt = DateTime.UtcNow
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving profile" });
            }
        }

        /// <summary>
        /// Decode and display token information
        /// </summary>
        [HttpGet("token-info")]
        [Authorize]
        public IActionResult GetTokenInfo()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                return Ok(new
                {
                    success = true,
                    message = "Token information retrieved",
                    data = new
                    {
                        encodedToken = token,
                        tokenLength = token.Length,
                        claims = User.Claims.Select(c => new { c.Type, c.Value }),
                        tokenParts = new
                        {
                            header = token.Split('.')[0],
                            payload = token.Split('.')[1],
                            signature = token.Split('.')[2]
                        },
                        userInfo = new
                        {
                            userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                            email = User.FindFirst(ClaimTypes.Email)?.Value,
                            role = User.FindFirst(ClaimTypes.Role)?.Value,
                            roleValue = User.FindFirst("RoleValue")?.Value,
                            status = User.FindFirst("Status")?.Value,
                            statusValue = User.FindFirst("StatusValue")?.Value
                        },
                        tokenClaims = new
                        {
                            jti = User.FindFirst("jti")?.Value,
                            iat = User.FindFirst("iat")?.Value,
                            sub = User.FindFirst("sub")?.Value,
                            exp = User.FindFirst("exp")?.Value
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving token info" });
            }
        }
    }
}