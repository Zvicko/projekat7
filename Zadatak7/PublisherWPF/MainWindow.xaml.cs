using Common;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using Manager;
using System.Security.Cryptography.X509Certificates;

namespace PublisherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static string pubname = "";
        public static int timeToSend;
        public static X509Certificate2 pubCert = null;
       
        Topic topic = new Topic();
        PublisherProxy proxy = null;
        Thread thread = null;

        public event PropertyChangedEventHandler PropertyChanged;
      
        public MainWindow()
        {
            InitializeComponent();

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:9999/PublisherService";
            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("Server");
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address), identity);
            pubCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "Publisher");

            proxy = new PublisherProxy(binding, endpointAddress);

            thread = new Thread(new ThreadStart(this.worker_DoWork));
            thread.IsBackground = true;
            thread.Start();
        }
      

        private void dugme_Click(object sender, RoutedEventArgs e)
        {
            labelaUspesno.Content = "";
            topic.Al = new Alarm();
            topic.NazivTopica = textBoxTopic.Text.Trim();
            int temp;
            if (Int32.TryParse(textBoxRizik.Text.Trim(), out temp))
            {
                if (temp < 1 || temp > 100)
                {
                    labelaTopicRizik.Content = "Rizik nije u opsegu od 1 do 100!";
                }
                else
                {
                    string poruka = proveraRizika(temp); 

                    ResourceManager rm = new ResourceManager("Poruke", Assembly.GetExecutingAssembly());

                    topic.Al.Rizik = temp;
                    topic.Al.Izgenerisan = DateTime.Now;
                    topic.Al.ImePub = MainWindow.pubname;

                    topic.Al.Poruka = Poruke.ResourceManager.GetString(poruka);
                    topic.NazivPub = MainWindow.pubname;
                    string potpis = "PublisherSign";
                    X509Certificate2 signCert = Manager.CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, potpis);
                    byte[] sign = DigitalSignature.Create(topic.Al.Poruka, "SHA1", signCert);
                    //proxy.Publish(topic);
                    proxy.Publish(topic, sign); // Privatni kljuc ovde jos uvek postoji.

                    textBoxRizik.Text = "";
                    textBoxTopic.Text = "";
                    labelaUspesno.Content = "Topic je uspesno dodat!";

                }
            }
            else
            {
                labelaTopicRizik.Content = "Uneta je nebrojevna vrednost za rizik!";
            }
        }

        private string proveraRizika(int temp)
        {
            if (temp < 11)
                return "nemaRizika";
            else if (temp >= 11 && temp < 31)
                return "niskiRizik";
            else if (temp >= 31 && temp < 71)
                return "srednjiRizik";
            else if (temp >= 71)
                return "visokiRizik";
            else
                return null;
        }

        private void worker_DoWork()
        {
            while (true)
            {
                

                Thread.Sleep(6000);
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void closeClick(object sender, CancelEventArgs e)
        {
            bool result = proxy.ShutDown(MainWindow.pubname,true);
            if (result)
            {
                return;
            }

        }
    }
}
