using System.Runtime.Serialization;

namespace MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient
{
    public class Param
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
