using CommunityToolkit.Maui.Core.Platform;
using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView4 : ContentPage
{
	private HandlerEntry4 _currententry;
	public HandlerEntryView4()
	{
		InitializeComponent();
	}

	private void pg_Unloaded(object sender, EventArgs e)
	{
		_currententry?.Handler?.DisconnectHandler();
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
}