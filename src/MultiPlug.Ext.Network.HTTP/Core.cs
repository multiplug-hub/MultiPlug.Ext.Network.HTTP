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
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint;

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

        internal Dictionary<string, HttpEndpointComponent> HttpEndpointsGets = new Dictionary<string, HttpEndpointComponent>();
        internal Dictionary<string, HttpEndpointComponent> HttpEndpointsPosts = new Dictionary<string, HttpEndpointComponent>();

        private void PopulateEndpointDictionaries()
        {
            HttpEndpointsGets = HttpEndpoints.Where(x => x.Verb == HttpEndpointProperties.VerbGet).ToDictionary(x => x.Url);
            HttpEndpointsPosts = HttpEndpoints.Where(x => x.Verb == HttpEndpointProperties.VerbPost).ToDictionary(x => x.Url);
        }

        internal void HttpClientAdd(string theVerb, string theUrl)
        {
            string NewHttpClientGuid = Guid.NewGuid().ToString().Substring(9, 4);

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

        internal void HttpEndpointAdd(string theUrl, string theVerb)
        {
            string EndpointUrl = theUrl.ToLower();
            EndpointUrl = EndpointUrl.Replace(" ", "-");

            if (HttpEndpointsPosts.ContainsKey(EndpointUrl) || HttpEndpointsGets.ContainsKey(EndpointUrl))
            {
                return;
            }

            string NewHttpEndpointGuid = Guid.NewGuid().ToString().Substring(9, 4);

            HttpEndpointComponent NewHttpEndpoint = new HttpEndpointComponent(NewHttpEndpointGuid);

            NewHttpEndpoint.EventsUpdated += OnEventsUpdated;

            NewHttpEndpoint.UpdateProperties(new HttpEndpointProperties { Guid = NewHttpEndpointGuid, Url = EndpointUrl, Verb = theVerb });

            List<HttpEndpointComponent> HttpEndpointsList = HttpEndpoints.ToList();
            HttpEndpointsList.Add(NewHttpEndpoint);
            HttpEndpoints = HttpEndpointsList.ToArray();
            PopulateEndpointDictionaries();
            NewHttpEndpoint.UrlORVerbUpdated += OnEndpointUrlORVerbUpdated;

            AggregateEvents();
        }

        private void OnEndpointUrlORVerbUpdated()
        {
            PopulateEndpointDictionaries();
        }

        internal void Load(LoadRoot theConfiguration)
        {
            bool Updates = false;

            if (theConfiguration.HttpClients != null)
            {
                List<HttpClientComponent> HttpClientsList = HttpClients.ToList();

                foreach (var HttpClientProperties in theConfiguration.HttpClients)
                {
                    if (string.IsNullOrEmpty(HttpClientProperties.Guid))
                    {
                        continue;
                    }

                    var HttpClientSearch = HttpClients.FirstOrDefault(Lane => Lane.Guid == HttpClientProperties.Guid);

                    if (HttpClientSearch == null)
                    {
                        if (HttpEndpointsPosts.ContainsKey(HttpClientProperties.Url) || HttpEndpointsGets.ContainsKey(HttpClientProperties.Url))
                        {
                            continue;
                        }

                        HttpClientSearch = new HttpClientComponent(HttpClientProperties.Guid);
                        HttpClientSearch.UpdateProperties(HttpClientProperties);

                        // Add Event Handlers last as AggregateSubscriptions/Events() is called below.
                        HttpClientSearch.EventsUpdated += OnEventsUpdated;
                        HttpClientSearch.SubscriptionsUpdated += OnSubscriptionsUpdated;
                        HttpClientsList.Add(HttpClientSearch);
                    }
                    else
                    {
                        HttpClientSearch.UpdateProperties(HttpClientProperties);
                    }
                }

                HttpClients = HttpClientsList.ToArray();

                Updates = true;
            }

            if (theConfiguration.HttpEndpoints != null)
            {
                List<HttpEndpointComponent> HttpEndpointsList = HttpEndpoints.ToList();

                foreach (var HttpEndpointProperties in theConfiguration.HttpEndpoints)
                {
                    if (string.IsNullOrEmpty(HttpEndpointProperties.Guid))
                    {
                        continue;
                    }

                    var HttpEndpointSearch = HttpEndpoints.FirstOrDefault(Lane => Lane.Guid == HttpEndpointProperties.Guid);

                    if (HttpEndpointSearch == null)
                    {
                        HttpEndpointSearch = new HttpEndpointComponent(HttpEndpointProperties.Guid);
                        HttpEndpointSearch.UpdateProperties(HttpEndpointProperties);

                        // Add Event Handlers last as AggregateSubscriptions/Events() is called below.
                        HttpEndpointSearch.EventsUpdated += OnEventsUpdated;
                        HttpEndpointSearch.UrlORVerbUpdated += OnEndpointUrlORVerbUpdated;

                        HttpEndpointsList.Add(HttpEndpointSearch);
                    }
                    else
                    {
                        HttpEndpointSearch.UpdateProperties(HttpEndpointProperties);
                    }
                }

                HttpEndpoints = HttpEndpointsList.ToArray();
                PopulateEndpointDictionaries();

                Updates = true;
            }

            if(Updates)
            {
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

        internal bool HttpEndpointDelete(string theGuid)
        {
            HttpEndpointComponent HttpEndpointSearch = Core.Instance.HttpEndpoints.FirstOrDefault(Lane => Lane.Guid == theGuid);

            if (HttpEndpointSearch == null)
            {
                return false;
            }

            List<HttpEndpointComponent> HttpEndpointsList = HttpEndpoints.ToList();
            HttpEndpointsList.Remove(HttpEndpointSearch);
            HttpEndpoints = HttpEndpointsList.ToArray();
            PopulateEndpointDictionaries();

            HttpEndpointSearch.EventsUpdated -= OnEventsUpdated;
            HttpEndpointSearch.UrlORVerbUpdated -= OnEndpointUrlORVerbUpdated;

            AggregateEvents();

            return true;
        }

        internal bool HttpClientSubscriptionDelete(string theGuid, string theSubscriptionGuid)
        {
            var HttpClientSearch = HttpClients.FirstOrDefault(Lane => Lane.Guid == theGuid);

            if (HttpClientSearch == null)
            {
                return false;
            }

            return HttpClientSearch.SubscriptionDelete(theSubscriptionGuid);
        }

        internal bool HttpClientRenameDelete(string theGuid, string renamevalue)
        {
            var HttpClientSearch = HttpClients.FirstOrDefault(Lane => Lane.Guid == theGuid);

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

            foreach (var Lane in HttpEndpoints)
            {
                EventsList.Add(Lane.RequestEvent);
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
