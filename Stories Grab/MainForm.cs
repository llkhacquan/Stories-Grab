using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Stories_Grab
{
    public partial class MainForm : Form
    {
        BackgroundWorker bw;
        LogHandler logger = new LogHandler();

        public MainForm()
        {
            InitializeComponent();

            nPagesOfMenu.Value = Properties.Settings.Default.nPagesOfMenu;
            storyLinkText.Text = Properties.Settings.Default.sUrl;
            string folderPath = Properties.Settings.Default.sFolderPath;

            if (!Directory.Exists(folderPath))
            {
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Epub";
                folderTextBox.Text = folderPath;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (folderTextBox.Text.Length == 0)
                return;
            if (!Directory.Exists(folderTextBox.Text))
            {
                Directory.CreateDirectory(folderTextBox.Text);
            }
            bw = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };

            bw.DoWork += BackgroundWorker_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.ProgressChanged += Bw_ProgressChanged;

            bw.RunWorkerAsync();
            startBtn.Enabled = false;
            pauseBtn.Enabled = true;
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercentage;
            toolStripStatusLabel.Text = e.UserState.ToString();
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            startBtn.Enabled = true;
            pauseBtn.Enabled = false;
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IGrapper grapper = new ISachInfo();
            string url = storyLinkText.Text;
            int pages = (int)nPagesOfMenu.Value;
            List<string> chapterUrls = grapper.GetChapterLinks(url, 1, pages);
            for (int i = 0; i < chapterUrls.Count; i++)
            {
                string s = grapper.GetCorespondingFileName(chapterUrls[i]);
                string filePath = string.Format("{0}\\{1}", folderTextBox.Text, s);
                bool exists = File.Exists(filePath);
                if (exists)
                {
                    logger.log(s + " exists. Skip:");
                    continue;
                }
                Chapter c = grapper.GrabChapterContent(chapterUrls[i]);
                c.writeToFile(filePath);
                logger.log(s + " is written");
                bw.ReportProgress(i * 100 / chapterUrls.Count, i);
            }
        }

        private void folderTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.sFolderPath = folderTextBox.Text;
            Properties.Settings.Default.nPagesOfMenu = (int)nPagesOfMenu.Value;
            Properties.Settings.Default.sUrl = storyLinkText.Text;
            Properties.Settings.Default.Save();
        }
    }
}
