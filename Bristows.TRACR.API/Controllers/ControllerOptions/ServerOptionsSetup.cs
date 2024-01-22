using Bristows.TRACR.API.AuthenticationTemplate;
using Bristows.TRACR.API.Controllers.ControllerOptions.Interfaces;
using Microsoft.Extensions.Options;

internal sealed class ServerOptionsSetup : IConfigureOptions<ServerOptions>, IServerOptionsSetup<ServerOptions>
{
    private readonly IConfiguration _config;
    private readonly string _path;
    private readonly string _server;
    public ServerOptionsSetup(IConfiguration config, string path = "AppSettings:Servers:ResourceServers", string server = "SERV1")
    {
        (_config, _path, _server) = (config, path, server);        
    }
    public void Configure(ServerOptions options)
    {
        _config.GetSection($"{_path}:{_server}").Bind(options);
    }
}