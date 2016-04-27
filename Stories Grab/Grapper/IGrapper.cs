using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories_Grab
{
    public interface IGrapper
    {
        Chapter GrabChapterContent(string url);

        string GetCorespondingFileName(string url);

        List<string> GetChapterLinks(string url, int start, int end);
    }
}
