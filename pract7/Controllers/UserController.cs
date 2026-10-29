using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pract7.Application.Services;

namespace pract7.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(UserSerivce userSerivce) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = userSerivce.TryRegisterUser(request.ToModel());
            if (result == RegisterResult.Conflict)
                return Conflict("Такой юзер уже есть");
            return Ok("Пользователь зарегистрирован");
        }
    }


    public class RegisterRequest()
    {
        public string Login { get; set; }
        public string Password{ get; set; }


        public RegisterModel ToModel () => new() { Login = Login, Password = Password };
    }
}
