
using MultiPlug.Ext.Network.HTTP.Components.HttpEndpoint;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Models.Settings.Home
{
    public class HomeModel
    {
        public string Guid = string.Empty;
        public HttpClientProperties[] HttpClients { get; internal set; }
        public HttpEndpointComponent[] HttpEndpoints { get; internal set; }
    }
}
