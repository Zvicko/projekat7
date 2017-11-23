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

        public static ObservableCollection<Alarm> Prikazi
        {
            get;
            set;
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Prikazi = new ObservableCollection<Alarm>();
            object _lock = new object();

            BindingOperations.EnableCollectionSynchronization(Prikazi, _lock);
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:10010/ClientService";
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
                Alarm ala = dataGrid.SelectedItem as Alarm;
                if (ala.Poruka == "Nema rizika.")
                {

                }
                else if (ala.Poruka == "Rizik je nizak.")
                {

                }
                else if (ala.Poruka == "Rizik je srednji.")
                {

                }
                else if (ala.Poruka == "Rizik je visok.")
                {

                }
            }
            else
            {
                MessageBox.Show("Morate odabrati alarm!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
