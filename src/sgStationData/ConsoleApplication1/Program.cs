using sgStationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
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
        foreach( var it in data.station_l )
        {
            i++;
            Console.WriteLine(string.Format("{0}: {1}", i, it.station_name));
        }
        return;
    }
    public async Task<JRLine> Go()
    {
        var line_cd = 11109;
        var url = $"http://www.ekidata.jp/api/l/{line_cd}.json";

        var hc = new HttpClient();
        var res = await hc.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        Console.WriteLine(json);

        // 余分なjavascriptを取り除く
        json = json.Replace("if(typeof(xml)=='undefined') xml = {};", "");
        json = json.Replace("xml.data = ", "");
        json = json.Replace("if(typeof(xml.onload)=='function') xml.onload(xml.data);", "");

        var js = new Newtonsoft.Json.JsonSerializer();
        var jr = new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(json));
        var data = js.Deserialize<JRLine> (jr);

        return data;

    }
}
}
