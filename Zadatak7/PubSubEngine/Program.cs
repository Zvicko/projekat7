using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using Common;

namespace PubSubEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address1 = "net.tcp://localhost:9999/PublisherService";
            string address2 = "net.tcp://localhost:1000/SubscriberService";

            PublisherService p = new PublisherService(); // da bi inicijalizovao listu

            ServiceHost host1 = new ServiceHost(typeof(PublisherService));
            ServiceHost host2 = new ServiceHost(typeof(SubscriberService));
            host1.AddServiceEndpoint(typeof(IPublish), binding, address1);
            host2.AddServiceEndpoint(typeof(ISubscribe), binding, address2);

            host1.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host1.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            host2.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host2.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host1.Open();
            host2.Open();

            Console.WriteLine("PubSubEngine service has been started.\n");
            Console.WriteLine("Press ENTER to stop the service...");

            Console.ReadLine();
            host1.Close();
            host2.Close();
        }
    }
}
