using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkshopWebApi.Models;

namespace WorkshopWebApi.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(string login, string password)
    {
        if (login != "admin" && password != "admin")
        {
            return Unauthorized();
        }

        var token = await Authenticate("admin");

        return Ok(token);


    }

    private async Task<string> Authenticate(string userName)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
        ClaimsIdentity.DefaultRoleClaimType);

        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
        issuer: JwtAuthOptions.Issuer,
        audience: JwtAuthOptions.Audience,
        notBefore: now,
        claims: identity.Claims,
        expires: now.Add(TimeSpan.FromMinutes(JwtAuthOptions.LifeTime)),
        signingCredentials: new SigningCredentials(JwtAuthOptions.SecurityKey,
        SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return encodedJwt;
    }

}

