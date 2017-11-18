using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace PubSubEngine
{
    public class SubscriberService : ISubscribe
    {
        public List<Topic> Read()
        {
            return PublisherService.ListTopic;

        }
        public void Subscribe(string topicName)
        {
            Console.WriteLine("\nPokrenut Subscriber. Topic name: {0}", topicName);
        }
    }
}
