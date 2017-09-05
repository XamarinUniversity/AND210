using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MyDownloader
{
    [Activity(Label = "MyDownloader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        DownloadReceiver receiver;

        ProgressBar progressBar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            FindViewById<Button>(Resource.Id.buttonStart).Click += ButtonStartClick;
            FindViewById<Button>(Resource.Id.buttonCancel).Click += ButtonCancelClick;
        }

        protected override void OnResume()
        {
            base.OnResume();

            var filter = new IntentFilter("DownloadServiceFilter");
            filter.AddAction("DownloadComplete");

            receiver = new DownloadReceiver();
            receiver.DownloadComplete += ReceiverDownloadComplete;

            RegisterReceiver(receiver, filter);
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (receiver != null)
            {
                receiver.DownloadComplete -= ReceiverDownloadComplete;
                UnregisterReceiver(receiver);
            }
        }

        private void ReceiverDownloadComplete(object sender, System.EventArgs e)
        {
            progressBar.Indeterminate = false;
        }

        void ButtonStartClick(object sender, System.EventArgs e)
        {
            progressBar.Indeterminate = true;
		   
		    var intent = new Intent(this, typeof(MyDownloadService));
            intent.PutExtra("LoopCount", 6);

            StartService(intent);
        }
		
        void ButtonCancelClick(object sender, System.EventArgs e)
        {
            progressBar.Indeterminate = false;

            StopService(new Intent(this, typeof(MyDownloadService)));
        }
    }
}