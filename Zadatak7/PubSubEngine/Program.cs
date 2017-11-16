using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PubSubEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            //string address = "net.tcp://localhost:9999/SecurityService";

            //ServiceHost host = new ServiceHost(typeof(SecurityService));
            //host.AddServiceEndpoint(typeof(ISecurityService), binding, address);

            //host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            //host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            //host.Open();

            Console.WriteLine("PubSubEngine service has been started.\n");
            Console.WriteLine("Press ENTER to stop the service...");

            Console.ReadLine();
            //host.Close();
        }
    }
}
