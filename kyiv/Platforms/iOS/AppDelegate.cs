using Foundation;
using Plugin.MauiMTAdmob;
using UIKit;

namespace kyiv;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        CrossMauiMTAdmob.Current.Init(
            "ca-app-pub-3940256099942544~1458002511"
        );

        return base.FinishedLaunching(app, options);
    }

}

