using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Alarm
    {
        private DateTime izgenerisan;
        private string poruka;
        private int rizik;

        public DateTime Izgenerisan
        {
            get
            {
                return izgenerisan;
            }
            set
            {
                izgenerisan = value;
            }

        }

        public string Poruka
        {
            get
            {
                return poruka;
            }
            set
            {
                poruka = value;

            }

        }

       public int Rizik
       {
            get
            {
                return rizik;
            }
            set
            {
                rizik = value;
            }
       }

    }
}
