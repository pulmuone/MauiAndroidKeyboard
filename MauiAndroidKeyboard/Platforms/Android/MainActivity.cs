using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

namespace MauiAndroidKeyboard;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    static string[] PERMISSIONS = {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
        };


    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);

        //Platform.CurrentActivity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(new MauiAndroidKeyboard.Platforms.Android.Services.SoftKeyboardService());

        if (Build.VERSION.SdkInt >= BuildVersionCodes.M && Build.VERSION.SdkInt < BuildVersionCodes.Tiramisu) //23이상부터
        {
            ActivityCompat.RequestPermissions(this, PERMISSIONS, 0);
        }
    }

    //protected override void OnResume()
    //{
    //    base.OnResume();

    //    Microsoft.Maui.ApplicationModel.Platform.OnResume(this);
    //}

    //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    //{
    //    Microsoft.Maui.ApplicationModel.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    //}
}
