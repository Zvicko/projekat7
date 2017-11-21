using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SubscriberWPF
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
        public List<Topic> Read()
        {
            List<Topic> list = new List<Topic>();
            try
            {
                list = factory.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            //list.ForEach(t => Console.WriteLine($"{t.NazivTopica}\n{t.Al.Izgenerisan}\n{t.Al.Poruka}\n{t.Al.Rizik}"));
            
           /* foreach (Topic t in list)
            {
                if(!MainWindow.al.Contains(t.Al))
                    MainWindow.al.Add(t.Al);
            }
            */
            return list;

        }
        public void Dispose()
        {

        }
    }
}
