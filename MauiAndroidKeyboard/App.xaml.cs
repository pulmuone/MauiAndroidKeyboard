using MauiAndroidKeyboard.Helpers;
using MauiAndroidKeyboard.Resources.Localization;
using MauiAndroidKeyboard.Views;
using System.Globalization;

namespace MauiAndroidKeyboard;

public partial class App : Application
{
	public App()
	{
		CultureInfo culture;


		//CurrentUICulture : ko
		//CurrentCulture : ko-kr
		if (string.IsNullOrEmpty(Settings.Language))
		{
			foreach (var item in LanguageSupport.Instance.LanguageListShort)
			{
				if (Thread.CurrentThread.CurrentUICulture.Name.StartsWith(item))
				{
					culture = Thread.CurrentThread.CurrentUICulture;
					Settings.Language = item;
					break;
				}
				else
				{
					Settings.Language = "en";
				}
			}
		}

		culture = new CultureInfo("en"); //ko, en

		LocalizationResourceManager.Current.PropertyChanged += (sender, e) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
		LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
		LocalizationResourceManager.Current.CurrentCulture = culture;

		InitializeComponent();

		VersionTracking.Track();

		//MainPage = new AppShell();
		MainPage = new NavigationPage(new MainView());

	}
}
