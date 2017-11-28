using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public NetTcpBinding binding = new NetTcpBinding();
        public string address = "";
        public static ObservableCollection<Alarm> Prikazi
        {
            get;
            set;
        }

        private static string imeClient = "";
        private static int portnum;

        public static string ImeClient
        {
            get { return imeClient; }
            set
            {
                imeClient = value;
            }
        }
        public static int PortNum
        {
            get { return portnum; }
            set
            {
                portnum = value;
            }
        }

        private ICommand loginCommandSub;


        public ICommand StartCommandClient
        {
            get
            {
                return loginCommandSub ?? (loginCommandSub = new RelayCommand((param) => this.startComm()));
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Prikazi = new ObservableCollection<Alarm>();
            object _lock = new object();
            BindingOperations.EnableCollectionSynchronization(Prikazi, _lock);

        }

        private void startComm()
        {
            MainWindow.ImeClient = subnametb.Text;
            MainWindow.PortNum = Int32.Parse(porttb.Text);
            GridStart.Visibility = Visibility.Collapsed;
            mainGrid.Visibility = Visibility.Visible;
            clientName.Title = "Client  " + MainWindow.ImeClient;
            address = "net.tcp://localhost:" + portnum + "/ClientService";
            List<Alarm> temp = new List<Alarm>();
            using (ClientProxy proxy = new ClientProxy(binding, address))
            {
                temp = proxy.ShowAllAlarms();

                foreach (Alarm a in temp)
                {
                    Prikazi.Add(a);
                }

            }
        }

        private void Obradi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                using (ClientProxy proxy = new ClientProxy(binding, address))
                {

                    Alarm ala = dataGrid.SelectedItem as Alarm;
                    if (ala.Poruka == "Nema rizika.")
                    {
                        var s = new Obrada(ala.Poruka);
                        proxy.DoAlarm(ala);
                        s.ShowDialog();
                        
                    }
                    else if (ala.Poruka == "Rizik je nizak.")
                    {
                        var s = new Obrada(ala.Poruka);
                        proxy.DoAlarm(ala);
                        s.ShowDialog();

                    }
                    else if (ala.Poruka == "Rizik je srednji.")
                    {
                        var s = new Obrada(ala.Poruka);
                        proxy.DoAlarm(ala);
                        s.ShowDialog();

                    }
                    else if (ala.Poruka == "Rizik je visok.")
                    {
                        var s = new Obrada(ala.Poruka);
                        proxy.DoAlarm(ala);
                        s.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate odabrati alarm!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
