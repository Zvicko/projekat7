using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Documents;
using System.Runtime.CompilerServices;

namespace PubSubEngine
{
    public class SubscriberService : ISubscribe
    {
        public static Dictionary<Topic, List<string>> mainDic = new Dictionary<Topic,  List<string>>();

        public List<Topic> Read()
        {
            return PublisherService.ListTopic;

        }

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
