using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class SyslogMessage
    {
        [DataMember]
        private string _data;
        [DataMember]
        private string _time;
        [DataMember]
        private string _facility;
        [DataMember]
        private string _severity;
        [DataMember]
        private string _client;
        [DataMember]
        private string _message;

        public SyslogMessage()
        {

        }
        public SyslogMessage(string data, string time, string facility, string severity, string message, string client)
        {
            this._data = data;
            this._time = time;
            this._facility = facility;
            this._severity = severity;
            this._message = message;
            this._client = client;

        }
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string Facility
        {
            get { return _facility; }
            set { _facility = value; }
        }

        public string Severity
        {
            get { return _severity; }
            set { _severity = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

    }
}