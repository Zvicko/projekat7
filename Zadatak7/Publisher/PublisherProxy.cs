using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
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




        /* Interfejs factory;
         public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
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

         }

         public void RemoveUser(string username)
         {
             throw new NotImplementedException();
         }


         public void Subscribe(string topicName)
         {
             throw new NotImplementedException();
         }*/
    }
}
