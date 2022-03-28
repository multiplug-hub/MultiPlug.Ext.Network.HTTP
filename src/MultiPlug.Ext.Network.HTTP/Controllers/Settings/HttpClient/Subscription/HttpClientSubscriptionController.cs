using System.Linq;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Settings.HttpClient.Subscription;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;
using System.Collections.Generic;
using MultiPlug.Ext.Network.HTTP.Models.Exchange;

namespace MultiPlug.Ext.Network.HTTP.Controllers.Settings.HttpClient.HttpClientSubscription
{
    [Route("httpclient/subscription")]
    public class HttpClientSubscriptionController : SettingsApp
    {
        public Response Get(string id, string sub)
        {
            HttpClientProperties HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == id);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            var SubscriptionSearch = HttpClientSearch.Subscriptions.FirstOrDefault(Subscription => Subscription.Guid == sub);

            if (SubscriptionSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            return new Response
            {
                Model = new SubscriptionModel
                {
                    Guid = id, 
                    SubscriptionGuid = sub,
                    SubscriptionId = SubscriptionSearch.Id,
                    SubscriptionConnected = SubscriptionSearch.Connected,
                    AddToBody = SubscriptionSearch.SubjectRenames.Select(Subject => Subject.AddToBody).ToArray(),
                    AddToQuery = SubscriptionSearch.SubjectRenames.Select(Subject => Subject.AddToQuery).ToArray(),
                    Renames = SubscriptionSearch.SubjectRenames.Select(Subject => Subject.Rename).ToArray()
                },
                Template = Templates.SettingsHttpClientSubscription
            };
        }

        public Response Post(SubscriptionModel theModel)
        {
            HttpClientComponent HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theModel.Guid);

            if (HttpClientSearch == null)
            {
                return new Response
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            List<bool> AddToBodyList = new List<bool>();

            if(theModel.AddToBody != null)
            {
                for (int i = 0; i < theModel.AddToBody.Length; i++)
                {
                    AddToBodyList.Add(theModel.AddToBody[i]);

                    if(theModel.AddToBody[i] == true)
                    {
                        i++; // Skip the Next because it will be a false value, but a duplicate of this index value.
                    }
                }
            }

            List<bool> AddToQueryList = new List<bool>();

            if(theModel.AddToQuery != null)
            {
                for (int i = 0; i < theModel.AddToQuery.Length; i++)
                {
                    AddToQueryList.Add(theModel.AddToQuery[i]);

                    if (theModel.AddToQuery[i] == true)
                    {
                        i++; // Skip the Next because it will be a false value, but a duplicate of this index value.
                    }
                }
            }

            SubjectRename[] SubjectRenames = null;

            if( ( AddToBodyList.Count() == AddToQueryList.Count() ) && theModel.Renames != null && ( theModel.Renames.Length == AddToQueryList.Count() ))
            {
                SubjectRenames = new SubjectRename[theModel.Renames.Length];

                for (int i = 0; i < theModel.Renames.Length; i++)
                {
                    SubjectRenames[i] = new SubjectRename { Rename = theModel.Renames[i], AddToBody = AddToBodyList[i], AddToQuery = AddToQueryList[i] };
                }
            }
            else
            {
                SubjectRenames = new SubjectRename[0];
            }

            HttpClientSearch.UpdateProperties( new HttpClientProperties
            {

                Subscriptions = new Models.Exchange.Subscription[] { new Models.Exchange.Subscription
                {
                    Guid = theModel.SubscriptionGuid,
                    Id = theModel.SubscriptionId,
                    SubjectRenames = SubjectRenames
                } }
            });

            return new Response
            {
                StatusCode = System.Net.HttpStatusCode.Moved,
                Location = Context.Referrer
            };
        }
    }
}