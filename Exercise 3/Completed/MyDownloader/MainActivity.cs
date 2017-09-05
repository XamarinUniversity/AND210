using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MyDownloader
{
    [Activity(Label = "MyDownloader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.buttonStart).Click += ButtonStartClick;
            FindViewById<Button>(Resource.Id.buttonCancel).Click += ButtonCancelClick;
        }

        void ButtonStartClick(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(MyDownloadService));
            intent.PutExtra("LoopCount", 7);

            StartService(intent);
        }

        void ButtonCancelClick(object sender, System.EventArgs e)
        {
            StopService(new Intent(this, typeof(MyDownloadService)));
        }
    }
}