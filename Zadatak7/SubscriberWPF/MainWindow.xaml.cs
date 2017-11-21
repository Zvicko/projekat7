using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
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
            //worker.DoWork += worker_DoWork;
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

            //worker.RunWorkerAsync();
            Thread thread = new Thread(new ThreadStart(this.worker_DoWork));
            thread.IsBackground = true;
            //thread.Name = "My Worker.";
            thread.Start();

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
                Thread.Sleep(6000);
            }
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void dugmeOsvezi_Click(object sender, RoutedEventArgs e)
        {
           /* var items = al.ToList();

            foreach (var item in items)
            {
                al.Remove(item);
            }

            tempTop = proxy.Read();

            foreach (Topic t in tempTop)
            {

                al.Add(t.Al);
            }   */                 
        }
    }
}
