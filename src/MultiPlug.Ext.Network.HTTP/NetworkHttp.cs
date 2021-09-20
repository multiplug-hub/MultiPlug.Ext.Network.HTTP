using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;
using MultiPlug.Ext.Network.HTTP.Controllers;
using MultiPlug.Ext.Network.HTTP.Properties;

namespace MultiPlug.Ext.Network.HTTP
{
    public class NetworkHttp : MultiPlugExtension
    {
        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate( Templates.SettingsHome, Resources.SettingsHome ),
                };
            }
        }
    }
}
