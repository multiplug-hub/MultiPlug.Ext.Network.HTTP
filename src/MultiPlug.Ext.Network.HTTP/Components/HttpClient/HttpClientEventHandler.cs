using System;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MultiPlug.Base.Exchange;
using MultiPlug.Ext.Network.HTTP.Models.Components.HttpClient;
using MultiPlug.Ext.Network.HTTP.Models.Exchange;

namespace MultiPlug.Ext.Network.HTTP.Components.HttpClient
{
    internal class HttpClientEventHandler
    {
        readonly Models.Exchange.Subscription m_Subscription;
        readonly HttpClientProperties m_Properties;

        private readonly System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();

        internal HttpClientEventHandler(HttpClientProperties theProperties, System.Net.Http.HttpClient theHttpClient, Models.Exchange.Subscription theSubscription)
        {
            m_Properties = theProperties;
            m_HttpClient = theHttpClient;
            m_Subscription = theSubscription;

            m_Subscription.Event += OnEvent;
        }

        private string Rename(string theOriginalValue)
        {
            if (m_Properties.SubjectValueRenames.Length > 0)
            {
                SubjectValueRename Search = m_Properties.SubjectValueRenames.FirstOrDefault(Rename => Rename.Value == theOriginalValue);

                return (Search == null) ? theOriginalValue : Search.Rename;
            }
            else
            {
                return theOriginalValue;
            }
        }

        private string Rename(SubjectRename theRename, string theOrginalValue)
        {
            if (!string.IsNullOrEmpty(theRename.Rename))
            {
                return theRename.Rename;
            }
            else
            {
                return theOrginalValue;
            }
        }

        private string BuildQueryString(string[] theAddedValues)
        {
            var AsArray = m_Properties.QueryParams.Select(Subject => string.Format("{0}={1}", HttpUtility.UrlEncode(Subject.Key), HttpUtility.UrlEncode(Subject.Value))).ToList();

            AsArray.AddRange(theAddedValues);

            string QueryString = "";

            if (AsArray.Count() > 0)
            {
                QueryString = "?" + string.Join("&", AsArray);
            }

            return QueryString;
        }

        private void ProcessResponse(Task<HttpResponseMessage> theResponse)
        {
            theResponse.Wait();
            Task<string> Content = theResponse.Result.Content.ReadAsStringAsync();
            Content.Wait();

            m_Properties.ResponseEvent.Invoke(new Payload(m_Properties.ResponseEvent.Id, new PayloadSubject[]
            {
                new PayloadSubject(m_Properties.ResponseEvent.Subjects[0], Content.Result ),
                new PayloadSubject(m_Properties.ResponseEvent.Subjects[1], Content.Result.ToString() ),
                new PayloadSubject(m_Properties.ResponseEvent.Subjects[2], theResponse.Result.Headers.ToString() ),
            }));
        }

        private void OnEvent(SubscriptionEvent theEvent)
        {
            var SubjectsAsQueryString = new List<string>();
            var BodyKeyValues = m_Properties.BodyParams.Select(Subject => new KeyValuePair<string, string>(HttpUtility.UrlEncode(Subject.Key), HttpUtility.UrlEncode(Subject.Value))).ToList();

            for ( int i = 0; i < theEvent.PayloadSubjects.Length; i++)
            {
                if( i < m_Subscription.SubjectRenames.Length && m_Subscription.SubjectRenames[i].AddToQuery)
                {
                    SubjectsAsQueryString.Add(string.Format("{0}={1}",
                        HttpUtility.UrlEncode(Rename(m_Subscription.SubjectRenames[i], theEvent.PayloadSubjects[i].Subject)),
                        HttpUtility.UrlEncode(Rename(theEvent.PayloadSubjects[i].Value))
                    ));              
                }

                if (i < m_Subscription.SubjectRenames.Length &&  m_Subscription.SubjectRenames[i].AddToBody)
                {
                    BodyKeyValues.Add(new KeyValuePair<string, string>(HttpUtility.UrlEncode(Rename(m_Subscription.SubjectRenames[i], theEvent.PayloadSubjects[i].Subject)), HttpUtility.UrlEncode(Rename(theEvent.PayloadSubjects[i].Value))));
                }
            }

            HttpContent HttpContent = new FormUrlEncodedContent(BodyKeyValues);

            foreach (var Header in m_Properties.Headers)
            {
                HttpContent.Headers.Add(Header.Key, Header.Value);
            }

            if (m_Properties.Verb == HttpClientProperties.VerbGet)
            {
                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, m_Properties.Url + BuildQueryString(SubjectsAsQueryString.ToArray())))
                    {
                        request.Content = HttpContent;

                        ProcessResponse(m_HttpClient.SendAsync(request));
                    }
                }
                catch (Exception theException)
                {
                    m_Properties.Log.AppendLine(" - - - - -");
                    m_Properties.Log.AppendLine(theException.Message);

                    var Inner = theException.InnerException;

                    while (Inner != null)
                    {
                        m_Properties.Log.AppendLine(Inner.Message);

                        Inner = Inner.InnerException;
                    }
                    m_Properties.Log.AppendLine(" - - - - -");
                }
            }
            else //Post or Put
            {
                try
                {
                    Task<HttpResponseMessage> Response;

                    if (m_Properties.Verb == HttpClientProperties.VerbPost)
                    {
                        Response = m_HttpClient.PostAsync(m_Properties.Url + BuildQueryString(SubjectsAsQueryString.ToArray()), HttpContent);
                    }
                    else // Put
                    {
                        Response = m_HttpClient.PutAsync(m_Properties.Url + BuildQueryString(SubjectsAsQueryString.ToArray()), HttpContent);
                    }

                    ProcessResponse(Response);
                }
                catch (Exception theException)
                {
                    m_Properties.Log.AppendLine(" - - - - -");

                    m_Properties.Log.AppendLine(theException.Message);

                    var Inner = theException.InnerException;

                    while (Inner != null)
                    {
                        m_Properties.Log.AppendLine(Inner.Message);

                        Inner = Inner.InnerException;
                    }
                    m_Properties.Log.AppendLine(" - - - - -");
                }
            }
        }

        internal void Remove()
        {
            m_Subscription.Event -= OnEvent;
        }
    }
}
