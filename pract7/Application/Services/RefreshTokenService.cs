using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pract7.Application.DBContext;
using pract7.Application.Models;

namespace pract7.Application.Services;

public class RefreshTokenService(
    Trpo7Context context,
    RevokedTokenService revokedTokens,
    IConfiguration configuration)
{
    private static string Hash(string token) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

    public string GenerateJwtToken(User user)
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
            issuer: "ExampleApi",
            audience: "ExampleApiClient",
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(60),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
            .Replace('+', '-').Replace('/', '_').TrimEnd('=');
        context.RefreshTokens.Add(new RefreshToken
        {
            UserId = user.Id, TokenHash = Hash(token), ExpiresAt = DateTime.UtcNow.AddDays(7)
        });
        context.SaveChanges();
        return token;
    }

    public RefreshResult Rotate(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return new RefreshResult { ErrorMessage = "Недействительный Refresh Token" };
        using var transaction = context.Database.BeginTransaction();
        var hash = Hash(token);
        var stored = context.RefreshTokens.AsNoTracking().SingleOrDefault(x => x.TokenHash == hash);
        var user = stored == null ? null : context.Users.SingleOrDefault(x => x.Id == stored.UserId);
        if (stored == null || stored.ExpiresAt is null || stored.ExpiresAt <= DateTime.UtcNow || user == null)
            return new RefreshResult { ErrorMessage = "Недействительный Refresh Token" };
        if (context.RefreshTokens.Where(x => x.Id == stored.Id && x.ExpiresAt > DateTime.UtcNow).ExecuteDelete() != 1)
            return new RefreshResult { ErrorMessage = "Refresh Token уже использован" };
        var access = GenerateJwtToken(user);
        var refresh = GenerateRefreshToken(user);
        transaction.Commit();
        return new RefreshResult { AccessToken = access, RefreshToken = refresh };
    }

    public LogoutResult Logout(string token, string login, string jti, DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(token)) return LogoutResult.Unauthorized;
        using var transaction = context.Database.BeginTransaction();
        var hash = Hash(token);
        var stored = context.RefreshTokens.AsNoTracking().SingleOrDefault(x => x.TokenHash == hash);
        if (stored == null || stored.ExpiresAt is null || stored.ExpiresAt <= DateTime.UtcNow)
            return LogoutResult.Unauthorized;
        var user = context.Users.SingleOrDefault(x => x.Id == stored.UserId);
        if (user == null) return LogoutResult.Unauthorized;
        if (user.Login != login) return LogoutResult.Forbidden;
        if (context.RefreshTokens.Where(x => x.Id == stored.Id && x.UserId == user.Id && x.ExpiresAt > DateTime.UtcNow).ExecuteDelete() != 1)
            return LogoutResult.Unauthorized;
        revokedTokens.Revoke(jti, expiresAt);
        transaction.Commit();
        return LogoutResult.Success;
    }
}

public enum LogoutResult { Success, Unauthorized, Forbidden }
