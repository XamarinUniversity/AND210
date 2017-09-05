using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Threading;
using Android.Util;
using Android.Widget;
using System.Threading.Tasks;

namespace MyDownloader
{
    [Service (Label = "MyDownloadService", Icon = "@drawable/Icon") ] 
    class MyDownloadService : Service
    {
        const string tag = "MyDownloadService";

        volatile bool isCancelled;
        volatile bool isDownloaded;
        
        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug(tag, "Service created");
        }

        public override IBinder OnBind(Intent intent)
        {
            Log.Debug(tag, "OnBind called");

            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(tag, "OnStartCommand called");
			
			isDownloaded = false;
            isCancelled = false;

            Toast.MakeText(this, "Download Started", ToastLength.Short).Show();

            var steps = intent.GetIntExtra("LoopCount", 10);

            Task.Run(() =>
            {
                for (int i = 0; i < steps && isCancelled == false; i++)
                {
                    int percent = 100 * (i + 1) / steps;
                    var msg = String.Format("[{0}] download in progress: {1}% complete", startId, percent);
                    Log.Debug(tag, msg);

                    Thread.Sleep(500);
                }

                if (isCancelled == false)
                {
                    isDownloaded = true;
                    StopSelf();

                    Intent broadcast = new Intent();
                    broadcast.SetAction("DownloadServiceFilter");
                    broadcast.PutExtra("DownloadComplete", true);
                    SendBroadcast(broadcast);
                }
            });

            return StartCommandResult.RedeliverIntent;
        }

        public override void OnDestroy()
        {
            isCancelled = true;

            if (isDownloaded)
                Toast.MakeText(this, "Download Complete", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Download Cancelled", ToastLength.Long).Show();

            Log.Debug(tag, "Service destroyed");
        }
    }
}