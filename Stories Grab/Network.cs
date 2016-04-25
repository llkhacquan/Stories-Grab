using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Stories_Grab
{
    class Network : IDisposable
    {
        private readonly WebProxy myProxy;
        private readonly WebClient client;
        public void Dispose()
        {
            if (client != null)
                client.Dispose();
        }
        public Network()
        {
            WebProxy myProxy = new WebProxy("hl-proxyb", 8080) { Credentials = new NetworkCredential("quannk", "BuiVan(3") };
            client = new WebClient();
            client.Proxy = myProxy;
            client.Proxy.Credentials = new NetworkCredential("quannk", "VuHinh(3");
            client.Encoding = System.Text.Encoding.UTF8;
            client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
        }

        public string GetPageSource(string url)
        {
            string htmlSource = string.Empty;
            htmlSource = client.DownloadString(url);
            return htmlSource;
        }
    }
}
