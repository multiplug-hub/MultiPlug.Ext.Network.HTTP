using System;
using System.Linq;
using System.Net.Http;
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

        public HttpClientComponent(string theGuid)
        {
            this.Guid = theGuid;
        }

        internal void UpdateProperties(HttpClientProperties theNewProperties)
        {
            bool FlagSubscriptionUpdated = false;

            Verb = theNewProperties.Verb;
            Url = theNewProperties.Url;

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

                        Subscription.Event += OnEvent;
                    }
                    else
                    {
                        var SubscriptionSearch = Subscriptions.FirstOrDefault(sub => sub.Guid == Subscription.Guid);

                        if( SubscriptionSearch != null)
                        {
                            if (Subscription.Merge(SubscriptionSearch, Subscription)) { FlagSubscriptionUpdated = true; }
                        }
                        else
                        {
                            Subscription.Event += OnEvent;
                            SubscriptionsList.Add(Subscription);
                            FlagSubscriptionUpdated = true;
                        }
                    }
                }

                Subscriptions = SubscriptionsList.ToArray();

                if (FlagSubscriptionUpdated) { SubscriptionsUpdated?.Invoke(); }
            }
        }

        private void OnEvent(SubscriptionEvent theEvent)
        {
            if( Verb == "Get")
            {
                var AsArray = theEvent.Payload.Subjects.Select(Subject => string.Format("{0}={1}", Subject.Subject, Subject.Value));

                string QueryString = "";

                if (AsArray.Count() > 0)
                {
                    QueryString = "?" + string.Join("&", AsArray);
                }

                try
                {
                    m_HttpClient.GetStringAsync(Url + QueryString).Wait();
                }
                catch(Exception theException)
                {
                    Log.AppendLine(" - - - - -");
                    Log.AppendLine(theException.Message);

                    var Inner = theException.InnerException;

                    while (Inner != null)
                    {
                        Log.AppendLine(Inner.Message);

                        Inner = Inner.InnerException;
                    }
                    Log.AppendLine(" - - - - -");
                }   
            }
            else //Post
            {
                var KeyValues = theEvent.Payload.Subjects.Select(Subject => new KeyValuePair<string, string>(Subject.Subject, Subject.Value));

                HttpContent HttpContent = new FormUrlEncodedContent(KeyValues);
                try
                {
                    m_HttpClient.PostAsync(Url, HttpContent).Wait();
                }
                catch (Exception theException)
                {
                    Log.AppendLine(" - - - - -");

                    Log.AppendLine(theException.Message);

                    var Inner = theException.InnerException;

                    while (Inner != null)
                    {
                        Log.AppendLine(Inner.Message);

                        Inner = Inner.InnerException;
                    }
                    Log.AppendLine(" - - - - -");
                }
            }
        }

        internal bool SubscriptionDelete(string theSubscriptionGuid)
        {
            var SubscriptionsList = Subscriptions.ToList();

            Subscription SubscriptionSearch =  SubscriptionsList.FirstOrDefault(Subscription => Subscription.Guid == theSubscriptionGuid);

            if( SubscriptionSearch != null)
            {
                SubscriptionSearch.Event -= OnEvent;

                bool Result = SubscriptionsList.Remove(SubscriptionSearch);

                Subscriptions = SubscriptionsList.ToArray();

                return Result;
            }

            return false;
        }
    }
}
