using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace PubSubEngine
{
    public class PublisherService : IPublish
    {
       public static List<Topic> ListTopic
        {
            get;
            set;
        }

        public PublisherService()
        {
            ListTopic = new List<Topic>();
        }

        public void Publish(Topic topic)
        {
            ListTopic.Add(topic);

            //ListTopic.ForEach(t => Console.WriteLine($"{t.NazivTopica}\n{t.Al.Izgenerisan}\n{t.Al.Poruka}\n{t.Al.Rizik}\n"));
        }

        public bool ShutDown(string pubName, bool flag)
        {
            lock (SubscriberService._locck)
            {
                List<Alarm> alarms = new List<Alarm>();
                foreach (var item in SubscriberService.mainDic.Keys)
                {
                    if (item.ImePub.Equals(pubName))
                    {
                        alarms.Add(item);
                    }
                }
                foreach (var item in alarms)
                {
                    SubscriberService.mainDic.Remove(item);
                }
                
            }
            foreach (var item in ListTopic)
            {
                if (item.NazivPub == pubName)
                {
                    ListTopic.Remove(item);
                    break;
                }
            }
            ListTopic.RemoveAll(x => x.NazivPub == pubName);
            
            
            return true;

            
        }
    }
}
