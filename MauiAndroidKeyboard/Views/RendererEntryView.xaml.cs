using CommunityToolkit.Maui.Core.Platform;
using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class RendererEntryView : ContentPage
{
    private ExtendedEntry _currententry;
    public RendererEntryView()
    {
        InitializeComponent();
    }

    private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
    {
        if (_currententry != null)
        {
            if (this._currententry.IsSoftKeyboardShowing())
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
        _currententry = sender as ExtendedEntry;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as ExtendedEntry;
    }

    private void UserIDEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (_currententry != null)
        {
            if (this._currententry.IsSoftKeyboardShowing())
            {
                _currententry?.ClearFocus();
            }
        }
    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (_currententry != null)
        {
            if (this._currententry.IsSoftKeyboardShowing())
            {
                _currententry?.ClearFocus();
            }
        }
    }
}