using System;
using System.Collections.Generic;
using System.Linq;

namespace Stories_Grab
{
    public class TruyenFullGrapper : IGrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">http://truyenfull.vn/bach-luyen-thanh-tien/</param>
        /// <param name="i">1</param>
        /// <param name="j">50</param>
        /// <returns></returns>
        public List<string> GetChapterLinks(string url, int start, int end)
        {
            List<string> re = new List<string>();
            for (int iPage = start; iPage <= end; iPage++)
            {
                Network network = new Network();
                string htmlSource = network.GetPageSource(string.Format("{0}/trang-{1}", url, iPage));
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(htmlSource);
                IEnumerable<HtmlAgilityPack.HtmlNode> _listChapterDivs = htmlDoc.DocumentNode.Descendants("ul").
                    Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("list-chapter"));
                var listChapterDivs = _listChapterDivs.ToArray();
                foreach (var div in listChapterDivs)
                {
                    foreach (var il in div.ChildNodes)
                    {
                        if (il.Name == "li")
                        {
                            var a = il.ChildNodes[2].Attributes[0];
                            re.Add(a.Value);
                        }
                    }
                }
            }
            return re;
        }

        public Chapter GrabChapterContent(string url)
        {
            Network network = new Network();
            string htmlSource = network.GetPageSource(url);
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlSource);
            IEnumerable<HtmlAgilityPack.HtmlNode> divs = htmlDoc.DocumentNode.Descendants("div").
                Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("chapter-content"));
            var divs2 = divs.ToArray();
            if (divs2.Length > 0)
            {
                Chapter result = new Chapter();
                result.contentHtml = divs.ElementAt(0).InnerHtml;
                result.title = htmlDoc.DocumentNode.Descendants("title").ToArray()[0].InnerText;
                int i = url.IndexOf("/chuong-") + "/chuong-".Length;
                result.chapter = url.Substring(i, url.Length - i - 1);
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// http://truyenfull.vn/truyen-than-khong-thien-ha/chuong-1/
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetCorespondingFileName(string url)
        {
            string[] parts = url.Split(new char[] { '/' });
            return parts.Last() + "xhtml";
        }
    }
}
