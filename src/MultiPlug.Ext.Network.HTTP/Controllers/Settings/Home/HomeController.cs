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
                    HttpClients = Core.Instance.HttpClients
                },
                Template = Templates.SettingsHome
            };
        }
        public Response Post(NewHttpItem theModel)
        {
            if (theModel != null
                && theModel.Url != null
                && theModel.Verb != null
                && theModel.Url.Length == theModel.Verb.Length)
            {

                for (int Index = 0; Index < theModel.Url.Length; Index++)
                {
                    Core.Instance.HttpClientAdd(theModel.Verb[Index], theModel.Url[Index]);
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
