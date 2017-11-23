using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Documents;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Manager;

namespace PubSubEngine
{
    public class SubscriberService : ISubscribe
    {
        public static Dictionary<Topic, List<string>> mainDic = new Dictionary<Topic,  List<string>>();
        //private static int itemCounter = 0;

        public List<Topic> Read()
        {
            return PublisherService.ListTopic;
        }

        /*public List<Topic> Read(X509Certificate2 certificate)
        {
            List<Topic> topics = PublisherService.ListTopic;

            Topic topic = topics[itemCounter];

            bool verified = DigitalSignature.Verify(topic.Al.Poruka, "SHA1", topic.Potpis, certificate);

            return topics;
        }*/

        public void Subscribe(Topic topic,string imeSub)
        {
            //bool exit = false;
           if (!mainDic.ContainsKey(topic))
            {
                List<string> subs = new List<string>();
                subs.Add(imeSub);
                mainDic.Add(topic, subs);
            }
            else
            {
                mainDic[topic].Add(imeSub);
            }        
        }
    }
}
