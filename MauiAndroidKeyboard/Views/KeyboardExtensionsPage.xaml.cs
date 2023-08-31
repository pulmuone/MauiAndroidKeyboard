using CommunityToolkit.Maui.Core.Platform;

namespace MauiAndroidKeyboard.Views;

public partial class KeyboardExtensionsPage : ContentPage
{
    private CancellationTokenSource _source = new();
    private Entry _currententry;

    public KeyboardExtensionsPage()
    {
        InitializeComponent();
    }

    private void UserIDEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as Entry;

#if IOS || MACCATALYST
        // Currently .NET MAUI will auto close the keyboard on iOS if you click outside of the entry
        // This causes the examples on this page to behave oddly, because the keybord auto closes if the user
        // taps the "hide keyboard" or "check keyboard status" buttons.
        // We're going to remove this as the default behavior in .NET 8 and then users can re-enable it via
        // https://github.com/CommunityToolkit/Maui/issues/978
        // This code removes the TapGestureRecognizer used to detect and close the keyboard so that we can accurately
        // validate that these APIs work on iOS
        if (this?.Handler?.PlatformView is UIKit.UIView uiView &&
            uiView.GestureRecognizers is not null)
        {
            foreach (var gesture in uiView.GestureRecognizers)
            {
                if (gesture is UIKit.UITapGestureRecognizer gr)
                {
                    uiView.RemoveGestureRecognizer(gr);
                    break;
                }
            }
        }
#endif
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as Entry;
        _currententry.HideKeyboardAsync(_source.Token);

#if IOS || MACCATALYST
        // Currently .NET MAUI will auto close the keyboard on iOS if you click outside of the entry
        // This causes the examples on this page to behave oddly, because the keybord auto closes if the user
        // taps the "hide keyboard" or "check keyboard status" buttons.
        // We're going to remove this as the default behavior in .NET 8 and then users can re-enable it via
        // https://github.com/CommunityToolkit/Maui/issues/978
        // This code removes the TapGestureRecognizer used to detect and close the keyboard so that we can accurately
        // validate that these APIs work on iOS
        if (this?.Handler?.PlatformView is UIKit.UIView uiView &&
            uiView.GestureRecognizers is not null)
        {
            foreach (var gesture in uiView.GestureRecognizers)
            {
                if (gesture is UIKit.UITapGestureRecognizer gr)
                {
                    uiView.RemoveGestureRecognizer(gr);
                    break;
                }
            }
        }
#endif
    }

    private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
    {

        if (_currententry != null)
        {
            if (this._currententry.IsSoftKeyboardShowing())
            {
                _currententry.HideKeyboardAsync(_source.Token);
            }
            else
            {
                _currententry.ShowKeyboardAsync(_source.Token);
            }
        }
    }

    private void UserIDEntry_Unfocused(object sender, FocusEventArgs e)
    {

    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {

    }
}