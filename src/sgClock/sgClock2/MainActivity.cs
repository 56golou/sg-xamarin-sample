using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;


namespace sgClock2
{
    [Activity(Label = "sgClock2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView text1;
        ImageView image1;
        Task _timer;
        bool timeFormat = true;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            text1 = FindViewById<TextView>(Resource.Id.textView1);
            image1 = FindViewById<ImageView>(Resource.Id.imageView1);

            image1.Click += Image1_Click;


            int[] bmps = new int[] {
            Resource.Drawable.sg400_1,
            Resource.Drawable.sg400_2,
            Resource.Drawable.sg400_3,
            Resource.Drawable.sg400_4,
            Resource.Drawable.sg400_5,
        };
            int index = -1;

            _timer = new Task(async () =>
            {
                while (true)
                {
                    RunOnUiThread(() =>
                    {
                        if (timeFormat == true)
                        {
                            text1.Text = DateTime.Now.ToString("HH:mm:ss");
                        }
                        else
                        {
                            text1.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                    // 画像を切り替える
                    if (DateTime.Now.Second % 10 == 0)
                        {
                            index++;
                            if (index >= bmps.Length)
                                index = 0;

                            image1.SetImageResource(bmps[index]);
                        }
                    });
                    await Task.Delay(1000);
                }
            });
            _timer.Start();
        }

        private void Image1_Click(object sender, EventArgs e)
        {
            if (timeFormat == true)
            {
                timeFormat = false;
            }
            else
            {
                timeFormat = true;
            }
        }
    }
}

