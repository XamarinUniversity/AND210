using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace LocationService
{
    [Activity(Label = "LocationActivity")]
    public class LocationActivity : Activity
    {
        LocationServiceConnection lsConnection;

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

            lsConnection = new LocationServiceConnection();
            lsConnection.ServiceConnectionChanged += ServiceConnectionChanged;
        }

        void ServiceConnectionChanged(object sender, bool isConnected)
        {
            if (lsConnection.Service == null)
                return;

            if (isConnected)
            {
                lsConnection.Service.LocationChanged += LocationChanged;
                startText.Text = lsConnection.Service.StartTime.ToLongTimeString();
            }
            else
            {
                lsConnection.Service.LocationChanged -= LocationChanged;
            }
        }

        protected override void OnResume()
        {
            var intent = new Intent(this, typeof(LocationService));
            BindService(intent, lsConnection, Bind.AutoCreate);

            base.OnResume();
        }

        protected override void OnPause()
        {
            UnbindService(lsConnection);

            base.OnPause();
        }

        void LocationChanged(object sender, Android.Locations.LocationChangedEventArgs e)
        {
            var location = e.Location;

            latText.Text =   location.Latitude.ToString();
            lngText.Text =   location.Longitude.ToString();
            altText.Text =   location.Altitude.ToString();
            speedText.Text = location.Speed.ToString();
        }

        void ClearScreen ()
        {
            latText.Text = lngText.Text = altText.Text = speedText.Text = string.Empty;
        }
    }
}