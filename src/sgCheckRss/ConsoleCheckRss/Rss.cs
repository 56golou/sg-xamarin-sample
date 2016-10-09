using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleCheckRss
{
    public class RSS
    {
        public static RSS Load( string xml )
        {
            var st = new StringReader(xml);
            var doc = XDocument.Load(st);


#if false
            // デシリアライズがうまくいかないので、自前でRSSクラスにつめる
            var items = new XElement("items");
            var channel = doc.Root.Element("channel");
            foreach ( var it in channel.Elements())
            {
                if ( it.Name == "item")
                {
                    items.Add(it);
                }
            }
            channel.Add(items);

            var sb = new StringBuilder();
            var xw = XmlWriter.Create(sb);
            doc.Save(xw);
            var sbs = new StringReader(sb.ToString());
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSS));
            var rss = xs.Deserialize(sbs) as RSS;

            return rss;
#endif
            // リフレクションを使ったほうがシンプルだけど、
            // 手間じゃないのでベタに書く
            var rss = new RSS();
            var channel = doc.Root.Element("channel");
            rss.channel = new RSSChannel();
            rss.channel.title = channel.Element("title").Value;
            rss.channel.title = channel.Element("title").Value;
            rss.channel.language = channel.Element("language").Value;
            rss.channel.Items = new List<RSSItem>();
            foreach (var it in channel.Elements())
            {
                if (it.Name == "item")
                {
                    var item = new RSSItem();
                    item.title = it.Element("title").Value;
                    item.link = it.Element("link").Value;
                    item.guid = it.Element("guid").Value;
                    item.pubDate = DateTime.Parse( it.Element("pubDate").Value);
                    item.description = it.Element("description").Value;
                    rss.channel.Items.Add(item);
                }
            }
            return rss;
        }

        public RSSChannel channel { get; set; }
    }
    public class RSSChannel
    {
        public string title { get; set; }
        public string language { get; set; }
        public List<RSSItem> Items { get; set; }
    }
    public class RSSItem
    {
        public string title { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public DateTime pubDate { get; set; }
        public string description { get; set; }

        public override string ToString()
        {
            return this.title;
        }
    }
}
