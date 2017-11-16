using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientProxy : ChannelFactory<Interfejs>, Interfejs, IDisposable
    {
        public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
        }

        public void AddUser(string username, string password)
        {
            throw new NotImplementedException();
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
