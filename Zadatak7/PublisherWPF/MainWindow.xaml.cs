using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace PublisherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/PublisherService";
            Topic topic = new Topic();
            topic.Al = new Alarm();

            using (PublisherProxy proxy = new PublisherProxy(binding, address))
            {

                Console.WriteLine("Unesite naziv topic-a:");
                topic.NazivTopica = Console.ReadLine();
                topic.Al.Poruka = "poruka";
                topic.Al.Rizik = 5;
                topic.Al.Izgenerisan = DateTime.Now;
                proxy.Publish(topic);

            }

        }
    }
}
