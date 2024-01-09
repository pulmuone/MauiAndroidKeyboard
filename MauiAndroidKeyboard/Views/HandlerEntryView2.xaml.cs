using CommunityToolkit.Maui.Core.Platform;
using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView2 : ContentPage
{
    private HandlerEntry2 _currentEntry;

    public HandlerEntryView2()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
    {
        if (this._currentEntry != null)
        {
            if (this._currentEntry.IsSoftInputShowing())
            {
                this._currentEntry.HideKeyboard();
            }
            else
            {
                this._currentEntry.ShowKeyboard();
            }
        }
    }

    private void UserIDEntry_Focused(object sender, FocusEventArgs e)
    {
        this._currentEntry = sender as HandlerEntry2;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        this._currentEntry = sender as HandlerEntry2;
    }

    private void OnContentPageUnloaded(object sender, EventArgs e)
    {
        UserIDEntry.Handler?.DisconnectHandler();
        PasswordEntry.Handler?.DisconnectHandler();
    }

    private void UserIDEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (this._currentEntry != null)
        {
            if (this._currentEntry.IsSoftInputShowing())
            {
                this._currentEntry?.ClearFocus();
            }
        }
    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (this._currentEntry != null)
        {
            if (this._currentEntry.IsSoftInputShowing())
            {
                this._currentEntry?.ClearFocus();
            }
        }
    }
}