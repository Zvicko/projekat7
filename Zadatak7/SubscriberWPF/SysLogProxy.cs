using Common;
using System;
using System.ServiceModel;

namespace SubscriberWPF
{
    public class SysLogProxy : ChannelFactory<ISyslog>, ISyslog, IDisposable
    {
        ISyslog factory;
        public static string address1 = "net.tcp://localhost:8477/SysLog";

        public SysLogProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void EnterLog(SyslogMessage message)
        {
            try
            {
                factory.EnterLog(message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
