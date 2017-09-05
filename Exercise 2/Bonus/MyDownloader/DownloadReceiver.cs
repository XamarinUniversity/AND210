using System;
using Android.Content;

namespace MyDownloader
{
    class DownloadReceiver : BroadcastReceiver
    {
        public event EventHandler<EventArgs> DownloadComplete;
        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.GetBooleanExtra("DownloadComplete", false))
                DownloadComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}