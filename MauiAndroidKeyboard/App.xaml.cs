using MauiAndroidKeyboard.Views;

namespace MauiAndroidKeyboard;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new NavigationPage(new MainView());

	}
}
