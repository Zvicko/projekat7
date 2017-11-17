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
        }
    }
}
