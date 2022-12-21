using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Interfaces
{
    public interface ICustomEntry : IView
    {
        public void ShowKeyboard();
        public void HideKeyboard();

    }
}
