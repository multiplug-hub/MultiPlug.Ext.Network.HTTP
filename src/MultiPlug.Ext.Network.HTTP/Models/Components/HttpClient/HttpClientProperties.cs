using System.Text;
using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;

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
        public Subscription[] Subscriptions { get; set; }
        [DataMember]
        public Header[] Headers { get; set; }
        public StringBuilder Log { get; set; } = new StringBuilder();
    }
}
