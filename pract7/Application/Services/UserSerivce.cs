using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using pract7.Application.DBContext;
using pract7.Application.Models;

namespace pract7.Application.Services
{
    public class UserSerivce(
        IPasswordHasher<User> hasher,
        KuznetsovTaskDbContext context,
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
        public LoginResultWithToken LoginUser(LoginModel model)
        {
            var user =  context.Users.SingleOrDefault(x => x.Login == model.Login);

            if (user == null)
                return new LoginResultWithToken
                {
                    result = LoginResult.NotFound,
                    token = null
                };
            
            var result = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return new LoginResultWithToken
                {
                    result = LoginResult.IncorrectPassword,
                    token = null
                };


            var token = GenerateJwtToken(user);
            return new LoginResultWithToken
            {
                result = LoginResult.Success,
                token = token
            };
        }
        
        

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
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
    }

    public class RegisterModel()
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginModel()
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public enum RegisterResult
    {
        Success,
        Conflict
    }


    public class LoginResultWithToken
    {
        public LoginResult result { get; set; }
        public string? token { get; set; }
    }
    
    public enum LoginResult
    {
        Success,
        NotFound,
        IncorrectPassword
    }
}
