
using MultiPlug.Ext.Network.HTTP.Components.HttpClient;
using System.Runtime.Serialization;

namespace MultiPlug.Ext.Network.HTTP.Models.Exchange
{
    public class Subscription : Base.Exchange.Subscription
    {
        internal HttpClientEventHandler EventHandler { get; set; }


        [DataMember]
        public SubjectRename[] SubjectRenames { get; set; } = new SubjectRename[0];

        public static bool Merge(Subscription theSubscriptionInto, Subscription theSubscriptionFrom)
        {
            if (theSubscriptionInto.Subjects.Length == theSubscriptionFrom.SubjectRenames.Length)
            {
                theSubscriptionInto.SubjectRenames = theSubscriptionFrom.SubjectRenames;
            }
            else if(theSubscriptionInto.Subjects.Length < theSubscriptionFrom.SubjectRenames.Length)
            {
                SubjectRename[] SubjectRenames = new SubjectRename[theSubscriptionInto.Subjects.Length];

                for (int i = 0; i < theSubscriptionInto.Subjects.Length; i++)
                {
                    SubjectRenames[i] = theSubscriptionFrom.SubjectRenames[i];
                }

                theSubscriptionInto.SubjectRenames = SubjectRenames;
            }
            else if(theSubscriptionInto.Subjects.Length > theSubscriptionFrom.SubjectRenames.Length)
            {
                SubjectRename[] SubjectRenames = new SubjectRename[theSubscriptionInto.Subjects.Length];

                int i = 0;
                for (; i < theSubscriptionFrom.SubjectRenames.Length; i++)
                {
                    SubjectRenames[i] = theSubscriptionFrom.SubjectRenames[i];
                }

                for (; i < theSubscriptionInto.Subjects.Length; i++)
                {
                    SubjectRenames[i] = new SubjectRename
                    {
                        Rename = string.Empty,
                        AddToQuery = true,
                        AddToBody = false
                    };
                }

                theSubscriptionInto.SubjectRenames = SubjectRenames;
            }

            return true;
        }

        public static bool Sync(Subscription theSubscription)
        {
            if( theSubscription.Subjects.Length != theSubscription.SubjectRenames.Length)
            {
                theSubscription.SubjectRenames = new SubjectRename[theSubscription.Subjects.Length];

                for(int i = 0; i < theSubscription.Subjects.Length; i++)
                {
                    theSubscription.SubjectRenames[i] = new SubjectRename
                    {
                        Rename = string.Empty,
                        AddToQuery = true,
                        AddToBody = false
                    };
                }
            }

            return true;
        }
    }
}
