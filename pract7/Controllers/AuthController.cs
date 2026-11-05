using Microsoft.AspNetCore.Mvc;
using pract7.Application.Services;
using System.Text.Json.Serialization;

namespace pract7.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController(UserSerivce userSerivce) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = userSerivce.RegisterUser(request.ToModel());
            if (result == RegisterResult.Conflict)
                return Conflict("Такой юзер уже есть");
            return Ok("Пользователь зарегистрирован");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = userSerivce.LoginUser(request.ToModel());
            if (result.result == LoginResult.NotFound
                || result.result == LoginResult.IncorrectPassword)
                return Unauthorized("Неверная пара логин/пароль");
            return Ok(new LoginResponse { AccessToken = result.token });
        }
    }

    public class RegisterRequest()
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public RegisterModel ToModel() => new() { Login = Login, Password = Password };
    }

    public class LoginRequest()
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        public LoginModel ToModel() => new() { Login = Login, Password = Password };
    }

    public class LoginResponse()
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
    }
}
