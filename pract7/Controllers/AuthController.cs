using Microsoft.AspNetCore.Mvc;
using pract7.Application.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pract7.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(UserService userService, RefreshTokenService refreshTokens) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = userService.RegisterUser(request.ToModel());
            if (result == RegisterResult.Conflict)
                return Conflict("Такой юзер уже есть");
            return Ok("Пользователь зарегистрирован");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = userService.LoginUser(request.ToModel());
            if (result.Result is LoginResult.NotFound
                or LoginResult.IncorrectPassword)
                return Unauthorized("Неверная пара логин/пароль");
            if (result.AccessToken is null
                || result.RefreshToken is null)
                return StatusCode(500, "Токены не сгенерировались, это просто ужас");

            return Ok(new LoginResponse
            {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            var tokens = userService.Refresh(request.ToModel());

            if (tokens.ErrorMessage is not null)
                return Unauthorized(tokens.ErrorMessage);

            if(tokens.AccessToken is null || tokens.RefreshToken is null)
                return Unauthorized($"Токены не вернулись??? {tokens.ErrorMessage}");
            
            var response = new RefreshResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout([FromBody] RefreshRequest request)
        {
            var login = User.Identity?.Name;
            var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
            var exp = User.FindFirstValue(JwtRegisteredClaimNames.Exp);
            if (login is null || jti is null || !long.TryParse(exp, out var seconds))
                return Unauthorized();
            var result = refreshTokens.Logout(request.RefreshToken, login, jti,
                DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime);
            if (result == LogoutResult.Forbidden)
                return Forbid();
            if (result == LogoutResult.Unauthorized)
                return Unauthorized();
            return NoContent();
        }
    }

    public class RegisterRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }

        public RegisterModel ToModel() => new() { Login = Login, Password = Password };
    }

    public class LoginRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }

        public LoginModel ToModel() => new() { Login = Login, Password = Password };
    }

    public class RefreshRequest
    {
        [JsonPropertyName("refresh_token")] public required string RefreshToken { get; set; }

        public RefreshModel ToModel() => new() { RefreshToken = RefreshToken };
    }
    
    public class RefreshResponse
    {
        [JsonPropertyName("access_token")] 
        public required string AccessToken { get; set; }
        [JsonPropertyName("refresh_token")] 
        public required string RefreshToken { get; set; }
    }

    public class LoginResponse
    {
        [JsonPropertyName("access_token")] public required string AccessToken { get; set; }
        [JsonPropertyName("refresh_token")] public required string RefreshToken { get; set; }
    }
}