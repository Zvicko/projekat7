using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Subscriber
{
    public class SubscriberProxy : ChannelFactory<ISubscribe>, ISubscribe, IDisposable
    {
        ISubscribe factory;

        public SubscriberProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void Subscribe(string topicName)
        {
            try
            {
                factory.Subscribe(topicName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public void Dispose()
        {

        }

       
        


        
    }
}

