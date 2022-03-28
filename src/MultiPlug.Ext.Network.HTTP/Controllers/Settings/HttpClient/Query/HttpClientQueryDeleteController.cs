using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient.Params
{
    [Route("httpclient/query/delete")]
    public class HttpClientQueryDeleteController : SettingsApp
    {
        public Response Post(string id, string key)
        {
            HttpClientComponent HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == id);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if (HttpClientSearch.DeleteQueryParam(key))
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.Moved,
                    Location = Context.Referrer
                };
            }
            else
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
        }
    }
}
