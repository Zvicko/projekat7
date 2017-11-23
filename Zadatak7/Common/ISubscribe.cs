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
        void Subscribe(Topic topic,string imeSub);

        [OperationContract]
        List<Topic> Read();

        /*[OperationContract]
        List<Topic> Read(X509Certificate2 certificate);*/
    }
}
