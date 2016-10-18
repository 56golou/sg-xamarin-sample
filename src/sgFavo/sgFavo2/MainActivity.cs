using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using CoreTweet;
using System.Net.Http;
using System.Threading.Tasks;

namespace sgFavo2
{
    [Activity(Label = "sgFavo2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        // ここは消しておくこと!!! 
        // MACアドレスで暗号化するとか。
        const string ApiKey = "";
        const string ApiSecret = "";
        const string AccessToke = "";
        const string AccessTokeSecret = "";

        TextView text1;
        ListView lv1;
        Button button1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            text1 = FindViewById<TextView>(Resource.Id.text1);
            lv1 = FindViewById<ListView>(Resource.Id.lv1);
            button1 = FindViewById<Button>(Resource.Id.button1);

            button1.Click += Button1_Click;

        }

        /// <summary>
        /// 更新ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button1_Click(object sender, EventArgs e)
        {


            var tokens = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);
            var favs = await tokens.Favorites.ListAsync();


            var items = new List<Status>();
            foreach (var it in favs)
            {
                items.Add(it);
            }

            // var arr = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lst);
            var ad = new StatusAdapter(this, items);
            lv1.Adapter = ad;
            text1.Text = $"Count: {items.Count}";

            return;
        }
    }

    public class StatusAdapter : BaseAdapter<CoreTweet.Status>
    {

        Activity _activity;
        List<CoreTweet.Status> _items;

        public StatusAdapter(Activity act, List<CoreTweet.Status> items)
        {
            _activity = act;
            _items = items;
        }

        public override Status this[int position]
        {
            get
            {
                return _items[position];
            }
        }

        public override int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = _activity.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);
            }
            var it = _items[position];
            view.FindViewById<TextView>(Resource.Id.Text1).Text = it.User.Name;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = it.Text;
            // 画像は非同期で表示する
            var t = getImage(it.User.ProfileImageUrl);
            t.ContinueWith((e) => {
                view.FindViewById<ImageView>(Resource.Id.Image).SetImageBitmap(e.Result);
            });

            return view;
        }
        private async Task<Android.Graphics.Bitmap> getImage(string url)
        {
            var hc = new HttpClient();
            using (var st = await hc.GetStreamAsync(url))
            {
                var bmp = await Android.Graphics.BitmapFactory.DecodeStreamAsync(st);
                return bmp;
            }
        }
    }
}

