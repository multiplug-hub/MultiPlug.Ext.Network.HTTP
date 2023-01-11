using System.Runtime.Serialization;

namespace MultiPlug.Ext.Network.HTTP.Models.Exchange
{
    public class Event : Base.Exchange.Event
    {
        [DataMember]
        public string[] RequestKeys { get; set; }
    }
}
