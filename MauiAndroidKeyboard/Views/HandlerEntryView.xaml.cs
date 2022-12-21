using MauiAndroidKeyboard.Controls;

namespace MauiAndroidKeyboard.Views;

public partial class HandlerEntryView : ContentPage
{

    private HandlerEntry _currententry;
    public HandlerEntryView()
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
        _currententry = sender as HandlerEntry;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        _currententry = sender as HandlerEntry;
    }
}