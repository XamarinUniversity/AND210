using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace MyDownloader
{
    [Service (Label = "MyDownloadService", Icon = "@drawable/Icon") ] 
    class MyDownloadService : Service 
    {
        const string tag = "MyDownloadService";

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

            return StartCommandResult.RedeliverIntent;
        }

        public override void OnDestroy()
        {
            Log.Debug(tag, "Service destroyed");
        }
    }
}