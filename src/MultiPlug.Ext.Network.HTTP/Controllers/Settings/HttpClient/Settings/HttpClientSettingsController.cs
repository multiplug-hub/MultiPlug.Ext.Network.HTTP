﻿using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient
{
    [Route("httpclient/settings")]
    public class HttpClientSettingsController : SettingsApp
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
                Template = Templates.SettingsHttpClientSettings
            };
        }
    }
}
