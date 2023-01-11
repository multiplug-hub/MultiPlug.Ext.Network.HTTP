using System.Runtime.Serialization;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint;

namespace MultiPlug.Ext.Network.HTTP.Models.Load
{
    public class LoadRoot
    {
        [DataMember]
        public HttpClientProperties[] HttpClients { get; set; } = new HttpClientProperties[0];

        [DataMember]
        public HttpEndpointProperties[] HttpEndpoints { get; set; } = new HttpEndpointProperties[0];
    }
}
