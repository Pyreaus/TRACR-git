

using System.Text;
using System.Text.Json;
using Bristows.TRACR.API.AuthenticationTemplate.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Bristows.TRACR.API.AuthenticationTemplate;
internal sealed partial class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>, IJwtBearerOptionsSetup<JwtBearerOptions>
{
    public JwtOptions _options;
    public JwtBearerOptionsSetup(IOptions<JwtOptions> options) => _options = options.Value;
    public void Configure(JwtBearerOptions opts)
    {
        opts.TokenValidationParameters = new()
        {
            ClockSkew = new TimeSpan(0, 0, 30),
            ValidateIssuer = true, ValidIssuer = _options.Issuer,
            ValidateAudience = true, ValidAudience = _options.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _options.Key.ToCharArray()))
        };
        opts.Events = new JwtBearerEvents()
        {
            OnChallenge = ctx =>
            {
                ctx.HandleResponse();
                ctx.Response.ContentType = "application/json";
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;

                if (string.IsNullOrEmpty(ctx.Error)) ctx.Error = "invalid_token";
                if (string.IsNullOrEmpty(ctx.ErrorDescription)) ctx.ErrorDescription = "A valid security token is required";

                if (ctx.AuthenticateFailure != null && ctx.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                {
                    var authException = ctx.AuthenticateFailure as SecurityTokenExpiredException;
                    ctx.Response.Headers.Add("x-token-expired", authException!.Expires.ToString("o"));
                    ctx.ErrorDescription = $"Expired on: {authException.Expires:o}";
                }

                return ctx.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        new {
                            error = ctx.Error,
                            error_description = ctx.ErrorDescription
                        }
                    )
                );
            }
         };
    }
}
