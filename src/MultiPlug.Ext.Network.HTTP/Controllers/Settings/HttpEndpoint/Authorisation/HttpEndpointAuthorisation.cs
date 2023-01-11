using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient
{
    [Route("httpendpoint/authorisation")]
    public class HttpEndpointAuthorisationController : SettingsApp
    {
        public Response Get(string id)
        {
            HttpEndpointProperties HttpEndpointSearch = Core.Instance.HttpEndpoints.FirstOrDefault(HttpEndpoint => HttpEndpoint.Guid == id);

            if (HttpEndpointSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            return new Response
            {
                Model = HttpEndpointSearch,
                Template = Templates.SettingsHttpEndpointAuthorisation
            };
        }
    }
}
