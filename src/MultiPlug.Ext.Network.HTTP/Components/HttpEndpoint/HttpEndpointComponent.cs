using System;
using System.Linq;
using System.Collections.Generic;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpEndpoint;

namespace MultiPlug.Ext.Network.HTTP.Components.HttpEndpoint
{
    public class HttpEndpointComponent : HttpEndpointProperties
    {
        internal event Action EventsUpdated;
        internal event Action UrlORVerbUpdated;

        private const string c_Group = "HttpEndpoint";

        public HttpEndpointComponent(string theGuid)
        {
            this.Guid = theGuid;

            RequestEvent = new Models.Exchange.Event
            {
                Guid = theGuid,
                Id = System.Guid.NewGuid().ToString(),
                Description = "HTTP Request",
                Subjects = new string[0],
                RequestKeys = new string[0],
                Group = c_Group
            };

            Url = string.Empty;
            Verb = VerbPost;
        }

        internal void UpdateProperties(HttpEndpointProperties theNewProperties)
        {
            bool FlagEventUpdated = false;
            bool FlagUrlORVerbUpdated = false;

            if (theNewProperties.Url != null)
            {
                if(Url != theNewProperties.Url)
                {
                    Url = theNewProperties.Url;
                    FlagUrlORVerbUpdated = true;
                }
            }

            if (theNewProperties.Verb != null)
            {
                if (Verb != theNewProperties.Verb)
                {
                    Verb = theNewProperties.Verb;
                    FlagUrlORVerbUpdated = true;
                }
            }

            if (theNewProperties.RequestEvent != null)
            {
                FlagEventUpdated = Event.Merge(RequestEvent, theNewProperties.RequestEvent);

                if(theNewProperties.RequestEvent.RequestKeys != null)
                {
                    RequestEvent.RequestKeys = theNewProperties.RequestEvent.RequestKeys;
                }

                IntegrityCheck(RequestEvent);
            }

            if (FlagUrlORVerbUpdated) { UrlORVerbUpdated?.Invoke(); }
            if (FlagEventUpdated) { EventsUpdated?.Invoke(); }
        }

        private static void IntegrityCheck(Models.Exchange.Event theEvent)
        {
            if(theEvent.Subjects.Length > theEvent.RequestKeys.Length)
            {
                // Expand the RequestKeys Array

                var RequestKeys = theEvent.RequestKeys;

                Array.Resize(ref RequestKeys, theEvent.Subjects.Length);

                for (int i = theEvent.RequestKeys.Length; i < theEvent.Subjects.Length; i++)
                {
                    RequestKeys[i] = string.Empty;
                }

                theEvent.RequestKeys = RequestKeys;
            }

            if (theEvent.Subjects.Length < theEvent.RequestKeys.Length)
            {
                // Contract the RequestKeys Array

                theEvent.RequestKeys = theEvent.RequestKeys.Take(theEvent.Subjects.Length).ToArray();
            }
        }

        internal void Process(IEnumerable<KeyValuePair<string, string>> theKeyValueData)
        {
            PayloadSubject[] PayloadSubjects = new PayloadSubject[RequestEvent.Subjects.Length];

            Dictionary<string, KeyValuePair<string, string>> Dictionary = theKeyValueData.ToDictionary( x => x.Key.ToLower());

            for ( int i = 0; i < RequestEvent.Subjects.Length; i++)
            {
                KeyValuePair<string, string> result;

                string SearchFor = string.Empty;

                if(string.IsNullOrEmpty(RequestEvent.RequestKeys[i]))
                {
                    SearchFor = RequestEvent.Subjects[i];
                }
                else
                {
                    SearchFor = RequestEvent.RequestKeys[i];
                }

                if (Dictionary.TryGetValue(SearchFor.ToLower(), out result))
                {
                    PayloadSubjects[i] = new PayloadSubject(RequestEvent.Subjects[i], result.Value);
                }
                else
                {
                    PayloadSubjects[i] = new PayloadSubject(RequestEvent.Subjects[i], string.Empty);
                }
            }

            RequestEvent.Invoke(new Payload(RequestEvent.Id, PayloadSubjects));
        }
    }
}
