using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Manager;
using System.Security.Principal;
using System.IdentityModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SubscriberWPF
{
    public class SubscriberProxy : ChannelFactory<ISubscribe>, ISubscribe, IDisposable
    {
        ISubscribe factory;

        public SubscriberProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertificateValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);

            factory = this.CreateChannel();
        }

        public bool Subscribe(Alarm alarm,string imeSUb)
        {
            try
            {
                factory.Subscribe(alarm, imeSUb);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return false;
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

        public List<Alarm> SubscribedAlarms(string imeSub)
        {
            List<Alarm> list = new List<Alarm>();
            try
            {
                list = factory.SubscribedAlarms(imeSub);
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return list;
            }
        }
    }
}
