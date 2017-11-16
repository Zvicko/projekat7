using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
   public interface Interfejs
    {
        [OperationContract(IsOneWay = true)]
        void Publish(Alarm alarm, string topicName);
        [OperationContract]
        void RemoveUser(string username);
        [OperationContract]
        void AddUser(string username, string password);
        [OperationContract]
        void Subscribe(string topicName);
    }
}
