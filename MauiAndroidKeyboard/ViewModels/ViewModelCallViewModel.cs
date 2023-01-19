using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiAndroidKeyboard.ViewModels
{
    public class ViewModelCallViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }

        public ViewModelCallViewModel()
        {
            LoginCommand = new Command<object>((obj) => Login(obj), (obj) => IsControlEnable);
        }

        private void Login(object obj)
        {

            //ViewModel에서 View(UI) Control에 접근해서 컨트롤 하는 방법.
            Entry userIDEntry = ((Entry)((ContentPage)obj).FindByName("UserIDEntry"));
            Label messageLabel = ((Label)((ContentPage)obj).FindByName("MessageLabel"));


            userIDEntry.Text = "ViewMode에서 View UI에 접근";
            messageLabel.Text = "컨트롤의 특정 함수는 속성 접근해서 사용";


        }

    }
}
