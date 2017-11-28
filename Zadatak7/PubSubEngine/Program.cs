using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.IdentityModel;
using Common;
using Manager;

namespace PubSubEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address1 = "net.tcp://localhost:9999/PublisherService";
            string address2 = "net.tcp://localhost:1000/SubscriberService";

            // Common name sertifikata koji koristi server.
            string serverCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            PublisherService p = new PublisherService(); // da bi inicijalizovao listu

            ServiceHost host1 = new ServiceHost(typeof(PublisherService));
            ServiceHost host2 = new ServiceHost(typeof(SubscriberService));
            host1.AddServiceEndpoint(typeof(IPublish), binding, address1);
            host2.AddServiceEndpoint(typeof(ISubscribe), binding, address2);

            host1.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host1.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertificateValidator();
            host1.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            host2.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host2.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertificateValidator();
            host2.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            //serverCertCN = "Server";
            host1.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, serverCertCN);

            Logger logger = new Logger();

            try
            {
                host1.Open();
               // host2.Open();

                Console.WriteLine("PubSubEngine service has been started.\n");
                Console.WriteLine("Press ENTER to stop the service...");

                Console.ReadLine();
                PublisherService.ListTopic.ForEach(t => Console.WriteLine($"{t.NazivTopica}\n{t.Al.Izgenerisan}\n{t.Al.Poruka}\n{t.Al.Rizik}\n"));
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                Console.WriteLine("[StackTrace] {0}", e.StackTrace);
            }
            finally
            {
                host1.Close();
               // host2.Close();
            }
        }
    }
}
