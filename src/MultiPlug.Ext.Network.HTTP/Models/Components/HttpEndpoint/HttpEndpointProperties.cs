using System.Runtime.Serialization;
using MultiPlug.Base;

namespace MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint
{
    public class HttpEndpointProperties : MultiPlugBase
    {
        public const string VerbGet = "Get";
        public const string VerbPost = "Post";

        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Verb { get; set; }
        [DataMember]
        public Exchange.Event RequestEvent { get; set; }
    }
}
