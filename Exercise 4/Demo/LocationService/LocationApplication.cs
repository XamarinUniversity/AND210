using Android.App;
using Android.Content;
using Android.Runtime;
using System;

namespace LocationService
{
    [Application]
    class LocationApplication : Application
    {
        public LocationApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            StartService(new Intent(this, typeof(LocationService)));
        }

        public override void OnTerminate()
        {
            StopService(new Intent(this, typeof(LocationService)));
        }
    }
}