using System.Text;
using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient
{
    public class HttpClientProperties : MultiPlugBase
    {
        public const string VerbGet = "Get";
        public const string VerbPost = "Post";
        public const string VerbPut = "Put";

        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Verb { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public Exchange.Subscription[] Subscriptions { get; set; }
        [DataMember]
        public Event ResponseEvent { get; set; }
        [DataMember]
        public Header[] Headers { get; set; }
        [DataMember]
        public Param[] QueryParams { get; set; }
        [DataMember]
        public Param[] BodyParams { get; set; }
        [DataMember]
        public SubjectValueRename[] SubjectValueRenames { get; set; }
        public StringBuilder Log { get; set; } = new StringBuilder();
    }
}
