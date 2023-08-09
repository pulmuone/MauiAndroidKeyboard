using CommunityToolkit.Maui.Core.Platform;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using System.Diagnostics;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView2 : ContentPage
{
	private HandlerEntry2 _currententry;
	private ISoftwareKeyboardService _keyboardService;
	public HandlerEntryView2()
	{
		InitializeComponent();

		//_keyboardService = DependencyService.Get<ISoftwareKeyboardService>();

	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		{

		}

		this.UserIDEntry.Focus();
    }

    private void ToolbarItemKeyboard_Clicked(object sender, EventArgs e)
	{
		if (_currententry != null)
		{
            if (_currententry.IsSoftKeyboardShowing())
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