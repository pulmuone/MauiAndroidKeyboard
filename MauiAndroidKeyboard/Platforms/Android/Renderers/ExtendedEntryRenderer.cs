using Android.Content;
using Android.Views.InputMethods;
using Android.Widget;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                edittext.SetPadding(0, 0, 0, 0);
                edittext.SetTextIsSelectable(true);
                edittext.SetSelectAllOnFocus(true);
                edittext.ShowSoftInputOnFocus = true; //true: 키보드 보임, false: 키보드 안보임

                var view = (ExtendedEntry)Element;

                view.VirtualKeyboardHandler = this;

                if (view.IsReadOnly == true)
                {
                    edittext.Enabled = false;
                }
                else
                {
                    edittext.Enabled = true;
                }
            }
        }

        public void ShowKeyboard()
        {
            try
            {
                Control.RequestFocus();
                var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
                inputMethodManager.ShowSoftInput(Control, ShowFlags.Forced);
            }
            catch (Exception ex)
            {

            }
        }

        public void HideKeyboard()
        {
            try
            {
                Control.RequestFocus();
                var inputMethodManager = Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
                inputMethodManager.HideSoftInputFromWindow(this.Control.WindowToken, HideSoftInputFlags.None);
            }
            catch (Exception ex)
            {

            }
        }

        public void StatusKeyboard()
        {
            throw new NotImplementedException();
        }
    }
}
