using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sgQuestionnaire
{
    [Activity(Label = "sgQuestionnaire", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // コントロール
        EditText editName;
        EditText editMemo;
        Spinner spJoin;
        RadioButton radioJoin1, radioJoin2, radioJoin3;
        List<Horo> Horos;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            editName = FindViewById<EditText>(Resource.Id.editName);
            editMemo = FindViewById<EditText>(Resource.Id.editMemo);
            spJoin = FindViewById<Spinner>(Resource.Id.spinner1);
            radioJoin1 = FindViewById<RadioButton>(Resource.Id.radioJoin1);
            radioJoin2 = FindViewById<RadioButton>(Resource.Id.radioJoin2);
            radioJoin3 = FindViewById<RadioButton>(Resource.Id.radioJoin3);

            Button btnPost = FindViewById<Button>(Resource.Id.buttonPost);
            btnPost.Click += BtnPost_Click;

            // 星座の一覧を登録
            Horos = new List<Horo>();
            Horos.Add(new Horo() { Id = "", Text = "選択してください" });
            Horos.Add(new Horo() { Id = "Aries", Text = "おひつじ座" });
            Horos.Add(new Horo() { Id = "Taurus", Text = "おうし座" });
            Horos.Add(new Horo() { Id = "Gemini", Text = "ふたご座" });
            Horos.Add(new Horo() { Id = "Cancer", Text = "かに座" });
            Horos.Add(new Horo() { Id = "Leo", Text = "しし座" });
            Horos.Add(new Horo() { Id = "Virgo", Text = "おとめ座" });
            Horos.Add(new Horo() { Id = "Libra", Text = "てんびん座" });
            Horos.Add(new Horo() { Id = "Scorpio", Text = "さそり座" });
            Horos.Add(new Horo() { Id = "Saggitarius", Text = "いて座" });
            Horos.Add(new Horo() { Id = "Capricorn", Text = "やぎ座" });
            Horos.Add(new Horo() { Id = "Aquarius", Text = "みずがめ座" });
            Horos.Add(new Horo() { Id = "Pisces", Text = "うお座" });
            var ad = new ArrayAdapter<Horo>(this, Android.Resource.Layout.SimpleListItem1, Horos);
            spJoin.Adapter = ad;
        }

        /// <summary>
        /// 投稿ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnPost_Click(object sender, System.EventArgs e)
        {
            // 画面からデータを取得
            string name = editName.Text;
            string memo = editMemo.Text;
            if (spJoin.SelectedItemPosition == 0)
            {
                new AlertDialog.Builder(this)
                   .SetMessage("星座を選択してください")
                   .Show();
                return;
            }
            string horo = Horos[spJoin.SelectedItemPosition].Id;
            string join = "1";
            if (radioJoin1.Checked) join = "1";
            if (radioJoin2.Checked) join = "2";
            if (radioJoin3.Checked) join = "3";

            var html = await Go(name, join, horo, memo);
            // 結果を表示
            new AlertDialog.Builder(this)
               .SetMessage(html)
               .Show();
            return;
        }

        async Task<string> Go(string name, string join, string horo, string memo)
        {
            var hc = new HttpClient();
            var dic = new Dictionary<string, string>();
            dic["name"] = name;
            dic["join"] = join;
            dic["horoscope"] = horo;
            dic["memo"] = memo;
            var cont = new FormUrlEncodedContent(dic);
            // 呼び出しのIPアドレスは変更すること
            var url = "http://172.16.0.11/svQuest/post.php";
            // .NET Core のサンプルの場合
            // var url = "http://172.16.0.11:5000/Quest/Create";
            var req = await hc.PostAsync(url, cont);
            var html = await req.Content.ReadAsStringAsync();
            return html;
        }
    }
    public class Horo
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return this.Text;
        }
    }
}

