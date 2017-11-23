using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace Common
{
    public class Alarm
    {
        private DateTime izgenerisan;
        private string poruka;
        private int rizik;
        private string imePub;
      
        public DateTime Izgenerisan
        {
            get { return izgenerisan; }
            set { izgenerisan = value; }

        }
        
        public string Poruka
        {
            get { return poruka; }
            set { poruka = value; }
        }
       
        public int Rizik
        {
             get { return rizik; }
             set { rizik = value; }
        }

        public string ImePub
        {
            get { return imePub; }
            set
            {
                imePub = value;
            }
        }
    }
}
