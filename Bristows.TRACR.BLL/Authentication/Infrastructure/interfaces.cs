using Bristows.TRACR.Model.Models.POCOs;

namespace Bristows.TRACR.BLL.Authentication.Infrastructure;
public interface IAuthProvider
{
    public void GenerateHash(CancellationToken ct, out byte[] hash, out byte[] salt, params object[] password) => throw new NotImplementedException();
    public bool VerifyHash(CancellationToken ct, byte[] hash, byte[] salt, params object[] password);
    public (string, DateTime) BuildToken(CancellationToken ct, string path, params User[] user);
}
internal interface IJwtBearerOptionsSetup<T> where T : class
{
    public void Configure(T options);
}
internal interface IJwtOptionsSetup<T> where T : class
{
    public void Configure(T options);
}