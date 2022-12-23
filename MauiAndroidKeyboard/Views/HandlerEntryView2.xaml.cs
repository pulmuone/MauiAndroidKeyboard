using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView2 : ContentPage
{
	private HandlerEntry2 _currententry;

	public HandlerEntryView2()
	{
		InitializeComponent();

	}

	private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
	{
		if (_currententry != null)
		{
			if (_currententry.SoftKeyboardViewStatus == SoftKeyboardViewStatus.SHOW)
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
		_currententry = sender as HandlerEntry2;
	}

	private void PasswordEntry_Focused(object sender, FocusEventArgs e)
	{
		_currententry = sender as HandlerEntry2;
	}
}