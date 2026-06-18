using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoImovel.API.Data;
using AutoImovel.Shared.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AutoImovel.API.Features.Auth;

public static class LoginEndpoint
{
    public static void MapLogin(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async (
            LoginRequest request,
            UserManager<IdentityUser> userManager,
            AppDbContext db,
            IConfiguration config) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
                return Results.Unauthorized();

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Investidor";

            Guid? investidorId = null;
            if (role == "Investidor")
            {
                var investidor = await db.Investidores
                    .FirstOrDefaultAsync(i => i.Email == request.Email);
                investidorId = investidor?.Id.Value;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email ?? ""),
                new(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "SuperSecretKeyForAutoImovel@2026!ChangeMe"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"] ?? "AutoImovel",
                audience: config["Jwt:Audience"] ?? "AutoImovel",
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return Results.Ok(new LoginResponse(
                new JwtSecurityTokenHandler().WriteToken(token),
                user.Email ?? "",
                role,
                investidorId));
        })
        .WithName("Login")
        .AllowAnonymous();
    }
}
