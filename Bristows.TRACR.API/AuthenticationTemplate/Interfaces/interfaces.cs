using Bristows.TRACR.Model.Models.POCOs;

namespace Bristows.TRACR.API.AuthenticationTemplate.Interfaces;
public interface IAuthProvider
{
    public void GenerateHash(out byte[] hash, out byte[] salt, params object[] password);
    public bool VerifyHash(byte[] hash, byte[] salt, params object[] password);
    public string BuildToken(string path, params User[] user);
}
internal interface IJwtBearerOptionsSetup<T> where T : class
{
    public void Configure(T options);
}
internal interface IJwtOptionsSetup<T> where T : class
{
    public void Configure(T options);
}