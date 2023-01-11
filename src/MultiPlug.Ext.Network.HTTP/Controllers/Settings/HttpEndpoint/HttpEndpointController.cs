using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint;
using MultiPlug.Ext.Network.HTTP.Models.Settings.HttpEndpoint;
using MultiPlug.Ext.Network.HTTP.Components.HttpEndpoint;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpEndpoint
{
    [Route("httpendpoint")]
    public class HttpEndpointController : SettingsApp
    {
        public Response Get(string id)
        {
            HttpEndpointProperties HttpEndpointSearch = Core.Instance.HttpEndpoints.FirstOrDefault(HttpEndpoint => HttpEndpoint.Guid == id );

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
                Template = Templates.SettingsHttpEndpoint
            };
        }

        public Response Post(HttpEndpointModel theModel)
        {
            HttpEndpointComponent HttpEndpointSearch = Core.Instance.HttpEndpoints.FirstOrDefault(HttpEndpoint => HttpEndpoint.Guid == theModel.Guid );

            if (HttpEndpointSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            HttpEndpointSearch.UpdateProperties(new HttpEndpointProperties
            {
                Url = theModel.Url.Replace(" ", "-"),
                Verb = theModel.Verb,
                RequestEvent = new Models.Exchange.Event
                {
                    Guid = theModel.Guid,
                    Id = theModel.EventId,
                    Description = theModel.EventDescription,
                    Subjects = theModel.SubjectValue == null ? new string[0]: theModel.SubjectValue,
                    RequestKeys = theModel.RequestKey == null ? new string[0]: theModel.RequestKey
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
