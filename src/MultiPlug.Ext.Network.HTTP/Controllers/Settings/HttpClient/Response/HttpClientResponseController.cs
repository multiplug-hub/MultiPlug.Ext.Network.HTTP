using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient.Response;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient.HttpResponse
{
    [Route("httpclient/response")]
    public class HttpClientResponseController : SettingsApp
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
                Template = Templates.SettingsHttpClientResponse
            };
        }

        public Response Post(ResponseModel theModel)
        {
            HttpClientComponent HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theModel.Guid);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            HttpClientSearch.UpdateProperties(new HttpClientProperties
            {
                ResponseEvent = new Base.Exchange.Event
                {
                    Id = theModel.EventId,
                    Description = theModel.EventDescription
                }
            });

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}