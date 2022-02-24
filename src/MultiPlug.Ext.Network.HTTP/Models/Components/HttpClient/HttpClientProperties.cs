using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;
using System.Text;

namespace MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient
{
    public class HttpClientProperties : MultiPlugBase
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Verb { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public Subscription[] Subscriptions { get; set; } = new Subscription[0];


        public StringBuilder Log { get; set; } = new StringBuilder();
    }
}
