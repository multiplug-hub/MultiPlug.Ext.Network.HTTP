
namespace MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient.Subscription
{
    public class SubscriptionModel
    {
        public string Guid { get; set; }
        public string SubscriptionGuid { get; set; }
        public string[] Renames { get; set; }
        public bool[] AddToQuery { get; set; }
        public bool[] AddToBody { get; set; }
        public string SubscriptionId { get; set; }
        public bool SubscriptionConnected { get; set; }
    }
}
