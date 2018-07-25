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
	[Service(Label = "MyDownloadService", Icon = "@drawable/Icon")]
	class MyDownloadService : Service
	{
		const string tag = "MyDownloadService";

		const int NotificationID = 10000;

		volatile bool isCancelled;
		volatile bool isDownloaded;

		PendingIntent pendingIntent;

		public override void OnCreate()
		{
			base.OnCreate();
			Log.Debug(tag, "Service created");

			pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);
		}

		public override IBinder OnBind(Intent intent)
		{
			Log.Debug(tag, "OnBind called");

			throw new NotImplementedException();
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			Log.Debug(tag, "OnStartCommand called");

			StartForeground(NotificationID, GetNotification("Download started"));

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
					UpdateNotification(msg);

					Thread.Sleep(500);
				}

				if (isCancelled == false)
				{
					isDownloaded = true;
					StopSelf();
				}
			});

			return StartCommandResult.RedeliverIntent;
		}

		void UpdateNotification(string content)
		{
			var notification = GetNotification(content);

			NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(NotificationID, notification);
		}

		Notification GetNotification(string content)
		{
			// Beginning with API level 26 (Oreo) all notifications must be assigned to a channel (aka: category), otherwise they won't be shown.
			if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
			{
				const string notificationChannelId = "DOWNLOAD_CHANNEL";
				var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
				var notificationChannel = new NotificationChannel(notificationChannelId, "Downloads", NotificationImportance.Low);
				notificationManager.CreateNotificationChannel(notificationChannel);

				return new Notification.Builder(this, notificationChannelId)
				.SetContentTitle(tag)
				.SetContentText(content)
				.SetSmallIcon(Resource.Drawable.icon)
				.SetContentIntent(pendingIntent)
				.Build();
			}
			else
			{
				// Running on a device older than Oreo.
				return new Notification.Builder(this)
				.SetContentTitle(tag)
				.SetContentText(content)
				.SetSmallIcon(Resource.Drawable.icon)
				.SetContentIntent(pendingIntent)
				.Build();
			}
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