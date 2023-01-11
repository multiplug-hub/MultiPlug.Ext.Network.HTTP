using System;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;

namespace MultiPlug.Ext.Network.HTTP.Components.HttpClient
{
    public class HttpClientComponent : HttpClientProperties
    {
        internal event Action EventsUpdated;
        internal event Action SubscriptionsUpdated;

        private readonly System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();

        private string[] c_Subjects = new string[] { "Response", "StatusCode", "Headers" };

        private const string c_Group = "HttpClient";

        public HttpClientComponent(string theGuid)
        {
            this.Guid = theGuid;

            ResponseEvent = new Event
            {
                Guid = System.Guid.NewGuid().ToString(),
                Id = System.Guid.NewGuid().ToString(),
                Description = "HTTP Response",
                Subjects = c_Subjects,
                Group = c_Group
            };
            Subscriptions = new Models.Exchange.Subscription[0];
            Headers = new Header[0];
            QueryParams = new Param[0];
            BodyParams = new Param[0];
            SubjectValueRenames = new SubjectValueRename[0];
        }

        internal void UpdateProperties(HttpClientProperties theNewProperties)
        {
            bool FlagSubscriptionUpdated = false;
            bool FlagEventUpdated = false;

            if(!string.IsNullOrEmpty(theNewProperties.Verb))
            {
                Verb = theNewProperties.Verb;
            }

            if(theNewProperties.Url != null)
            {
                Url = theNewProperties.Url;
            }

            if( theNewProperties.Subscriptions != null)
            {
                var SubscriptionsList = Subscriptions.ToList();

                foreach( var Subscription in theNewProperties.Subscriptions)
                {
                    if( string.IsNullOrEmpty( Subscription.Guid ))
                    {
                        Subscription.Guid = System.Guid.NewGuid().ToString();
                        SubscriptionsList.Add(Subscription);
                        FlagSubscriptionUpdated = true;
                        Subscription.EventHandler = new HttpClientEventHandler(this, m_HttpClient, Subscription);

                        Models.Exchange.Subscription.Sync(Subscription);
                    }
                    else
                    {
                        var SubscriptionSearch = Subscriptions.FirstOrDefault(sub => sub.Guid == Subscription.Guid);

                        if( SubscriptionSearch != null)
                        {
                            if (Base.Exchange.Subscription.Merge(SubscriptionSearch, Subscription)) { FlagSubscriptionUpdated = true; }

                            Models.Exchange.Subscription.Merge(SubscriptionSearch, Subscription);
                        }
                        else
                        {
                            Subscription.EventHandler = new HttpClientEventHandler(this, m_HttpClient, Subscription);
                            SubscriptionsList.Add(Subscription);
                            FlagSubscriptionUpdated = true;
                            Models.Exchange.Subscription.Sync(Subscription); //Is this needed?
                        }
                    }
                }

                Subscriptions = SubscriptionsList.ToArray();
            }

            if(theNewProperties.Headers != null)
            {
                AddHeaders(theNewProperties.Headers);
            }

            if (theNewProperties.QueryParams != null)
            {
                AddQueryParams(theNewProperties.QueryParams);
            }

            if (theNewProperties.BodyParams != null)
            {
                AddBodyParams(theNewProperties.BodyParams);
            }

            if(theNewProperties.SubjectValueRenames != null)
            {
                SubjectValueRenames = theNewProperties.SubjectValueRenames;
            }

            if( theNewProperties.ResponseEvent != null)
            {
                FlagEventUpdated = Event.Merge(ResponseEvent, theNewProperties.ResponseEvent);
                ResponseEvent.Subjects = c_Subjects;
            }

            if (FlagEventUpdated) { EventsUpdated?.Invoke(); }
            if (FlagSubscriptionUpdated) { SubscriptionsUpdated?.Invoke(); }
        }

        private List<Param> AddParams(Param[] theNewParams, List<Param> ParamsList)
        {
            foreach (var NewParam in theNewParams)
            {
                if (string.IsNullOrEmpty(NewParam.Key) || NewParam.Value == null || NewParam.Description == null)
                {
                    continue;
                }

                Param Search = ParamsList.FirstOrDefault(Header => Header.Key.Equals(NewParam.Key, StringComparison.OrdinalIgnoreCase));

                if (Search == null)
                {
                    ParamsList.Add(NewParam);
                }
            }

            return ParamsList;
        }

        internal void AddBodyParams(Param[] theNewParams)
        {
            BodyParams = AddParams(theNewParams, BodyParams.ToList()).ToArray();
        }

        internal void AddQueryParams(Param[] theNewParams)
        {
            QueryParams = AddParams(theNewParams, QueryParams.ToList()).ToArray();
        }

        private bool DeleteParam( string theKey, List<Param> ParamsList)
        {
            Param Search = ParamsList.FirstOrDefault(Header => Header.Key == theKey);

            if (Search != null)
            {
                ParamsList.Remove(Search);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool DeleteQueryParam(string key)
        {
            List<Param> ParamsList = QueryParams.ToList();

            var Result = DeleteParam(key, ParamsList);

            QueryParams = ParamsList.ToArray();

            return Result;
        }

        internal bool RenameDelete(string renamevalue)
        {
            bool Found = false;

            SubjectValueRenames = SubjectValueRenames.Where(Rename =>
            {
                if (Rename.Value == renamevalue)
                {
                    Found = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }).ToArray();

            return Found;
        }

        internal bool DeleteBodyParam(string key)
        {
            List<Param> ParamsList = BodyParams.ToList();

            bool Result = DeleteParam(key, ParamsList);

            BodyParams = ParamsList.ToArray();

            return Result;
        }

        internal void AddHeaders(Header[] theNewHeaders)
        {
            List<Header> HeadersList = Headers.ToList();

            foreach( var NewHeader in theNewHeaders)
            {
                if(string.IsNullOrEmpty(NewHeader.Key) || NewHeader.Value == null || NewHeader.Description == null)
                {
                    continue;
                }

                Header Search = HeadersList.FirstOrDefault(Header => Header.Key.Equals( NewHeader.Key, StringComparison.OrdinalIgnoreCase));

                if(Search == null)
                {
                    HeadersList.Add(NewHeader);
                }
            }

            Headers = HeadersList.ToArray();
        }

        internal bool DeleteHeader(string key)
        {
            List<Header> HeadersList = Headers.ToList();
            Header Search = HeadersList.FirstOrDefault(Header => Header.Key == key);

            if( Search != null)
            {
                HeadersList.Remove(Search);
                Headers = HeadersList.ToArray();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool SubscriptionDelete(string theSubscriptionGuid)
        {
            var SubscriptionsList = Subscriptions.ToList();

            Models.Exchange.Subscription SubscriptionSearch =  SubscriptionsList.FirstOrDefault(Subscription => Subscription.Guid == theSubscriptionGuid);

            if( SubscriptionSearch != null)
            {
                SubscriptionSearch.EventHandler.Remove();

                bool Result = SubscriptionsList.Remove(SubscriptionSearch);

                Subscriptions = SubscriptionsList.ToArray();

                if(Result)
                {
                    SubscriptionsUpdated?.Invoke();
                }

                return Result;
            }

            return false;
        }
    }
}
