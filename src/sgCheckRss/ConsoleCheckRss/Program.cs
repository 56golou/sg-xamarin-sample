using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCheckRss
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://rss.rssad.jp/rss/codezine/new/20/index.xml";
            url = "https://blogs.windows.com/feed/";
            var rsscl = new RSSClient();
            var task = rsscl.Open(url);
            task.Wait();
            var msg = task.Result;
            Console.WriteLine(msg);
            var rss = RSS.Load(msg);
        }
    }
    public class RSSClient
    {
        public async Task<string> Open(string url)
        {
            var cl = new HttpClient();
            var res = await cl.GetStringAsync(url);
            return res;
        }
    }

}
