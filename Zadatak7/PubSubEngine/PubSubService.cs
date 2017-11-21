using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSubEngine
{ 
    public class PubSubService : Interfejs
    {
        public void AddUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Publish(Alarm alarm, string topicName)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string topicName)
        {
            throw new NotImplementedException();
        }
    }
}
