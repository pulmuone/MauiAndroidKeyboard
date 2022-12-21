using MauiAndroidKeyboard.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAndroidKeyboard.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        INavigation Navigation => Application.Current.MainPage.Navigation;
        public ICommand NavigateCommand { get; private set; }

        public MainViewModel() 
        {
            NavigateCommand = new Command<Type>(async (obj) => await ViewNavigate(obj), (obj) => IsControlEnable);
        }

        private async Task ViewNavigate(Type type)
        {
            IsControlEnable = false;
            IsBusy = true;
            (NavigateCommand as Command).ChangeCanExecute();

            Page page = (Page)Activator.CreateInstance(type);
            await Navigation.PushAsync(page);

            IsBusy = false;
            IsControlEnable = true;
            (NavigateCommand as Command).ChangeCanExecute();
        }
    }
}
