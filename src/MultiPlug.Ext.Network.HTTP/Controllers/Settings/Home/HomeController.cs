using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Settings.Home;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.Home
{
    [Route("")]
    public class HomeController : SettingsApp
    {
        public Response Get()
        {
            return new Response
            {
                Model = new HomeModel
                {
                    HttpClients = Core.Instance.HttpClients,
                    HttpEndpoints = Core.Instance.HttpEndpoints
                },
                Template = Templates.SettingsHome
            };
        }
        public Response Post(NewHttpItem theModel)
        {
            if (theModel != null
                && theModel.ClientUrl != null
                && theModel.ClientVerb != null
                && theModel.ClientUrl.Length == theModel.ClientVerb.Length)
            {

                for (int Index = 0; Index < theModel.ClientUrl.Length; Index++)
                {
                    Core.Instance.HttpClientAdd(theModel.ClientVerb[Index], theModel.ClientUrl[Index]);
                }
            }

            if (theModel != null
                && theModel.EndpointUrl != null
                && theModel.EndpointVerb != null)
            {
                for (int Index = 0; Index < theModel.EndpointUrl.Length; Index++)
                {
                    Core.Instance.HttpEndpointAdd(theModel.EndpointUrl[Index], theModel.EndpointVerb[Index]);
                }
            }

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
