using System.Runtime.Serialization;


namespace MultiPlug.Ext.Network.HTTP.Models.Exchange
{
    public class SubjectRename
    {
        [DataMember]
        public string Rename { get; set; }
        [DataMember]
        public bool AddToQuery { get; set; }
        [DataMember]
        public bool AddToBody { get; set; }
    }
}
