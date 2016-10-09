using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using ConsoleCheckRss;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;

namespace sgCheckRss
{
    [Activity(Label = "sgCheckRss", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView textUrl;
        ListView lv;
        Button btnGet;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            // Button button = FindViewById<Button>(Resource.Id.MyButton);
            this.textUrl = FindViewById<TextView>(Resource.Id.textView1);
            this.lv = FindViewById<ListView>(Resource.Id.listView1);
            this.btnGet = FindViewById<Button>(Resource.Id.button1);

            btnGet.Click += BtnGet_Click;
        }

        RSS _rss;
        /// <summary>
        /// RSSを取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnGet_Click(object sender, EventArgs e)
        {
            var url = "https://blogs.windows.com/feed/";
            var rsscl = new RSSClient();
            var xml = await rsscl.Open(url);
            var rss = RSS.Load(xml);
            _rss = rss;
            var arr = new ArrayAdapter<RSSItem>(this, Android.Resource.Layout.SimpleListItem1, rss.channel.Items);
            lv.Adapter = arr;
            lv.ItemClick += Lv_ItemClick;
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            textUrl.Text = _rss.channel.Items[e.Position].link;
        }

        private void Lv_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            textUrl.Text = lv.SelectedItemPosition.ToString();
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

}

