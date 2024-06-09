using Foundation;
using UIKit;

namespace MauiAndroidKeyboard;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        UIKeyboard.Notifications.ObserveDidShow(OnKeyboardDidShow);
        UIKeyboard.Notifications.ObserveDidHide(OnKeyboardDidHide);

        return base.FinishedLaunching(app, options);
    }

    private void OnKeyboardDidHide(object sender, UIKeyboardEventArgs e)
    {
        SoftKeyboard.Current.InvokeVisibilityChanged(false);
    }

    private void OnKeyboardDidShow(object sender, UIKeyboardEventArgs e)
    {
        SoftKeyboard.Current.InvokeVisibilityChanged(true);
    }
}
