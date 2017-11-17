using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Topic
    {
        private string nazivTopica;
        private Alarm al;

        public string NazivTopica
        {
            get { return nazivTopica; }
            set { nazivTopica = value; }
        }

        public Alarm Al
        {
            get { return al; }
            set { al = value; }
        }
    }
}
