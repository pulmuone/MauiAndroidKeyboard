using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class MainView : ContentPage
{
	private HandlerEntry _currententry;
	public MainView()
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


	private void HandlerEntry_Focused(object sender, FocusEventArgs e)
	{
		_currententry = sender as HandlerEntry;
	}

	private void HandlerEntry_Completed(object sender, EventArgs e)
	{
		Console.WriteLine("aa");
	}

	private void HandlerEntry_HandlerChanged(object sender, EventArgs e)
	{
		Console.WriteLine("b");
	}
}