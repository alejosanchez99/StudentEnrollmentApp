using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentEnrollment.Api.DTOs.Authentication;
using StudentEnrollment.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentEnrollment.Api.Services;

public class AuthManager(UserManager<SchoolUser> userManager, IConfiguration configuration, IMapper mapper) : IAuthManager
{
    private readonly UserManager<SchoolUser> userManager = userManager;
    private readonly IConfiguration configuration = configuration;
    private readonly IMapper mapper = mapper;
    private SchoolUser? user;

    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return default;
        }

        bool isValidCredentials = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isValidCredentials)
        {
            return default;
        }

        string token = await GenerateTokenAsync();

        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id
        };
    }

    public async Task<IEnumerable<IdentityError>> Register(RegisterDto registerDto)
    {
        user = mapper.Map<SchoolUser>(registerDto);
        user.UserName = user.Email;

        IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
        }

        return result.Errors;
    }

    private async Task<string> GenerateTokenAsync()
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        IList<string> roles = await userManager.GetRolesAsync(user);
        List<Claim> roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
        IList<Claim> userClaims = await userManager.GetClaimsAsync(user);

        IEnumerable<Claim> claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("userId",user.Id),
            }.Union(userClaims)
             .Union(roleClaims);

        JwtSecurityToken token = new(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToInt32(configuration["JwtSettings:DurationInHours"])),
            signingCredentials: credentials
            );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
