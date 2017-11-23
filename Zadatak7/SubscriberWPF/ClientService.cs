using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SubscriberWPF
{
    public class ClientService : IClient
    {
        public static List<Alarm> Alarmi
        {
            get;
            set;
        }
        public ClientService()
        {
            Alarmi = new List<Alarm>();
        }


        public void DoAlarm(Alarm a)
        {
            throw new NotImplementedException();
        }

        public List<Alarm> ShowAllAlarms()
        {
            //Alarm a = new Alarm();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\xmlBaza.xml");
            XmlNodeList elementList = doc.GetElementsByTagName("Alarm");
            XmlNodeList childList = null;
            XmlNode child = null;
            for (int i = 0; i < elementList.Count; i++)
            {
                Alarm a = new Alarm();
                childList = elementList[i].ChildNodes;
                
                    child = childList.Item(0);
                    a.Izgenerisan = Convert.ToDateTime(child.FirstChild.Value.Trim());
                    child = childList.Item(1);
                    a.Poruka = child.FirstChild.Value.Trim();
                    child = childList.Item(2);
                    a.Rizik = Convert.ToInt32(child.FirstChild.Value.Trim());
                    Alarmi.Add(a);
                

            }
            return Alarmi;
        }
    }
}
