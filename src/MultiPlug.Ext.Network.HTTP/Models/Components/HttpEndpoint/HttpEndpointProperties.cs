using System.Runtime.Serialization;
using MultiPlug.Base;

namespace MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint
{
    public class HttpEndpointProperties : MultiPlugBase
    {
        [DataMember]
        public string Guid { get; set; }
    }
}
