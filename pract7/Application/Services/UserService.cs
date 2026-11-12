using Microsoft.AspNetCore.Identity;
using pract7.Application.DBContext;
using pract7.Application.Models;

namespace pract7.Application.Services
{
    public class UserService(
        IPasswordHasher<User> hasher,
        Trpo7Context context,
        RefreshTokenService refreshTokens)
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


            var accessToken = refreshTokens.GenerateJwtToken(user);
            var refreshToken = refreshTokens.GenerateRefreshToken(user);
            
            return new LoginResultWithTokens
            {
                Result = LoginResult.Success,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public RefreshResult Refresh(RefreshModel model)
        {
            return refreshTokens.Rotate(model.RefreshToken);
        }
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
