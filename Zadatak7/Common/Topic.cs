using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract()]
    public class Topic
    {
        private string nazivTopica;
        private Alarm al;
        private string nazivPub;

        [DataMember()]
        public string NazivTopica
        {
            get { return nazivTopica; }
            set { nazivTopica = value; }
        }

        [DataMember()]
        public Alarm Al
        {
            get { return al; }
            set { al = value; }
        }
        [DataMember()]
        public string NazivPub
        {
            get { return nazivPub; }
            set { nazivPub = value; }
        }
    }
}
