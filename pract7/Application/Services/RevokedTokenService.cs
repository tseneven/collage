using Microsoft.EntityFrameworkCore;
using pract7.Application.DBContext;
using pract7.Application.Models;

namespace pract7.Application.Services;

public class RevokedTokenService(Trpo7Context context)
{
    public async Task<bool> IsRevokedAsync(string jti)
    {
        await context.RevokedTokens.Where(x => x.ExpiresAt <= DateTime.UtcNow).ExecuteDeleteAsync();
        return await context.RevokedTokens.AnyAsync(x => x.Jti == jti);
    }

    public void Revoke(string jti, DateTime expiresAt)
    {
        context.RevokedTokens.Add(new RevokedToken { Jti = jti, ExpiresAt = expiresAt });
        context.SaveChanges();
    }
}
