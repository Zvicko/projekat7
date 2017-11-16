using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSubEngine
{
    public class PubSubService : IPublish, ISubscribe
    {
        public void Publish(Alarm alarm, string topicName)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string topicName)
        {
            throw new NotImplementedException();
        }
    }
}
