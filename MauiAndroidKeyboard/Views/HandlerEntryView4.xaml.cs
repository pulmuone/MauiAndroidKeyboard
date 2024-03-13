using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView4 : ContentPage
{
    private HandlerEntry4 _currententry;
    public HandlerEntryView4()
    {
        InitializeComponent();
    }

    private void OnContentPageUnloaded(object sender, EventArgs e)
    {
        UserIDEntry.Handler?.DisconnectHandler();
        PasswordEntry.Handler?.DisconnectHandler();
    }

    private void UserIDEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as HandlerEntry4;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as HandlerEntry4;
    }

    private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
    {
        if (_currententry != null)
        {
            if (this._currententry.IsSoftInputShowing())
            {
                _currententry?.HideKeyboard();
            }
            else
            {
                _currententry?.ShowKeyboard();
            }
        }
    }

    private void UserIDEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (_currententry != null)
        {
            if (this._currententry.IsSoftInputShowing())
            {
                _currententry?.ClearFocus();
            }
        }
    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (_currententry != null)
        {
            if (this._currententry.IsSoftInputShowing())
            {
                _currententry?.ClearFocus();
            }
        }
    }
}