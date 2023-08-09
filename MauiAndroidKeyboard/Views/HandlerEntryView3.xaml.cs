using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView3 : ContentPage
{
    private HandlerEntry3 _currententry;

    public HandlerEntryView3()
    {
        InitializeComponent();
    }


    private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
    {
        if (_currententry != null)
        {
            if (_currententry.OnIsKeyboardShowing())
            {
                _currententry.HideKeyboard();
            }
            else
            {
                _currententry.ShowKeyboard();
            }
        }
    }

    private void UserIDEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as HandlerEntry3;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as HandlerEntry3;
    }
}