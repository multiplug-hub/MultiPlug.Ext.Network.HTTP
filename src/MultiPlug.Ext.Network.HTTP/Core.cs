using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MultiPlug.Base;
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Components.HttpEndpoint;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Load;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Network.HTTP
{
    internal class Core : MultiPlugBase
    {
        private static Core m_Instance = null;

        internal event Action SubscriptionsUpdated;
        internal event Action EventsUpdated;

        internal Subscription[] Subscriptions { get; private set; } = new Subscription[0];
        internal Event[] Events { get; private set; } = new Event[0];

        public static Core Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Core();
                }
                return m_Instance;
            }
        }

        [DataMember]
        public HttpEndpointComponent[] HttpEndpoints { get; set; } = new HttpEndpointComponent[0];
        [DataMember]
        public HttpClientComponent[] HttpClients { get; set; } = new HttpClientComponent[0];

        internal void HttpClientAdd(string theVerb, string theUrl)
        {
            string NewHttpClientGuid = Guid.NewGuid().ToString();

            HttpClientComponent NewHttpClient = new HttpClientComponent(NewHttpClientGuid);

            NewHttpClient.EventsUpdated += OnEventsUpdated;
            NewHttpClient.SubscriptionsUpdated += OnSubscriptionsUpdated;

            NewHttpClient.UpdateProperties(new HttpClientProperties { Guid = NewHttpClientGuid, Verb = theVerb, Url = theUrl });

            List<HttpClientComponent> HttpClientsList = HttpClients.ToList();
            HttpClientsList.Add(NewHttpClient);
            HttpClients = HttpClientsList.ToArray();
            AggregateSubscriptions();
            AggregateEvents();

        }

        internal void Load(LoadRoot theConfiguration)
        {
            if (theConfiguration.HttpClients != null)
            {
                List<HttpClientComponent> HttpClientsList = HttpClients.ToList();

                foreach (var HttpClientProperties in theConfiguration.HttpClients)
                {
                    if (string.IsNullOrEmpty(HttpClientProperties.Guid))
                    {
                        continue;
                    }

                    var HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == HttpClientProperties.Guid);

                    if (HttpClientSearch == null)
                    {
                        HttpClientSearch = new HttpClientComponent(HttpClientProperties.Guid);

                        HttpClientSearch.EventsUpdated += OnEventsUpdated;
                        HttpClientSearch.SubscriptionsUpdated += OnSubscriptionsUpdated;
                        HttpClientsList.Add(HttpClientSearch);
                    }

                    HttpClientSearch.UpdateProperties(HttpClientProperties);
                }

                HttpClients = HttpClientsList.ToArray();
                AggregateSubscriptions();
                AggregateEvents();
            }
        }

        internal bool HttpClientDelete(string theGuid)
        {
            var HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theGuid);

            if (HttpClientSearch == null)
            {
                return false;
            }

            List<HttpClientComponent> HttpClientsList = HttpClients.ToList();
            HttpClientsList.Remove(HttpClientSearch);
            HttpClients = HttpClientsList.ToArray();

            HttpClientSearch.EventsUpdated -= OnEventsUpdated;
            HttpClientSearch.SubscriptionsUpdated -= OnSubscriptionsUpdated;

            AggregateSubscriptions();
            AggregateEvents();

            return true;
        }

        internal bool HttpClientSubscriptionDelete(string theGuid, string theSubscriptionGuid)
        {
            var HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theGuid);

            if (HttpClientSearch == null)
            {
                return false;
            }

            return HttpClientSearch.SubscriptionDelete(theSubscriptionGuid);
        }

        internal bool HttpClientRenameDelete(string theGuid, string renamevalue)
        {
            var HttpClientSearch = Core.Instance.HttpClients.FirstOrDefault(Lane => Lane.Guid == theGuid);

            if (HttpClientSearch == null)
            {
                return false;
            }

            return HttpClientSearch.RenameDelete(renamevalue);
        }

        private void OnSubscriptionsUpdated()
        {
            AggregateSubscriptions();
        }

        private void OnEventsUpdated()
        {
            AggregateEvents();
        }

        private void AggregateEvents()
        {
            var EventsList = new List<Event>();

            foreach (var Lane in HttpClients)
            {
                EventsList.Add(Lane.ResponseEvent);
            }

            Events = EventsList.ToArray();
            EventsUpdated?.Invoke();
        }

        private void AggregateSubscriptions()
        {
            var SubscriptionsList = new List<Subscription>();

            foreach (var Lane in HttpClients)
            {
                SubscriptionsList.AddRange(Lane.Subscriptions);
            }

            Subscriptions = SubscriptionsList.ToArray();
            SubscriptionsUpdated?.Invoke();
        }
    }
}
