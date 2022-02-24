using System.Runtime.Serialization;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;


namespace MultiPlug.Ext.Network.HTTP.Models.Load
{
    public class LoadRoot
    {
        [DataMember]
        public HttpClientProperties[] HttpClients { get; set; } = new HttpClientProperties[0];
    }
}
