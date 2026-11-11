using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using pract7.Application.DBContext;
using pract7.Application.Models;

namespace pract7.Application.Services
{
    public class UserService(
        IPasswordHasher<User> hasher,
        Trpo7Context context,
        IConfiguration configuration)
    {
        public RegisterResult RegisterUser(RegisterModel model)
        {
            if (context.Users.Any(x => x.Login == model.Login))
                return RegisterResult.Conflict;

            var user = new User { Login = model.Login, Role = Role.User.ToString() };
            user.PasswordHash = hasher.HashPassword(user, model.Password);
            context.Users.Add(user);
            context.SaveChanges();
            return RegisterResult.Success;
        }

        public void CreateAdmin()
        {
            if (context.Users.Any(x => x.Login == "admin123"))
                return;

            var user = new User { Login = "admin123", Role = Role.Admin.ToString() };
            user.PasswordHash = hasher.HashPassword(user, "P@s$w0rd");
            context.Users.Add(user);
            context.SaveChanges();
        }

        public LoginResultWithTokens LoginUser(LoginModel model)
        {
            var user = context.Users.SingleOrDefault(x => x.Login == model.Login);

            if (user == null)
                return new LoginResultWithTokens
                {
                    Result = LoginResult.NotFound,
                    AccessToken = null,
                    RefreshToken = null,
                };

            var result = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return new LoginResultWithTokens
                {
                    Result = LoginResult.IncorrectPassword,
                    AccessToken = null,
                    RefreshToken = null,
                };


            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user);

            return new LoginResultWithTokens
            {
                Result = LoginResult.Success,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public RefreshResult Refresh(RefreshModel model)
        {
            var validateRefreshToken = RefreshTokenExistAndIsActive(new CheckRefreshModel
            {
                RefreshToken = model.RefreshToken
            });

            if (!validateRefreshToken.Success)
                return new RefreshResult
                {
                    AccessToken = null,
                    RefreshToken = null,
                    ErrorMessage = validateRefreshToken.ErrorMessage,
                };
            
            if(!validateRefreshToken.UserId.HasValue)
                return new RefreshResult
                {
                    AccessToken = null,
                    RefreshToken = null,
                    ErrorMessage = "Не пришел ID владельца токена",
                };

            var user = GetUserById(validateRefreshToken.UserId.Value);
            
            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user);

            return new RefreshResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ErrorMessage = null
            };
        }
        
        private User GetUserById(int id)
        {
            return context.Users.Single(x => x.Id == id);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "ExampleApi"
                ,
                audience: "ExampleApiClient"
                ,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string? GenerateRefreshToken(User user)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(32);
            var refreshToken = Convert.ToBase64String(randomBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
            var tokenBytes = Encoding.UTF8.GetBytes(refreshToken);
            var hashBytes = SHA256.HashData(tokenBytes);

            try
            {
                context.RefreshTokens.Add(new RefreshToken
                {
                    UserId = user.Id,
                    TokenHash = hashBytes.ToString(),
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return refreshToken;
        }

        private FunctionResult RefreshTokenExistAndIsActive(CheckRefreshModel checkRefreshModel)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(checkRefreshModel.RefreshToken);
            var hashToken = SHA256.HashData(tokenBytes);

            var refreshTokensInMemory = context.RefreshTokens
                .Where(x => x.UserId == checkRefreshModel.User.Id)
                .ToArray();

            if (refreshTokensInMemory.Length == 0)
                return new FunctionResult
                {
                    Success = false,
                    ErrorMessage = "Токена нет вообще",
                };

            if (refreshTokensInMemory.Length > 1)
                return new FunctionResult
                {
                    Success = false,
                    ErrorMessage = "Токенов больше одного, а?",
                };


            var refreshTokenFromMemory = refreshTokensInMemory.Single();

            if (refreshTokenFromMemory.TokenHash != hashToken.ToString())
                return new FunctionResult
                {
                    Success = false,
                    ErrorMessage = "Refresh Token is not active",
                };

            if (refreshTokenFromMemory.ExpiresAt < DateTime.UtcNow)
                return new FunctionResult
                {
                    Success = false,
                    ErrorMessage = "Токен истек",
                };

            return new FunctionResult
            {
                Success = true,
            };
        }
    }

    public class FunctionResult
    {
        public bool Success { get; set; }
        public int? UserId { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class RegisterModel
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

    public class LoginModel
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

    public class RefreshModel
    {
        public required string RefreshToken { get; set; }
    }

    public class CheckRefreshModel
    {
        public User User { get; set; }
        public required string RefreshToken { get; set; }
    }

    public enum RegisterResult
    {
        Success,
        Conflict
    }


    public class LoginResultWithTokens
    {
        public LoginResult Result { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    public class RefreshResult
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public enum LoginResult
    {
        Success,
        NotFound,
        IncorrectPassword
    }
}