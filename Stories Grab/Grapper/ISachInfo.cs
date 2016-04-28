using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories_Grab
{
    class ISachInfo : IGrapper
    {
        public Chapter GrabChapterContent(string url)
        {
            Chapter chapter = new Chapter();
            List<string> re = new List<string>();
            Network network = new Network();
            string htmlSource = network.GetPageSource(url);
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlSource);

            IEnumerable<HtmlAgilityPack.HtmlNode> divs = htmlDoc.DocumentNode.Descendants("div").
                Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("motsach_content_body"));
            var divs2 = divs.ToArray()[0];

            StringBuilder sb = new StringBuilder();
            foreach (var item in divs2.ChildNodes)
            {
                if (item.Attributes.Contains("class"))
                {
                    if (item.Attributes["class"].Value == "ms_text")
                        sb.AppendLine(item.OuterHtml);
                    else if (item.Attributes["class"].Value == "ms_chapter")
                    {
                        chapter.title = item.InnerText;
                    }
                }
                chapter.contentHtml = sb.ToString();
            }
            return chapter;
        }

        /// <summary>
        /// http://isach.info/story.php?story=toi_thay_hoa_vang_tren_co_xanh__nguyen_nhat_anh&chapter=0000
        /// </summary>
        /// <param name="url"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<string> GetChapterLinks(string url, int start, int end)
        {
            List<string> re = new List<string>();
            Network network = new Network();
            string htmlSource = network.GetPageSource(url);
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlSource);

            IEnumerable<HtmlAgilityPack.HtmlNode> divs = htmlDoc.DocumentNode.Descendants("div").
                Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("right_menu_item"));
            var divs2 = divs.ToArray();
            foreach (var d in divs2)
            {
                if (d.Id.StartsWith("c"))
                {
                    re.Add("http://isach.info/" + d.ChildNodes[0].Attributes[0].Value);
                }
            }
            return re;
        }

        public string GetCorespondingFileName(string url)
        {
            return url.Substring(url.Length - 4) + ".xhtml";
        }
    }
}
