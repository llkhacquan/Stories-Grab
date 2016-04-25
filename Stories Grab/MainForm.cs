using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Stories_Grab
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private readonly static string footer = "</body></html>";
        private void startBtn_Click(object sender, EventArgs e)
        {
            if (folderTextBox.Text.Length == 0)
                return;
            TruyenFullGrapper grapper = new TruyenFullGrapper();
            string url = "http://truyenfull.vn/bach-luyen-thanh-tien/";
            List<String> chapterUrls = grapper.GetChapterLinks(url, 1, 50);
            for (int i = 0; i < chapterUrls.Count; i++)
            {
                string s = chapterUrls[i].Substring(url.Length);
                s = s.Remove(s.Length - 1);
                string filePath = String.Format("{0}\\{1}.xhtml", folderTextBox.Text, s);
                bool exists = File.Exists(filePath);
                if (exists)
                {
                    content.AppendText(s + " exists. Skip\r\n");
                    content.Refresh();
                    continue;
                }
                Chapter c = grapper.GrabChapterContent(chapterUrls[i]);
                c.writeToFile(filePath);
                content.AppendText(s + " is written.\r\n");
                content.Refresh();
            }
        }

        private void folderTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void content_VisibleChanged(object sender, EventArgs e)
        {
            if (content.Visible)
            {
                content.SelectionStart = content.TextLength;
                content.ScrollToCaret();
            }
        }
    }
}
