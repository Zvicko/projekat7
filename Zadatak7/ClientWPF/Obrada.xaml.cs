using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for Obrada.xaml
    /// </summary>
    public partial class Obrada : Window
    {
        public static int cekam = 0;
        public Obrada(string poruka)
        {
            
            InitializeComponent();

            if (poruka == "Nema rizika.")
                cekam = 60;
            else if (poruka == "Rizik je nizak.")
                cekam = 120;
           else if (poruka == "Rizik je srednji.")
                cekam = 180;
           else if (poruka == "Rizik je visok.")
                cekam = 240;

        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < cekam; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(1000);
            }
           
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ObradaAlarma.Value = e.ProgressPercentage;
        }
    }
}
