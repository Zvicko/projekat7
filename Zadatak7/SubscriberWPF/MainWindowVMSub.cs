using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SubscriberWPF
{
    public class MainWindowVMSub : INotifyPropertyChanged
    {
        
        private string imeSub ="" ;
        private string port;

        public string ImeSub
        {
            get { return imeSub; }
            set
            {
                imeSub = value;
                OnPropertyChanged("ImeSub");
            }
        }
        public string Port
        {
            get { return port; }
            set
            {
                port = value;
                OnPropertyChanged("Port");
            }
        }

        public MainWindowVMSub()
        {

            StartUp = isStart.Yes;


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
        private void startComm()
        {
            StartUp = isStart.No;
            MainWindow.SubName = ImeSub;
            MainWindow.Port = Int32.Parse(Port);


            NetTcpBinding bindingClient = new NetTcpBinding();
            string address1 = "net.tcp://localhost:"+Port+"/Client";


            ServiceHost host1 = new ServiceHost(typeof(ClientService));
            host1.AddServiceEndpoint(typeof(IClient), bindingClient, address1);

            host1.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host1.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

            host1.Open();

            
        }
    }
}