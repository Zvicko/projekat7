using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
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
        Topic topic = new Topic();
        PublisherProxy proxy = null;
        public MainWindow()
        {
            InitializeComponent();
            
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/PublisherService";


            proxy = new PublisherProxy(binding, address);
           
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

                    topic.Al.Poruka = Poruke.ResourceManager.GetString(poruka);
                    proxy.Publish(topic);
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


    }
}
