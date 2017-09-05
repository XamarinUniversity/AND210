using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace LocationService
{
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity
    {
        TextView latText, lngText, altText, speedText, startText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Location);

            latText = FindViewById<TextView>(Resource.Id.latText);
            lngText = FindViewById<TextView>(Resource.Id.lngText);
            altText = FindViewById<TextView>(Resource.Id.altText);
            speedText = FindViewById<TextView>(Resource.Id.speedText);
            startText = FindViewById<TextView>(Resource.Id.startText);

            ClearScreen();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        void ClearScreen ()
        {
            latText.Text = lngText.Text = altText.Text = speedText.Text = string.Empty;
        }
    }
}