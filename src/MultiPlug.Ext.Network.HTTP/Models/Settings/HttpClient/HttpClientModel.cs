
namespace MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient
{
    public class HttpClientModel
    {
        public string Guid { get; set; }
        public string Verb { get; set; }
        public string Url { get; set; }
        public string[] Subscriptions { get; set; }
        public string[] SubjectValue { get; set; }
        public string[] SubjectRename { get; set; }
    }
}
