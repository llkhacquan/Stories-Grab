using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Stories_Grab
{
    class Network : IDisposable
    {
        private readonly WebClient client;
        public void Dispose()
        {
            if (client != null)
                client.Dispose();
        }
        public Network()
        {
            client = new WebClient() { Proxy = WebRequest.DefaultWebProxy as WebProxy };
            if (client.Proxy != null)
                client.Proxy.Credentials = new NetworkCredential("quannk", "CamVan(3");
            client.Encoding = System.Text.Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
        }

        public string GetPageSource(string url)
        {
            string htmlSource = string.Empty;
            if (!url.StartsWith("http://"))
                url = "http://" + url;
            htmlSource = client.DownloadString(url);
            return htmlSource;
        }
    }
}
