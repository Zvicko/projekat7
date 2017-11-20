using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
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

namespace SubscriberWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] rizici = { "nema rizika", "blagi rizik", "rizicno", "veoma rizicno" };
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:1000/SubscriberService";

            NetTcpBinding bindingSysLog = new NetTcpBinding();
            string addressSyslog = "net.tcp://localhost:8477/SysLog";


            NetTcpBinding bindingClient = new NetTcpBinding();
            string address1 = "net.tcp://localhost:5656/Client";


            ServiceHost host1 = new ServiceHost(typeof(ClientService));
            host1.AddServiceEndpoint(typeof(IClient), bindingClient, address1);

            host1.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host1.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host1.Open();

            comboBox.ItemsSource = rizici;
            using (SubscriberProxy proxy = new SubscriberProxy(binding, address))
            {
                proxy.Read();
                proxy.Subscribe("burek");
            }


        }
    }
}
