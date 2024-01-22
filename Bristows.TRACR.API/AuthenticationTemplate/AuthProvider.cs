using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Bristows.TRACR.API.AuthenticationTemplate.Interfaces;
using Bristows.TRACR.Model.Models.POCOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Bristows.TRACR.API.AuthenticationTemplate;
internal sealed partial class AuthProvider : IAuthProvider
{
    private readonly JwtOptions _options;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthProvider> _logger;
    public AuthProvider(IOptions<JwtOptions> options, ILogger<AuthProvider> logger, IConfiguration configuration)
    {
        (_options, _configuration, _logger) = (options.Value, configuration, logger);
    }
    public void GenerateHash(out byte[] hash, out byte[] salt, params object[] password)
    {
        salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }                                      
        hash = KeyDerivation.Pbkdf2((string)password[0], salt, KeyDerivationPrf.HMACSHA256, 
        iterationCount: 100000, numBytesRequested: 256 / 8); 
    }
    public static bool VerifyHash(byte[] hash, byte[] salt, params object[] password)
    {
        byte[] computedHash = KeyDerivation.Pbkdf2((string)password[0], salt, KeyDerivationPrf.HMACSHA256, 100000, 256 / 8);
        return computedHash.SequenceEqual(hash);
    }
    public string BuildToken(string path = "AppSettings:Auth:Jwt", params User[] user)
    {
        Claim[] claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
            new (JwtRegisteredClaimNames.Jti, user[0].Id),
            new (JwtRegisteredClaimNames.Sub, user[0].Id),
            new (ClaimTypes.Role, user[0]?.Role ?? "unassigned")
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _options.Key.ToCharArray() ?? _configuration[$"{path}:Key"]!.ToCharArray())
        );                                               
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var JWT = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, null, DateTime.UtcNow.AddHours(1), credentials);
        var serializedJWT = new JwtSecurityTokenHandler().WriteToken(JWT);

        return serializedJWT;
    }
}
