using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PublisherWPF
{
    public class PublisherProxy : ChannelFactory<IPublish>, IPublish, IDisposable
    {
        IPublish factory;
        public PublisherProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void Publish(Topic topic)
        {
            try
            {
                factory.Publish(topic);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }
        }

        public void Dispose()
        {

        }
    }
}
