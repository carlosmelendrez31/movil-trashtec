using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;

namespace Trash_TecMovil
{
    [Activity(Theme = "@style/Maui.SplashTheme",
              MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize
                                   | ConfigChanges.Orientation
                                   | ConfigChanges.UiMode
                                   | ConfigChanges.ScreenLayout
                                   | ConfigChanges.SmallestScreenSize,
              Exported = true)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // 🔐 Pedir permisos si es Android 12+
            if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            {
                string[] permissions =
                {
            Android.Manifest.Permission.BluetoothScan,
            Android.Manifest.Permission.BluetoothConnect,
            Android.Manifest.Permission.AccessFineLocation
        };

                RequestPermissions(permissions, 0);
            }
        }
    }
}

