using MauiAndroidKeyboard.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Interfaces
{
    public interface ISoftwareKeyboardService
    {
        event EventHandler<SoftwareKeyboardEventArgs> KeyboardHeightChanged;
        bool IsKeyboardVisible { get; }
    }
}
