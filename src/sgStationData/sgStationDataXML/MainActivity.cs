using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using sgStationData;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;

namespace sgStationDataXML
{
[Activity(Label = "sgStationDataXML", MainLauncher = true, Icon = "@drawable/icon")]
public class MainActivity : Activity
{
    Spinner spLine;
    ListView lv;
    List<JRLine> jrlines;


    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.Main);

        spLine = FindViewById<Spinner>(Resource.Id.spinner1);
        lv = FindViewById<ListView>(Resource.Id.listView1);


        // 路線データを登録
        jrlines = new List<JRLine>();
        jrlines.Add(new JRLine() { line_cd = 11109, line_name = "JR千歳線" });
        jrlines.Add(new JRLine() { line_cd = 11201, line_name = "JR東北本線(八戸～青森)" });
        jrlines.Add(new JRLine() { line_cd = 11302, line_name = "JR山手線" });
        jrlines.Add(new JRLine() { line_cd = 11305, line_name = "JR武蔵野線" });
        jrlines.Add(new JRLine() { line_cd = 11623, line_name = "大阪環状線" });
        jrlines.Add(new JRLine() { line_cd = 28001, line_name = "東京メトロ銀座線" });
        jrlines.Add(new JRLine() { line_cd = 99927, line_name = "ゆいレール" });

        var ad = new ArrayAdapter<JRLine>(this, Android.Resource.Layout.SimpleListItem1, jrlines);
        spLine.Adapter = ad;
        spLine.ItemSelected += SpLine_ItemSelected;
    }

    private void BtnGet_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 路線の選択時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void SpLine_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
    {
        var cd = jrlines[e.Position].line_cd;

        var jrline = await GetLineInfo(cd);

        var ad = new ArrayAdapter<JRStation>(this, Android.Resource.Layout.SimpleListItem1, jrline.station_l);
        lv.Adapter = ad;

    }

    /// <summary>
    /// XML形式でデータ取得
    /// </summary>
    /// <returns></returns>
    public async Task<JRLine> GetLineInfo(int line_cd)
    {
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
        var st = new System.IO.StringReader(xml);
        var doc = XDocument.Load(st);

        var jrline = new JRLine();
        jrline.station_l = new List<JRStation>();
        var line = doc.Root.Element("line");
        jrline.line_cd = int.Parse(line.Element("line_cd").Value);
        jrline.line_name = line.Element("line_name").Value;
        jrline.line_lon = double.Parse(line.Element("line_lon").Value);
        jrline.line_lat = double.Parse(line.Element("line_lat").Value);
        jrline.line_zoom = int.Parse(line.Element("line_zoom").Value);
        foreach (var it in doc.Root.Elements())
        {
            if (it.Name == "station")
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

