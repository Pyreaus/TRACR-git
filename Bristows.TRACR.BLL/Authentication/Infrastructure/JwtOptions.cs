namespace Bristows.TRACR.BLL.Authentication.Infrastructure;
public sealed class JwtOptions
{
    public string? Key { get; internal set; }
    public string? Issuer { get; internal set; }
    public string? Audience { get;internal set; }
    public string? ApiKey { get; internal set; }
}
