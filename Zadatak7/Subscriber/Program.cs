using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:1000/SubscriberService";

            using (SubscriberProxy proxy = new SubscriberProxy(binding, address))
            {
                proxy.Subscribe("burek");
            }

            Console.ReadLine();
        }
    }
}
