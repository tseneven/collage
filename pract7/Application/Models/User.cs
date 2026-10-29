using System;
using System.Collections.Generic;

namespace pract7.Application.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}


public enum Role
{
    User,
    Admin
}

public static class RoleExtension
{
    public static string toString(this Role role)
    {
        switch (role)
        {
            case Role.User:
                return "User";
            case Role.Admin:
                return "Admin";
            default:
                throw new ArgumentOutOfRangeException("Расшили модельку ролей?");
        };
    }
}