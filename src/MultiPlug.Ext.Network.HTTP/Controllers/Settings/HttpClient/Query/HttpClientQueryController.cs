using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient.Params;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient
{
    [Route("httpclient/query")]
    public class HttpClientQueryController : SettingsApp
    {
        public Response Get(string id)
        {
            HttpClientProperties HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == id);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            return new Response
            {
                Model = HttpClientSearch,
                Template = Templates.SettingsHttpClientQuery
            };
        }

        public Response Post(ParamsModel theModel)
        {
            HttpClientComponent HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theModel.Guid);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            if (theModel.Key != null && theModel.Value != null && theModel.Description != null &&
                (theModel.Key.Length == theModel.Value.Length) && (theModel.Key.Length == theModel.Description.Length))
            {
                var NewParams = new Param[theModel.Key.Length];

                for (int i = 0; i < theModel.Key.Length; i++)
                {
                    NewParams[i] = new Param
                    {
                        Key = theModel.Key[i],
                        Value = theModel.Value[i],
                        Description = theModel.Description[i]
                    };
                }

                HttpClientSearch.AddQueryParams(NewParams);
            }

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}
