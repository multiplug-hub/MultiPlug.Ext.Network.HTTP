﻿using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient
{
    [Route("httpclient/delete")]
    public class HttpClientDeleteController : SettingsApp
    {
        public Response Post(string id, string sub, string renamevalue)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            
            if( sub != null) // Subscription Delete
            {
                if (Core.Instance.HttpClientSubscriptionDelete(id, sub))
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
            else if (renamevalue != null) // Rename Subject Value Delete
            {
                if (Core.Instance.HttpClientRenameDelete(id, renamevalue))
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
            else
            {
                if (Core.Instance.HttpClientDelete(id))
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
}
