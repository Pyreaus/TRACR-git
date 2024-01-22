using Bristows.TRACR.API.AuthenticationTemplate;

namespace Bristows.TRACR.API.Controllers.ControllerOptions.Interfaces;
internal interface IServerOptionsSetup<T> where T : class
{
    public void Configure(T options);
}