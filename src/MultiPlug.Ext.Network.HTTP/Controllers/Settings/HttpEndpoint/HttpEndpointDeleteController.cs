using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpEndpoint
{
    [Route("httpendpoint/delete")]
    public class HttpEndpointDeleteController : SettingsApp
    {
        public Response Post(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }


            if (Core.Instance.HttpEndpointDelete(id))
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
