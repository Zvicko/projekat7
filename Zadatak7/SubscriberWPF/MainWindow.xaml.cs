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

namespace SubscriberWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Alarm> al
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

        private static SubscriberProxy proxy = null; // mozda ne mora da bude static
        private readonly BackgroundWorker worker = new BackgroundWorker();
        Thread thread = null;
        string s = string.Empty;
        static int pom = 0;
        public MainWindow()
        {

            InitializeComponent();
            object _lock = new object();
            object _lock1 = new object();
            al = new ObservableCollection<Alarm>();
            pretplaceni = new ObservableCollection<Alarm>();
            BindingOperations.EnableCollectionSynchronization(al, _lock);
            BindingOperations.EnableCollectionSynchronization(pretplaceni, _lock1);
            //al = new ObservableCollection<Alarm>();
            tempTop = new List<Topic>();
            DataContext = this;
            string[] rizici = { "nema rizika", "niski rizik", "srednji rizik", "visoki rizik" };
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:1000/SubscriberService";

            NetTcpBinding bindingSysLog = new NetTcpBinding();
            //string addressSyslog = "net.tcp://localhost:8477/SysLog";


            NetTcpBinding bindingClient = new NetTcpBinding();
            string address1 = "net.tcp://localhost:5656/Client";


            ServiceHost host1 = new ServiceHost(typeof(ClientService));
            host1.AddServiceEndpoint(typeof(IClient), bindingClient, address1);

            host1.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host1.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host1.Open();

            comboBox.ItemsSource = rizici;
            proxy = new SubscriberProxy(binding, address);


            thread = new Thread(new ThreadStart(this.worker_DoWork));
            thread.IsBackground = true;
            thread.Start();

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
                if (!File.Exists(@"..\xmlBaza\xmlBaza.xml"))
                {
                    XmlTextWriter txtwriter = new XmlTextWriter(@"..\xmlBaza\xmlBaza.xml", System.Text.Encoding.UTF8);
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
                    var xmlDoc = XDocument.Load(@"..\xmlBaza\xmlBaza.xml");
                    var parentElement = new XElement("Alarm");
                    var generisanElement = new XElement("Genersan", a.Izgenerisan.ToString());
                    var porukaElement = new XElement("Poruka", a.Poruka);
                    var rizikElement = new XElement("Rizik", a.Rizik.ToString());

                    parentElement.Add(generisanElement);
                    parentElement.Add(porukaElement);
                    parentElement.Add(rizikElement);

                    var rootElement = xmlDoc.Element("Alarmi");

                    rootElement?.Add(parentElement);

                    xmlDoc.Save(@"..\xmlBaza\xmlBaza.xml");

                }
            }
            else
            {
                MessageBox.Show("Morate odabrati alarm u okviru data grida!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}






