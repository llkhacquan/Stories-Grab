using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace Stories_Grab
{
    public class Chapter
    {
        public string title;
        public string contentHtml;
        public string chapter;

        private readonly static string header = File.ReadAllText("header.txt");

        public void writeToFile(string filePath)
        {
            Debug.Assert(!String.IsNullOrEmpty(filePath), "filePath is null or empty.");
            using (StreamWriter f = new System.IO.StreamWriter(filePath))
            {
                f.Write(header.Replace("THIS IS TITLE", title));
                f.Write(contentHtml);
                f.Write("</body></html>");
                f.Close();
            }
        }
    }
}
