using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using CoreTweet;

namespace sgFavo
{
    [Activity(Label = "sgFavo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        // ここは消しておくこと!!! 
        // MACアドレスで暗号化するとか。
        const string ApiKey = "API_KEY";
        const string ApiSecret = "API_SECRET";
        const string AccessToken = "ACCESS_TOKEN";
        const string AccessTokenSecret = "ACCESS_TOKEN_SECRET";

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
            var tokens = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToken, AccessTokenSecret);
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
                view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }
            var it = _items[position];
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = it.User.Name;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = it.Text;

            return view;
        }
    }
}

