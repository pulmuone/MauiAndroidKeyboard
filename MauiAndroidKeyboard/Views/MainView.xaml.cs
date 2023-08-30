using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Helpers;
using MauiAndroidKeyboard.Resources.Localization;
using System.Globalization;

namespace MauiAndroidKeyboard.Views;

public partial class MainView : ContentPage
{
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