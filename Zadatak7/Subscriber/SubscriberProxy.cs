using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Subscriber
{
    public class SubscriberProxy : ChannelFactory<Interfejs>, Interfejs, IDisposable
    {
        Interfejs factory;

        public SubscriberProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
        }

        public void AddUser(string username, string password)
        {
            try
            {
                factory.AddUser(username, password);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        public void Dispose()
        {

        }

        public void Publish(Alarm alarm, string topicName)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(string username)
        {
            throw new NotImplementedException();
        }


        public void Subscribe(string topicName)
        {
            throw new NotImplementedException();
        }
    }
}

