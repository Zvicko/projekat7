using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
            var identity = Thread.CurrentPrincipal;
            var winId = identity as WindowsPrincipal;

            var time = DateTime.Now.ToString("HH:mm:ss tt");

            var data = DateTime.Now.ToString("MMMM dd, yyyy");
            var facility = "Clinet";
            var severiti = "urgent";
            var client = Thread.CurrentPrincipal.Identity.Name;

            var mess = " DoAlarm operaiton succeed";
            var currentLog = new SyslogMessage(data, time, facility, severiti, mess, client);

            // Generisani log se salje na Main server , u slucaju otkaza prevezuje se na Backup
            try
            {
                using (SysLogProxy proxy = new SysLogProxy(new NetTcpBinding(),SysLogProxy.address1))
                {
                    proxy.EnterLog(currentLog);
                }
            }
            catch (CommunicationException e)
            {

                Console.Write(e.Message);
            }

            foreach (var item in Alarmi)
            {
                if (a.Izgenerisan == item.Izgenerisan)
                {

                }
            }
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
