using Android.Content;
using Android.Views.InputMethods;
using Android.Widget;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace MauiAndroidKeyboard.Platforms.Android.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer, IVirtualKeyboard
    {
        public ExtendedEntryRenderer(Context context) : base(context)
        {

        }

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if ((e.OldElement == null) && (Control != null))
            {
                var edittext = (EditText)Control;

                //edittext.SetPadding(0, 0, 0, 0);
                edittext.SetTextIsSelectable(true);
                edittext.SetSelectAllOnFocus(true);
                edittext.ShowSoftInputOnFocus = false; //true: Keyboard Show, false: Keyboard Hidden
                edittext.SetSingleLine(true);

                var view = (ExtendedEntry)Element;

                view.VirtualKeyboardHandler = this;
            }
        }

        public void ShowKeyboard()
        {
            Control.RequestFocus();
            var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.ShowSoftInput(Control, ShowFlags.Forced);
        }

        public void HideKeyboard()
        {
            Control.RequestFocus();
            var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.None);
        }

        public void EntryClearFocus()
        {
            Control.ClearFocus();

            var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.None);
        }
    }
}
