using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SubscriberWPF
{
    public class SysLogProxy : ChannelFactory<ISyslog>, ISyslog, IDisposable
    {
        ISyslog factory;

        public SysLogProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void EnterLog()
        {
            throw new NotImplementedException();
        }
    }
}
