using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stories_Grab
{
    public class LogHandler
    {
        private List<Log> logs = new List<Log>();

        TextBox textBox;
        int iTextBoxLogs;

        Timer timer;

        public LogHandler()
        {
            timer = new Timer() { Interval = 50, Enabled = true, Tag = this };
            timer.Tick += Timer_Tick;
        }

        public bool LogToUI
        {
            get
            {
                return timer.Enabled;
            }
            set
            {
                timer.Enabled = value;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (textBox == null)
                return;
            for (; iTextBoxLogs < logs.Count; iTextBoxLogs++)
            {
                Log l = logs[iTextBoxLogs];
                textBox.AppendText(string.Format("{0:yyyy/MM/dd HH:mm:ss}\t{1}{2}", l.time, l.content, Environment.NewLine));
            }
        }

        public void log(string s)
        {
            Log l = new Log() { time = DateTime.Now, content = s };
            logs.Add(l);
        }

        public void AssignEditControl(TextBox _textBox)
        {
            this.textBox = _textBox;
            iTextBoxLogs = logs.Count;
        }
    }
}
