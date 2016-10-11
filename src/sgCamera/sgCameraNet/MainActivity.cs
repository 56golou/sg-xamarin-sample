using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
using Android.Graphics;
using Android.Provider;

namespace sgCameraNet
{
    /* できるだけ、System.IO のような標準ライブラリを使う */


    [Activity(Label = "sgCamera", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public string _file;
        public string _dir;
        public Android.Graphics.Bitmap bitmap;

        ImageView image1;

        /* refer to
         * Take a Picture and Save Using Camera App - Xamarin
         * https://developer.xamarin.com/recipes/android/other_ux/camera_intent/take_a_picture_and_save_using_camera_app/
         */

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            image1 = FindViewById<ImageView>(Resource.Id.imageView1);
            var btn = FindViewById<Button>(Resource.Id.myButton);
            btn.Click += Btn_Click;
            // フォルダ作成
            CreateDirectoryForPictures();
        }

        /// <summary>
        /// ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            StartActivityForResult(intent, 0);
        }

        /// <summary>
        /// 保存フォルダを作成
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            _dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }
        /*
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        */

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume to much memory
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = image1.Height;
            bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            if (bitmap != null)
            {
                image1.SetImageBitmap(bitmap);
                bitmap = null;
            }
            // Dispose of the Java side bitmap.
            GC.Collect();
        }
    }
    public static class BitmapHelpers
    {
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }
    }
}

