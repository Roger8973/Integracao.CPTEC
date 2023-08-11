using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Integracao.CPTEC.Infra.Data.Identity
{
    public static class TokenService
    {
       public static async Task<string> GenerateAccessToken(UserManager<IdentityUser> userManager, IJwtService jwtService, string? email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var userRoles = await userManager.GetRolesAsync(user);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(await userManager.GetClaimsAsync(user));
            identityClaims.AddClaims(userRoles.Select(s => new Claim("role", s)));

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "https://refreshtoken.test",
                Audience = "Integracao.CPTEC.API",
                SigningCredentials = await jwtService.GetCurrentSigningCredentials(),
                Subject = identityClaims,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(60),
                IssuedAt = DateTime.UtcNow,
                TokenType = "at+jwt"
            });

            var encodedJwt = handler.WriteToken(securityToken);
            return encodedJwt;
        }

        public static async Task<string> GenerateRefreshToken(UserManager<IdentityUser> userManager, IJwtService jwtService, string? email)
        {
            var jti = Guid.NewGuid().ToString();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            };

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "https://refreshtoken.test",
                Audience = "Integracao.CPTEC.API",
                SigningCredentials = await jwtService.GetCurrentSigningCredentials(),
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(30),
                TokenType = "rt+jwt"
            });
            await UpdateLastGeneratedClaim(userManager, email, jti);
            var encodedJwt = handler.WriteToken(securityToken);
            return encodedJwt;
        }

        public static async Task UpdateLastGeneratedClaim(UserManager<IdentityUser> userManager, string? email, string jti)
        {
            var user = await userManager.FindByEmailAsync(email);
            var claims = await userManager.GetClaimsAsync(user);
            var newLastRtClaim = new Claim("LastRefreshToken", jti);

            var claimLastRt = claims.FirstOrDefault(f => f.Type == "LastRefreshToken");
            if (claimLastRt != null)
                await userManager.ReplaceClaimAsync(user, claimLastRt, newLastRtClaim);
            else
                await userManager.AddClaimAsync(user, newLastRtClaim);
        }
    }
}
