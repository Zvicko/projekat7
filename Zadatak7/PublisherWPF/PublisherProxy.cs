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

namespace PublisherWPF
{
   
    public class PublisherProxy : ChannelFactory<IPublish>, IPublish, IDisposable
    {
        IPublish factory;
        public PublisherProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            string clientCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertificateValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, clientCertCN);

            factory = this.CreateChannel();
        }

        public void Publish(Topic topic)
        {
            try
            {
                factory.Publish(topic);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
            }
        }

        public void Dispose()
        {

        }

        public bool ShutDown(string pubName, bool flag)
        {
            try
            {
                factory.ShutDown(pubName,flag);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return false;

            }
        }
    }
}
