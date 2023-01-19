using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Helpers;
using MauiAndroidKeyboard.Resources.Localization;
using System.Globalization;

namespace MauiAndroidKeyboard.Views;

public partial class MainView : ContentPage
{
	private HandlerEntry _currententry;
	public MainView()
	{
		InitializeComponent();
	}



	protected override void OnAppearing()
	{
		base.OnAppearing();


		//((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Transparent;

		this.LabelVerison.Text = String.Format("{0} {1}", "Ver ", Helpers.VersionCheck.Instance.GetVersionClient().ToString());
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

	private async void AutoUpdateButton_Clicked(object sender, EventArgs e)
	{
		if (VersionCheck.Instance.IsNetworkAccess())
		{
			if (await VersionCheck.Instance.IsUpdate())
			{
				await VersionCheck.Instance.UpdateCheck();

				return;
			}
		}
	}

	private void Translate_Clicked(object sender, EventArgs e)
	{
		LocalizationResourceManager.Current.CurrentCulture = CultureInfo.GetCultureInfo("ko"); //ko, en
	}    
}