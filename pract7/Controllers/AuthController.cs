using Microsoft.AspNetCore.Mvc;
using pract7.Application.Services;
using System.Text.Json.Serialization;

namespace pract7.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(UserService userService) : ControllerBase
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
            return Ok(tokens);
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
        [JsonPropertyName("refresh_token")]
        public required string RefreshToken { get; set; }

        public RefreshModel ToModel() => new() { RefreshToken = RefreshToken };
    }

    public class LoginResponse
    {
        [JsonPropertyName("access_token")] public required string AccessToken { get; set; }
        [JsonPropertyName("refresh_token")] public required string RefreshToken { get; set; }
    }
}