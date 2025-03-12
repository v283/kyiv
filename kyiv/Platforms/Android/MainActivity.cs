using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace kyiv;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);


        // to adjust keybord for ai chat
        Window.SetSoftInputMode(SoftInput.AdjustResize);
    }

    protected override void OnNewIntent(Android.Content.Intent intent)
    {
        base.OnNewIntent(intent);
        if (intent.Data != null)
        {
            string deepLinkUrl = intent.Data.ToString();
            Shell.Current.GoToAsync(deepLinkUrl); // Navigate in the app
        }
    }

}

