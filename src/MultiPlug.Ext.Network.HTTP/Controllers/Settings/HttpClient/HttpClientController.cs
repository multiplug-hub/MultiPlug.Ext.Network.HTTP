using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient
{
    [Route("httpclient")]
    public class HttpClientController : SettingsApp
    {
        public Response Get(string id)
        {
            HttpClientProperties HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(HttpClient => HttpClient.Guid == id);

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
            HttpClientComponent HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(HttpClient => HttpClient.Guid == theModel.Guid);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            Models.Exchange.Subscription[] Subscriptions = null;


            if( theModel.Subscriptions != null)
            {
                Subscriptions = theModel.Subscriptions.Select(sub => new Models.Exchange.Subscription { Id = sub }).ToArray();
            }

            SubjectValueRename[] SubjectValueRenames = null;

            if (theModel.SubjectValue != null
                && theModel.SubjectRename != null
                && theModel.SubjectValue.Length == theModel.SubjectRename.Length)
            {

                SubjectValueRenames = new SubjectValueRename[theModel.SubjectValue.Length];

                for ( int i = 0; i < theModel.SubjectValue.Length; i++)
                {
                    SubjectValueRenames[i] = new SubjectValueRename
                    {
                        Value = theModel.SubjectValue[i],
                        Rename = theModel.SubjectRename[i]
                    };
                }
            }

            HttpClientSearch.UpdateProperties(new HttpClientProperties
            {
                Url = theModel.Url,
                Verb = theModel.Verb,
                Subscriptions = Subscriptions,
                SubjectValueRenames = SubjectValueRenames
            });

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
