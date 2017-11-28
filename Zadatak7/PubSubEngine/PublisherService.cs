using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Common;
using Manager;

namespace PubSubEngine
{
    public class PublisherService : IPublish
    {
        public static List<Topic> ListTopic
        {
            get;
            set;
        }

        public PublisherService()
        {
            ListTopic = new List<Topic>();
        }

        public void Publish(Topic topic)
        {
            ListTopic.Add(topic);

            //ListTopic.ForEach(t => Console.WriteLine($"{t.NazivTopica}\n{t.Al.Izgenerisan}\n{t.Al.Poruka}\n{t.Al.Rizik}\n"));
        }

        /*public void Publish(Topic topic, X509Certificate2 certificate)
        {
            string poruka = topic.Al.Poruka;

            byte[] potpis = DigitalSignature.Create(poruka, "SHA1", certificate);

            topic.Potpis = potpis;

            ListTopic.Add(topic);
        }
        */

        public void Publish(Topic topic, byte[] sign)
        {
            X509Certificate2 clientCertificate = Manager.CertificateManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "PublisherSign");

            if (DigitalSignature.Verify(topic.Al.Poruka, "SHA1", sign,clientCertificate))
            {
                Console.WriteLine("Valid.");
                ListTopic.Add(topic);
            }
           
        }

        public bool ShutDown(string pubName, bool flag)
        {
            lock (SubscriberService._locck)
            {
                List<Alarm> alarms = new List<Alarm>();

                foreach (var item in SubscriberService.mainDic.Keys)
                {
                    if (item.ImePub.Equals(pubName))
                    {
                        alarms.Add(item);
                    }
                }
                foreach (var item in alarms)
                {
                    SubscriberService.mainDic.Remove(item);
                }

            }

            foreach (var item in ListTopic)
            {
                if (item.NazivPub == pubName)
                {
                    ListTopic.Remove(item);
                    break;
                }
            }

            ListTopic.RemoveAll(x => x.NazivPub == pubName);

            return true;
        }
    }
}
