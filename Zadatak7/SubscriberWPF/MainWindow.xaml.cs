using Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Windows.Input;
using System;
using System.Security.Cryptography.X509Certificates;
using Manager;

namespace SubscriberWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public X509Certificate2 subCert;

        public static ObservableCollection<Alarm> al
        {
            get;
            set;
        }

        public static ObservableCollection<Alarm> Subs
        {
            get;
            set;
        }

        public static ObservableCollection<Alarm> pretplaceni
        {
            get;
            set;
        }


        public static List<Topic> tempTop
        {
            get;
            set;
        }

        private string imeSub = "";
        private string portnum;

        public string ImeSub
        {
            get { return imeSub; }
            set
            {
                imeSub = value;
                OnPropertyChanged("ImeSub");
            }
        }
        public string PortNum
        {
            get { return portnum; }
            set
            {
                portnum = value;
                OnPropertyChanged("PortNUm");
            }
        }

        public enum isStart
        {
            Yes,
            No
        }
        private isStart isStartProp = isStart.Yes;

        private ICommand loginCommandSub;

        public isStart StartUp
        {
            get { return isStartProp; }
            set
            {
                isStartProp = value;
                OnPropertyChanged("StartUp");
            }
        }
        public ICommand StartCommandClient
        {
            get
            {
                return loginCommandSub ?? (loginCommandSub = new RelayCommand((param) => this.startComm()));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private static SubscriberProxy proxy = null; // mozda ne mora da bude static
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public static string SubName;
        public static int Port;
        Thread thread = null;
        Thread threadSubscribed = null;
        string s = string.Empty;
        //static int pom = 0;
        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
            StartUp = isStart.Yes;
            object _lock = new object();
            object _lock1 = new object();
            object _lockSub = new object();
            al = new ObservableCollection<Alarm>();
            pretplaceni = new ObservableCollection<Alarm>();
            Subs = new ObservableCollection<Alarm>();
            BindingOperations.EnableCollectionSynchronization(al, _lock);
            BindingOperations.EnableCollectionSynchronization(pretplaceni, _lock1);
            BindingOperations.EnableCollectionSynchronization(Subs, _lockSub);
            //al = new ObservableCollection<Alarm>();
            tempTop = new List<Topic>();
            string[] rizici = { "nema rizika", "niski rizik", "srednji rizik", "visoki rizik" };
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            string address = "net.tcp://localhost:1000/SubscriberService";

            NetTcpBinding bindingSysLog = new NetTcpBinding();
            //string addressSyslog = "net.tcp://localhost:8477/SysLog";

            subCert = CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "SubscriberSign");

            comboBox.ItemsSource = rizici;
            proxy = new SubscriberProxy(binding, address);

            thread = new Thread(new ThreadStart(this.worker_DoWork));
            thread.IsBackground = true;
            thread.Start();

            threadSubscribed = new Thread(new ThreadStart(this.subs_work));
            threadSubscribed.IsBackground = true;
            threadSubscribed.Start();

        }

        private void subs_work()
        {
            while(true)
            {
                Subs.Clear();
                List<Alarm> pom = proxy.SubscribedAlarms(MainWindow.SubName);
                foreach (var item in pom)
                {
                    Subs.Add(item);
                }

                Thread.Sleep(7000);
            }
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            pretplaceni.Clear();
            s = comboBox.SelectedValue.ToString();
            foreach (Topic t in tempTop)
            {
                var obj = pretplaceni.Where(a => a.Izgenerisan == t.Al.Izgenerisan).FirstOrDefault();

                if (s == "nema rizika" && t.Al.Rizik > 0 && t.Al.Rizik < 11 && obj == null)
                    pretplaceni.Add(t.Al);
                else if (s == "niski rizik" && t.Al.Rizik >= 11 && t.Al.Rizik < 31 && obj == null)
                    pretplaceni.Add(t.Al);
                else if (s == "srednji rizik" && t.Al.Rizik >= 31 && t.Al.Rizik < 71 && obj == null)
                    pretplaceni.Add(t.Al);
                else if (s == "visoki rizik" && t.Al.Rizik >= 71 && t.Al.Rizik <= 100 && obj == null)
                    pretplaceni.Add(t.Al);
            }
        }

        private void worker_DoWork()
        {
            while (true)
            {
                var items = al.ToList();
                //al.Clear();

                tempTop = proxy.Read();

                pretplaceni.Clear();
                al.Clear();

                //tempTop = proxy.Read(subCert);

                foreach (Topic t in tempTop)
                {
                    var obj = al.Where(a => a.Izgenerisan == t.Al.Izgenerisan).FirstOrDefault();


                    if (obj == null)
                        al.Add(t.Al);
                    if (s == "nema rizika" && t.Al.Rizik > 0 && t.Al.Rizik < 11 && obj == null)
                        pretplaceni.Add(t.Al);
                    else if (s == "niski rizik" && t.Al.Rizik >= 11 && t.Al.Rizik < 31 && obj == null)
                        pretplaceni.Add(t.Al);
                    else if (s == "srednji rizik" && t.Al.Rizik >= 31 && t.Al.Rizik < 71 && obj == null)
                        pretplaceni.Add(t.Al);
                    else if (s == "visoki rizik" && t.Al.Rizik >= 71 && t.Al.Rizik <= 100 && obj == null)
                        pretplaceni.Add(t.Al);

                }

                Thread.Sleep(6000);
            }
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void dugmePretplatiSe_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid2.SelectedItem != null)
            {
                Alarm a = dataGrid2.SelectedItem as Alarm;

                bool result = proxy.Subscribe(a, MainWindow.SubName);

                if (result)
                {
                    Subs.Add(a);
                    MessageBox.Show("Uspesno ste se predplatili na izabran alarm");
                }
                else
                {
                    MessageBox.Show("Vec ste subscribovani na dati alarm");
                }

                if (!File.Exists(@"..\xmlBaza.xml"))

                {
                    XmlTextWriter txtwriter = new XmlTextWriter(@"..\xmlBaza.xml", System.Text.Encoding.UTF8);
                    txtwriter.Formatting = Formatting.Indented;
                    txtwriter.Indentation = 4;

                    txtwriter.WriteStartDocument();

                    txtwriter.WriteStartElement("Alarmi");
                    txtwriter.WriteStartElement("Alarm");
                    txtwriter.WriteStartElement("Generisan");
                    txtwriter.WriteString(a.Izgenerisan.ToString());
                    txtwriter.WriteEndElement();

                    txtwriter.WriteStartElement("Poruka", "");
                    txtwriter.WriteString(a.Poruka);
                    txtwriter.WriteEndElement();

                    txtwriter.WriteStartElement("Rizik", "");
                    txtwriter.WriteString(a.Rizik.ToString());
                    txtwriter.WriteEndElement();

                    txtwriter.WriteEndElement();
                    txtwriter.WriteEndElement();

                    txtwriter.WriteEndDocument();
                    txtwriter.Close();
                }
                else
                {

                    var xmlDoc = XDocument.Load(@"..\xmlBaza.xml");

                    var parentElement = new XElement("Alarm");
                    var generisanElement = new XElement("Genersan", a.Izgenerisan.ToString());
                    var porukaElement = new XElement("Poruka", a.Poruka);
                    var rizikElement = new XElement("Rizik", a.Rizik.ToString());

                    parentElement.Add(generisanElement);
                    parentElement.Add(porukaElement);
                    parentElement.Add(rizikElement);

                    var rootElement = xmlDoc.Element("Alarmi");

                    rootElement?.Add(parentElement);



                    xmlDoc.Save(@"..\xmlBaza.xml");


                }
            }
            else
            {
                MessageBox.Show("Morate odabrati alarm u okviru data grida!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void startComm()
        {
            StartUp = isStart.No;
            MainWindow.SubName = subnametb.Text;
            MainWindow.Port = Int32.Parse(porttb.Text);
            GridStart.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            SubNameWindow.Title = "Subscriber " + MainWindow.SubName;

            NetTcpBinding bindingClient = new NetTcpBinding();
            string address1 = "net.tcp://localhost:"+MainWindow.Port+ "/ClientService";


            ServiceHost host1 = new ServiceHost(typeof(ClientService));
            host1.AddServiceEndpoint(typeof(IClient), bindingClient, address1);

            host1.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host1.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            try
            {
                host1.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }


        }


    }
}






