using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient
{
    [Route("httpclient")]
    public class HttpClientController : SettingsApp
    {
        public Response Get(string id)
        {
            HttpClientProperties HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == id);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            return new Response
            {
                Model = HttpClientSearch,
                Template = Templates.SettingsHttpClient
            };
        }

        public Response Post(HttpClientModel theModel)
        {
            HttpClientComponent HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theModel.Guid);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            Subscription[] Subscriptions = null;


            if( theModel.Subscriptions != null)
            {
                Subscriptions = theModel.Subscriptions.Select(sub => new Subscription { Id = sub }).ToArray();
            }

            HttpClientSearch.UpdateProperties(new HttpClientProperties { Url = theModel.Url, Verb = theModel.Verb, Subscriptions = Subscriptions });

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
