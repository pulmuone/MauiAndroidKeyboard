using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Controls
{
    public class SoftwareKeyboardEventArgs : EventArgs
    {
        public SoftwareKeyboardEventArgs(int keyboardheight)
        {
            KeyboardHeight = keyboardheight;
        }

        public int KeyboardHeight { get; private set; }
    }
}
