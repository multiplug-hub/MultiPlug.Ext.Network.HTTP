using System.Drawing;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Properties;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Assets
{
    [Route("images/*")]
    public class ImageController : AssetsEndpoint
    {
        public Response Get(string theName)
        {
            ImageConverter converter = new ImageConverter();
            return new Response { RawBytes = (byte[])converter.ConvertTo(Resources.NetworksIcon, typeof(byte[])), MediaType = "image/png" };
        }
    }
}
