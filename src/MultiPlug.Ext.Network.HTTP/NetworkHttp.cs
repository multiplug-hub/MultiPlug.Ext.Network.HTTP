using System.Net;
using MultiPlug.Extension.Core;
using MultiPlug.Extension.Core.Http;
using MultiPlug.Ext.Network.HTTP.Controllers;
using MultiPlug.Ext.Network.HTTP.Properties;
using MultiPlug.Ext.Network.HTTP.Models.Load;
using MultiPlug.Base.Exchange;

namespace MultiPlug.Ext.Network.HTTP
{
    public class NetworkHttp : MultiPlugExtension
    {
        private LoadRoot m_LoadModel = null;

        public NetworkHttp()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

            Core.Instance.SubscriptionsUpdated += OnSubscriptionsUpdated;
            Core.Instance.EventsUpdated += OnEventsUpdated;
        }

        public override RazorTemplate[] RazorTemplates
        {
            get
            {
                return new RazorTemplate[]
                {
                    new RazorTemplate( Templates.SettingsHttpClientNavigation, Resources.SettingsHttpClientNavigation),
                    new RazorTemplate( Templates.SettingsHome, Resources.SettingsHome ),
                    new RazorTemplate( Templates.SettingsAbout, Resources.SettingsAbout ),
                    new RazorTemplate( Templates.SettingsHttpClient, Resources.SettingsHttpClient ),
                    new RazorTemplate( Templates.SettingsHttpClientSubscription, Resources.SettingsHttpClientSubscription ),
                    new RazorTemplate( Templates.SettingsHttpClientAuthorisation, Resources.SettingsHttpClientAuthorisation ),
                    new RazorTemplate( Templates.SettingsHttpClientHeaders, Resources.SettingsHttpClientHeaders ),
                    new RazorTemplate( Templates.SettingsHttpClientBody, Resources.SettingsHttpClientBody ),
                    new RazorTemplate( Templates.SettingsHttpClientQuery, Resources.SettingsHttpClientQuery ),
                    new RazorTemplate( Templates.SettingsHttpClientResponse, Resources.SettingsHttpClientResponse ),
                    new RazorTemplate( Templates.SettingsHttpClientSettings, Resources.SettingsHttpClientSettings ),

                    new RazorTemplate( Templates.SettingsHttpEndpointNavigation, Resources.SettingsHttpEndpointNavigation ),
                    new RazorTemplate( Templates.SettingsHttpEndpoint, Resources.SettingsHttpEndpoint ),
                    new RazorTemplate( Templates.SettingsHttpEndpointAuthorisation, Resources.SettingsHttpEndpointAuthorisation )
                };
            }
        }

        public void Load(LoadRoot theConfiguration)
        {
            m_LoadModel = theConfiguration;

            Core.Instance.Load(theConfiguration);
        }

        public override void Initialise()
        {
            if( m_LoadModel != null)
            {
                Core.Instance.Load(m_LoadModel);
                m_LoadModel = null;
            }
        }

        public override object Save()
        {
            return Core.Instance;
        }

        public override Subscription[] Subscriptions
        {
            get
            {
                return Core.Instance.Subscriptions;
            }
        }
        public override Event[] Events
        {
            get
            {
                return Core.Instance.Events;
            }
        }

        private void OnSubscriptionsUpdated()
        {
            MultiPlugActions.Extension.Updates.Subscriptions();
        }

        private void OnEventsUpdated()
        {
            MultiPlugActions.Extension.Updates.Events();
        }
    }
}
