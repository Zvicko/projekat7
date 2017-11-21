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
        public List<Topic> Read()
        {
            return PublisherService.ListTopic;

        }
        public void Subscribe(string topicName)
        {
            bool exit = false;
            
            Console.WriteLine("\nPokrenut Subscriber. Topic name: {0}", topicName);
            
      //  Console.WriteLine("\nSubscriber se odjavio sa topica.");
        }
    }
}
