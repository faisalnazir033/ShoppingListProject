using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingList.API.Settings;
using ShoppingList.Application.DTOs;
using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserService userService, IOptions<JwtSettings> options) : ControllerBase
{
    private readonly IUserService userService = userService;
    private readonly JwtSettings jwtSettings = options.Value;


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await userService.GetUserByEmailAsync(model.Email);
        if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid credentials" });

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtSettings.ValidIssuer,
            Audience = jwtSettings.ValidAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            NotBefore = DateTime.UtcNow,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static bool VerifyPassword(string password, string hashedPassword) => password.Equals(hashedPassword);

}