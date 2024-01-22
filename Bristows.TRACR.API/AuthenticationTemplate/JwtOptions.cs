namespace Bristows.TRACR.API.AuthenticationTemplate;
internal sealed class JwtOptions
{
    public string Key { get; internal init; }
    public string Issuer { get; internal init; }
    public string Audience { get; internal init; }
}
