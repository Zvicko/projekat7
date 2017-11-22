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



        public static List<Topic> tempTop
        {
            get;
            set;
        }

        private static SubscriberProxy proxy = null; // mozda ne mora da bude static
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public MainWindow()
        {

            InitializeComponent();
            object _lock = new object();
            al = new ObservableCollection<Alarm>();
            BindingOperations.EnableCollectionSynchronization(al, _lock);
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


            Thread thread = new Thread(new ThreadStart(this.worker_DoWork));
            thread.IsBackground = true;

        }
        private void Potvrdi_click(object sender, RoutedEventArgs e)
        {
            string[] rizici = { "nema rizika", "niski rizik", "srednji rizik", "visoki rizik" };
            string s = comboBox.SelectedValue.ToString();

            int pom = 1;

            if (s == "nema rizika")
                pom = 1;
            if (s == "niski rizik")
                pom = 2;
            if (s == "srednji rizik")
                pom = 3;
            if (s == "visoki rizik")
                pom = 4;

            al.Clear();
            foreach (Topic t in tempTop)
            {
                if (pom == 1)
                {
                    if (t.Al.Rizik > 0 && t.Al.Rizik < 11)
                        al.Add(t.Al);
                }
                else if (pom == 2)
                {
                    if (t.Al.Rizik >= 11 && t.Al.Rizik < 31)
                        al.Add(t.Al);
                }
                else if (pom == 3)
                {
                    if (t.Al.Rizik >= 31 && t.Al.Rizik < 71)
                        al.Add(t.Al);
                }
                else if (pom == 4)
                {
                    if (t.Al.Rizik >= 71 && t.Al.Rizik <= 100)
                        al.Add(t.Al);
                }
            }
        }





        private void worker_DoWork()
        {
            while (true)
            {
                var items = al.ToList();

                foreach (var item in items)
                {
                    al.Remove(item);
                }

                tempTop = proxy.Read();

                foreach (Topic t in tempTop)
                {
                    al.Add(t.Al);
                }
                //dataGrid.Items.Refresh();
                Thread.Sleep(2000);
            }
        }


        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void dugmeOsvezi_Click(object sender, RoutedEventArgs e)
        {
            var items = al.ToList();

            foreach (var item in items)
            {
                al.Remove(item);
            }

            tempTop = proxy.Read();

            foreach (Topic t in tempTop)
            {

                al.Add(t.Al);
            }
        }
    }
}






