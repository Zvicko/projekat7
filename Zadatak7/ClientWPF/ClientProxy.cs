using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF
{
    public class ClientProxy : ChannelFactory<IClient>, IClient, IDisposable
    {
        IClient factory;

        public ClientProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void DoAlarm(Alarm a)
        {
            return;
        }

        public List<Alarm> ShowAllAlarms()
        {
            List<Alarm> ls = new List<Alarm>();
            try
            {
                ls = factory.ShowAllAlarms();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return ls;
        }
    }
}
