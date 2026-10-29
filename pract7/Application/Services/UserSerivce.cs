using Microsoft.AspNetCore.Identity;
using pract7.Application.DBContext;
using pract7.Application.Models;

namespace pract7.Application.Services
{
    public class UserSerivce (IPasswordHasher<User> hasher, KuznetsovTaskDbContext context)
    {
        public RegisterResult TryRegisterUser (RegisterModel model)
        {
            if (context.Users.Any(x => x.Login == model.Login))
                return RegisterResult.Conflict;

            var user = new User { Login = model.Login, Role = Role.User.ToString() };
            user.PasswordHash = hasher.HashPassword(user, model.Password);
            context.Users.Add(user);
            context.SaveChanges();
            return RegisterResult.Success;
        }
    }
    public class RegisterModel ()
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public enum RegisterResult{
        Success,
        Conflict
    }
}
