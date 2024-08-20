using MauiAndroidKeyboard.Controls;
using Microsoft.Maui;
using System.Diagnostics;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView2 : ContentPage
{
    bool IsSoftInputShowing;
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

        SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;
    }

    private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
    {
        IsSoftInputShowing = e.IsVisible;
        Debug.WriteLine($"KeyBoard is visible : {(e.IsVisible ? "Yes" : "No")}");
    }

    private async void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
    {
        if (this._currentEntry != null)
        {
            if (this.IsSoftInputShowing)
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

        UserIDEntry.Text = "Å×½ºÆ®";
    }

    void BtnHidden_Clicked(System.Object sender, System.EventArgs e)
    {
        this._currentEntry.HideKeyboard();
    }

    private void _page_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        Debug.WriteLine("_page_NavigatedTo");
    }

    private void _page_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        Debug.WriteLine("_page_NavigatedFrom");
    }

    private void _page_NavigatingFrom(object sender, NavigatingFromEventArgs e)
    {
        Debug.WriteLine("_page_NavigatingFrom");
    }

    private void _page_Loaded(object sender, EventArgs e)
    {
        Debug.WriteLine("_page_Loaded");
    }

}