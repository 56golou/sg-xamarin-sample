using sgStationData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            var t = prog.Go();
            t.Wait();
            var data = t.Result;

            int i = 0;
            foreach (var it in data.station_l)
            {
                i++;
                Console.WriteLine(string.Format("{0}: {1}", i, it.station_name));
            }
            return;
        }

        /// <summary>
        /// XML形式でデータ取得
        /// </summary>
        /// <returns></returns>
        public async Task<JRLine> Go()
        {
            var line_cd = 11109;
            var url = $"http://www.ekidata.jp/api/l/{line_cd}.xml";

            var hc = new HttpClient();
            var res = await hc.GetAsync(url);
            var xml = await res.Content.ReadAsStringAsync();

            Console.WriteLine(xml);

            /*
             * XML形式が特殊なので、手作業で読み込む
            var st = new StringReader(xml);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(JRLine));
            var data = xs.Deserialize(st) as JRLine;
            return data;
            */
            var st = new StringReader(xml);
            var doc = XDocument.Load(st);

            var jrline = new JRLine();
            jrline.station_l = new List<JRStation>();
            var line = doc.Root.Element("line");
            jrline.line_cd = int.Parse( line.Element("line_cd").Value);
            jrline.line_name = line.Element("line_name").Value;
            jrline.line_lon = double.Parse( line.Element("line_lon").Value);
            jrline.line_lat = double.Parse( line.Element("line_lat").Value);
            jrline.line_zoom = int.Parse(line.Element("line_zoom").Value);
            foreach ( var it in doc.Root.Elements())
            {
                if ( it.Name == "station")
                {
                    var item = new JRStation();
                    item.station_cd = int.Parse(it.Element("station_cd").Value);
                    item.station_g_cd = int.Parse(it.Element("station_g_cd").Value);
                    item.station_name = it.Element("station_name").Value;
                    item.lon = double.Parse(it.Element("lon").Value);
                    item.lat = double.Parse(it.Element("lat").Value);
                    jrline.station_l.Add(item);
                }
            }
            return jrline;
        }
    }
}
