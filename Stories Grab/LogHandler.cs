using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories_Grab
{
    public class LogHandler
    {
        private List<Log> logs = new List<Log>();

        public void log(string s)
        {
            Log l = new Log();
            l.time = DateTime.Now;
            l.content = s;
            logs.Add(l);
        }
    }


    public struct Log
    {
        public DateTime time;
        public string content;
    }
}
