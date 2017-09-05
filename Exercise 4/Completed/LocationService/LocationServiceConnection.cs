using Android.Content;
using Android.OS;
using System;

namespace LocationService
{
    class LocationServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public event EventHandler<bool> ServiceConnectionChanged;
        public LocationService Service { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            LocationServiceBinder lsBinder = service as LocationServiceBinder;

            if (lsBinder == null)
                return;

            Service = lsBinder.Service;

            ServiceConnectionChanged?.Invoke(this, true);
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            ServiceConnectionChanged?.Invoke(this, false);
            Service = null;
        }
    }
}