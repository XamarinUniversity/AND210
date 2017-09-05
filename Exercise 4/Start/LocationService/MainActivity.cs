using Android.App;
using Android.Widget;
using Android.OS;

namespace LocationService
{
    [Activity(Label = "LocationService", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.buttonLocation).Click += (s, e) => StartActivity(typeof(LocationActivity));
        }
    }
}