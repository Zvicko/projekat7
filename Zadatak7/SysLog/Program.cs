using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SysLog
{
   public class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding bindingClient = new NetTcpBinding();
            string address1 = "net.tcp://localhost:8477/SysLog";

            ServiceHost host1 = new ServiceHost(typeof(SubService));
            host1.AddServiceEndpoint(typeof(ISyslog), bindingClient, address1);

            host1.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host1.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host1.Open();

            Console.WriteLine("SysLog service has been started.\n");
            Console.WriteLine("Press ENTER to stop the service...");

            Console.ReadLine();

            host1.Close();
        }
    }
}
