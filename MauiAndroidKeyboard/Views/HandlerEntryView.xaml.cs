using CommunityToolkit.Maui.Core.Platform;
using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView : ContentPage
{

	private HandlerEntry _currententry;

	private CustomEntry _customEntry;
	public HandlerEntryView()
	{
		InitializeComponent();
	}

	private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
	{
		if (_customEntry != null)
		{
			if (this._customEntry.IsSoftKeyboardShowing())
			{
				_customEntry.HideKeyboard();
			}
			else
			{
				_customEntry.ShowKeyboard();
			}
		}

		//if (_currententry != null)
		//{
		//	if (_currententry.SoftKeyboardViewStatus == SoftKeyboardViewStatus.SHOW)
		//	{
		//		_currententry.HideKeyboard();
		//	}
		//	else
		//	{
		//		_currententry.ShowKeyboard();
		//	}
		//}
	}

	private void UserIDEntry_Focused(object sender, FocusEventArgs e)
	{
		_currententry = sender as HandlerEntry;
	}

	private void PasswordEntry_Focused(object sender, FocusEventArgs e)
	{
		_currententry = sender as HandlerEntry;
	}

	private void pg_Unloaded(object sender, EventArgs e)
	{
		UserNameEntry.Handler?.DisconnectHandler();
	}

	private void UserNameEntry_Focused(object sender, FocusEventArgs e)
	{
		_customEntry = sender as CustomEntry;	
	}
}