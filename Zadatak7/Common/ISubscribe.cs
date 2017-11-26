using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;

namespace Common
{
    [ServiceContract]
    public interface ISubscribe
    {
        [OperationContract]
        bool Subscribe(Alarm alarm,string imeSub);

        [OperationContract]
        List<Topic> Read();

        [OperationContract]
        List<Alarm> SubscribedAlarms(string imeSub);

        /*[OperationContract]
        List<Topic> Read(X509Certificate2 certificate);*/
    }
}
