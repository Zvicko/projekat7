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
    public interface IPublish
    {
        [OperationContract(IsOneWay = true)]
        void Publish(Topic topic);

        /*[OperationContract(IsOneWay = true)]
        void Publish(Topic topic, X509Certificate2 certificate);*/
    }
}
