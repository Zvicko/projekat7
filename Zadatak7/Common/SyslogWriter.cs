using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SysLogWriter
    {
        private static StreamWriter _logWriter = null;
        private static readonly object locker = new object();

        private static bool _isWritten = false;
       
        public static void WriteSysLog(SyslogMessage message)
        {
            lock (locker)
            {
                if (_logWriter == null)
                {
                    _logWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\logfile.txt");
                }
                if (_isWritten)
                {
                    _logWriter = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "\\logfile.txt");
                }


                string line = message.Data + " " + message.Time + " " + message.Facility + " " + message.Severity + " " +
                              message.Client + " " + message.Message + "\n ";
                lock (locker)
                {
                    _logWriter.WriteLine(line);
                    _isWritten = true;
                    _logWriter.Close();
                }

            }
        }

    }
}