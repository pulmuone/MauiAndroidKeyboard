using MauiAndroidKeyboard.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAndroidKeyboard.ViewModels
{
    public class HandlerEntryViewModel : BaseViewModel
    {
        public ICommand UserIDCompletedCommand { get; private set; }
        public ICommand PasswordCompletedCommand { get; private set; }
        public string _userID;
        public string _password;

        public HandlerEntryViewModel()
        {
            UserIDCompletedCommand = new Command<object>(async (obj) => await UserIDCompleted(obj), (obj) => IsControlEnable);
            PasswordCompletedCommand = new Command<object>(async (obj) => await PasswordCompleted(obj), (obj) => IsControlEnable);
        }

        private async Task UserIDCompleted(object obj)
        {
            IsControlEnable = false;
            IsBusy = true;
            (UserIDCompletedCommand as Command).ChangeCanExecute();

            HandlerEntry entry = ((HandlerEntry)((ContentPage)obj).FindByName("PasswordEntry"));
            entry.IsEnabled = false;
            entry.IsEnabled = true;
            entry.CursorPosition = 0;
            entry.SelectionLength = entry.Text != null ? entry.Text.Length : 0;
            entry.Focus();

            //ToDo


            IsControlEnable = true;
            IsBusy = false;
            (UserIDCompletedCommand as Command).ChangeCanExecute();
        }

        private async Task PasswordCompleted(object obj)
        {
            IsControlEnable = false;
            IsBusy = true;
            (PasswordCompletedCommand as Command).ChangeCanExecute();

            HandlerEntry entry = ((HandlerEntry)((ContentPage)obj).FindByName("UserIDEntry"));
            entry.IsEnabled = false;
            entry.IsEnabled = true;
            entry.CursorPosition = 0;
            entry.SelectionLength = entry.Text != null ? entry.Text.Length : 0;
            entry.Focus();

            //ToDo


            IsControlEnable = true;
            IsBusy = false;
            (PasswordCompletedCommand as Command).ChangeCanExecute();
        }

        public string UserID
        {
            get => this._userID;
            set => SetProperty(ref this._userID, value);
        }

        public string Password
        {
            get => this._password;
            set => SetProperty(ref this._password, value);
        }
    }
}
