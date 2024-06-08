using MauiAndroidKeyboard.Controls;
using Microsoft.Maui;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView2 : ContentPage
{
    private HandlerEntry2 _currentEntry;

    public HandlerEntryView2()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(300);

        UserIDEntry.Focus();
    }

    private async void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
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

    void BtnShow_Clicked(System.Object sender, System.EventArgs e)
    {
        this._currentEntry.ShowKeyboard();
    }

    void BtnHidden_Clicked(System.Object sender, System.EventArgs e)
    {
        this._currentEntry.HideKeyboard();
    }
}