using Bristows.TRACR.API.AuthenticationTemplate.Interfaces;
using Microsoft.Extensions.Options;

namespace Bristows.TRACR.API.AuthenticationTemplate;
internal sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>, IJwtOptionsSetup<JwtOptions>
{
    private readonly IConfiguration _config;
    private readonly string _path;
    public JwtOptionsSetup(IConfiguration config, string path = "AppSettings:Auth:Jwt")
    {
        (_config, _path) = (config, path);
    }
    public void Configure(JwtOptions options)
    {
        _config.GetSection(_path).Bind(options);
        
    }
}
