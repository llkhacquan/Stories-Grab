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

        private readonly static string header = "<?xml version=\"1.0\" encoding=\"utf-8\"?><!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\"  \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head>  <h1>THIS IS TITLE</h1></head><body>";

        public void writeToFile(string filePath)
        {
            Debug.Assert(!string.IsNullOrEmpty(filePath), "filePath is null or empty.");
            using (StreamWriter f = new StreamWriter(filePath))
            {
                f.Write(header.Replace("THIS IS TITLE", title));
                f.Write(contentHtml);
                f.Write("</body></html>");
                f.Close();
            }
        }
    }
}
