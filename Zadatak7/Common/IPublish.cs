using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Common
{
    [ServiceContract]
    public interface IPublish
    {
        [OperationContract(IsOneWay =true)]
        void Publish(Topic topic);
        [OperationContract]
        bool ShutDown(string pubName,bool flag);
    }
}
