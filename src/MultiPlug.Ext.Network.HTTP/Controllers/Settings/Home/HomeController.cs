using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Settings;

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
                },
                Template = Templates.SettingsHome
            };
        }
    }
}
