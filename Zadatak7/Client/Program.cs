using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/PublisherService";
            Topic topic = new Topic();
            topic.Al = new Alarm();
            
            using (PublisherProxy proxy = new PublisherProxy(binding, address))
            {
                
                Console.WriteLine("Unesite naziv topic-a:");
                topic.NazivTopica = Console.ReadLine();
                topic.Al.Poruka = "poruka";
                topic.Al.Rizik = 5;
                topic.Al.Izgenerisan = DateTime.Now;
                proxy.Publish(topic);
               
            }

            Console.ReadLine();
        }
    }
}
