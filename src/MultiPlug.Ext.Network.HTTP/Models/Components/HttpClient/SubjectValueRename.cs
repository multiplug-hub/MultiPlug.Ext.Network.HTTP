using System.Runtime.Serialization;

namespace MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient
{
    public class SubjectValueRename
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Rename { get; set; }
    }
}
