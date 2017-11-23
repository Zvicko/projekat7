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

        public static Dictionary<Alarm, List<string>> mainDic = new Dictionary<Alarm, List<string>>();
        public static object _locck = new object();

        private static int itemCounter = 0;


        public List<Topic> Read()
        {
            return PublisherService.ListTopic;
        }



        public List<Topic> Read(X509Certificate2 certificate)
        {
            List<Topic> topics = PublisherService.ListTopic;

            Topic topic = topics[itemCounter];

            bool verified = DigitalSignature.Verify(topic.Al.Poruka, "SHA1", topic.Potpis, certificate);

            return topics;
        }

        public bool Subscribe(Alarm alarm, string imeSub)

        {
            //bool exit = false;

            foreach (var item in mainDic.Keys)
            {
                if (item.Izgenerisan == alarm.Izgenerisan)
                {
                    if (!mainDic[item].Contains(imeSub))
                    {
                        lock (this)
                        {
                            mainDic[item].Add(imeSub);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            List<string> subs = new List<string>();
            lock (_locck)
            {
                subs.Add(imeSub);
                mainDic.Add(alarm, subs);
            }
            return true;
        }

        public List<Alarm> SubscribedAlarms(string imeSub)
        {
            List<Alarm> returnList = new List<Alarm>();
            lock (_locck)
            {

                foreach (var item in mainDic.Keys)
                {
                    if (mainDic[item].Contains(imeSub))
                    {
                        returnList.Add(item);
                    }
                }
            }
            return returnList;

        }

    }
}

