using Android.App;
using Android.OS;
using Android.Content;
using Android.Widget;
using System;
using Android.Provider;

namespace ShakeToLaunch
{
    [Activity(Label = "Shake to Launch", MainLauncher = true, Icon = "@drawable/ic_vibration_white_48dp")]
    public class MainActivity : Activity
    {
        RadioButton radioCamera, radioTimer, radioSettings;
        Button buttonStart, buttonStop;
        Switch switchNotification;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            buttonStart = FindViewById<Button>(Resource.Id.buttonStartService);
            buttonStop = FindViewById<Button>(Resource.Id.buttonStopService);

            buttonStart.Click += OnButtonStartService;
            buttonStop.Click += OnButtonStopService;

            radioCamera = FindViewById<RadioButton>(Resource.Id.radioCamera);
            radioTimer = FindViewById<RadioButton>(Resource.Id.radioTimer);
            radioSettings = FindViewById<RadioButton>(Resource.Id.radioSettings);
            switchNotification = FindViewById<Switch>(Resource.Id.swtichNotification);
        }

        void OnButtonStartService(object sender, EventArgs e)
        {
            string intentAction;
            string title;

            if (radioCamera.Checked)
            {
                intentAction = MediaStore.IntentActionStillImageCamera;
                title = "Camera";
            }
            else if (radioTimer.Checked)
            {
                intentAction = AlarmClock.ActionSetTimer;
                title = "Timer";
            }
            else
            {
                intentAction = Settings.ActionSettings;
                title = "Settings";
            }
             
            var intent = new Intent(this, typeof(ShakeToLaunchService));
            intent.PutExtra("Title", title);
            intent.PutExtra("Action", intentAction);
            intent.PutExtra("Notification", switchNotification.Checked);

            StartService(intent);

            buttonStart.Text = "Update Shake Service";
            buttonStop.Enabled = true;
        }

        void OnButtonStopService(object sender, EventArgs e)
        {
            StopService(new Intent(this, typeof(ShakeToLaunchService)));

            buttonStart.Text = "Start Shake Service";
            buttonStop.Enabled = false;
        }
    }
}