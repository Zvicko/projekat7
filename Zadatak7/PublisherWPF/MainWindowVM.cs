using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PublisherWPF
{
    public class MainWindowVM : INotifyPropertyChanged
    {

        private string imePub;
        private string vreme;

        public string ImePub
        {
            get { return imePub; }
            set
            {
                imePub = value;
                OnPropertyChanged("ImePub");
            }
        }
        public string Vreme
        {
            get { return vreme; }
            set
            {
                vreme = value;
                OnPropertyChanged("Vreme");
            }
        }

        public MainWindowVM()
        {

            StartUp = isStart.Yes;


        }

        public enum isStart
        {
            Yes,
            No
        }
        private isStart isStartProp = isStart.Yes;

        private ICommand loginCommand;

        public isStart StartUp
        {
            get { return isStartProp; }
            set
            {
                isStartProp = value;
                OnPropertyChanged("StartUp");
            }
        }
        public ICommand StartCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand((param) => this.startComm()));
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
            MainWindow.pubname = ImePub;
            MainWindow.timeToSend = Int32.Parse(Vreme);
        }
    }
}
