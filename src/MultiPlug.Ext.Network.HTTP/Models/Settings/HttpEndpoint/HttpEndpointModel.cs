
namespace MultiPlug.Ext.Network.HTTP.Models.Settings.HttpEndpoint
{
    public class HttpEndpointModel
    {
        public string Guid { get; set; }
        public string Verb { get; set; }
        public string Url { get; set; }
        public string EventId { get; set; }
        public string EventDescription { get; set; }
        public string[] SubjectValue { get; set; }
        public string[] RequestKey { get; set; }
    }
}
