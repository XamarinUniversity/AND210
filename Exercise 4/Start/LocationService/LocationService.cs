using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Locations;

namespace LocationService
{
    [Service]
    public class LocationService : Service
    {
        LocationHelper locationHelper;

        public event EventHandler<LocationChangedEventArgs> LocationChanged
        {
            add { locationHelper.LocationChanged += value; }
            remove { locationHelper.LocationChanged -= value; }
        }

        public DateTime StartTime { get; private set; } = DateTime.Now;

        public override void OnCreate()
        {
            locationHelper = new LocationHelper();

            locationHelper.StartLocationUpdates();

            base.OnCreate();
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            locationHelper.StopLocationUpdates();
        }
    }
}