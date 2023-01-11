using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Components.HttpEndpoint;

namespace MultiPlug.Ext.Network.HTTP.Controllers.API
{
    [Route("/*")]
    public class RequestHandlerController : ApiEndpoint
    {
        public Response Get(string theName)
        {
            HttpEndpointComponent HttpEndpointSearch;

            if (Core.Instance.HttpEndpointsGets.TryGetValue(theName.ToLower(), out HttpEndpointSearch))
            {
                HttpEndpointSearch.Process(Context.QueryString);

                return new Response { StatusCode = System.Net.HttpStatusCode.OK };
            }
            else
            {
                return new Response { StatusCode = System.Net.HttpStatusCode.NotFound };
            }
        }

        public Response Post(string theName)
        {
            HttpEndpointComponent HttpEndpointSearch;

            if (Core.Instance.HttpEndpointsPosts.TryGetValue(theName.ToLower(), out HttpEndpointSearch))
            {
                HttpEndpointSearch.Process(Context.FormData);

                return new Response { StatusCode = System.Net.HttpStatusCode.OK };
            }
            else
            {
                return new Response { StatusCode = System.Net.HttpStatusCode.NotFound };
            }
        }
    }
}
