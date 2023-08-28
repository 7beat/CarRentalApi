using CarRental.Application.Contracts.Identity;
using CarRental.Application.Exceptions;
using CarRental.Application.Features.Auth.Commands;
using CarRental.Application.Models.Auth;
using CarRental.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRental.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IConfiguration configuration;
    private ApplicationUser? user;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
    }

    public async Task<AuthResult> LoginAsync(LoginCommand request)
    {
        user = await userManager.FindByEmailAsync(request.Email) ??
            throw new UnAuthorizedException("Wrong SignIn Credentials");

        if (!await userManager.CheckPasswordAsync(user, request.Password))
        {
            await userManager.AccessFailedAsync(user);
            throw new UnAuthorizedException("Wrong SignIn Credentials");
        }

        if (await userManager.IsLockedOutAsync(user))
            throw new UnAuthorizedException($"Account locked out for: {user.LockoutEnd - DateTime.UtcNow}");

        return await CreateTokenAsync();
    }

    public async Task<AuthResult> RegisterAsync(RegisterCommand request)
    {
        var newUser = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthday = request.Birthday,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
            throw new BadRequestException("Could not register User");

        await userManager.AddToRoleAsync(newUser, "User");
        user = newUser;

        return await CreateTokenAsync();
    }

    private async Task<AuthResult> CreateTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            Expiration = tokenOptions.ValidTo,
            Succeded = true
        };
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
        };

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}
