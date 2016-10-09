using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace sgStationData
{
    [Activity(Label = "sgStationData", MainLauncher = true, Icon = "@drawable/icon")]
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

        public async Task<JRLine> GetLineInfo( int line_cd )
        {
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
            var data = js.Deserialize<JRLine>(jr);

            return data;

        }
    }
}

