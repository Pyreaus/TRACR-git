using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Bristows.TRACR.Model.Models.POCOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Bristows.TRACR.BLL.Authentication.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Bristows.TRACR.API.Authentication;
public sealed class AuthProvider : IAuthProvider
{   
    private readonly JwtOptions _options;
    private readonly IConfiguration _configuration;
    public AuthProvider(IConfiguration configuration, IOptions<JwtOptions> options)
    {
        (_configuration, _options) = (configuration, options.Value);
    }
    public sealed class HashSaltResolver : IValueResolver<RegisterUserReq, User, (byte[] StoredHash, byte[] StoredSalt)>
    {
        public (byte[] StoredHash, byte[] StoredSalt) Resolve(RegisterUserReq src, User dest, (byte[] StoredHash, byte[] StoredSalt) destMember, ResolutionContext ctx)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            byte[] hash = KeyDerivation.Pbkdf2(src.Password, salt, KeyDerivationPrf.HMACSHA256, numBytesRequested: 256 / 8, iterationCount: 100000); 
            (byte[] StoredHash, byte[] StoredSalt) = (hash, salt);
            return (StoredHash, StoredSalt);
        }
    }
    public void GenerateHash(CancellationToken ct, out byte[] hash, out byte[] salt, params object[] password)
    {
        salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
        hash = KeyDerivation.Pbkdf2((string)password[0], salt, KeyDerivationPrf.HMACSHA256, 256 / 8, 100000); 
    }
    public bool VerifyHash(CancellationToken ct, byte[] hash, byte[] salt, params object[] password)
    {
        byte[] computedHash = KeyDerivation.Pbkdf2((string)password[0], salt, KeyDerivationPrf.HMACSHA256, 100000, 256 / 8);
        return computedHash.SequenceEqual(hash);
    }
    public (string, DateTime) BuildToken(CancellationToken ct, string? path = null, params User[] user)
    {
        Claim[] claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new (ClaimsIdentity.DefaultRoleClaimType, user[0].Role ?? "Unassigned"),
            new (JwtRegisteredClaimNames.Jti, user[0].Id!),
            new (JwtRegisteredClaimNames.Sub, user[0].Email!)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key!.ToCharArray() ?? _configuration[$"{path ?? "AppSettings:Auth:Jwt"}:Key"]!.ToCharArray()));                                               
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var JWT = new JwtSecurityToken(
            _options.Issuer, _options.Audience, claims,
            notBefore: null,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);
        var serializedJWT = new JwtSecurityTokenHandler().WriteToken(JWT);

        return (serializedJWT, JWT.ValidTo);
    }
}
