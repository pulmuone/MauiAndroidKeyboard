using Android.Text;
using Android.Views.InputMethods;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inputTypes = Android.Text.InputTypes;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler3 : ViewHandler<CustomEntry, AppCompatEditText>
    {
        public static PropertyMapper<CustomEntry, CustomEntryHandler3> PropertyMapper = new PropertyMapper<CustomEntry, CustomEntryHandler3>(ViewHandler.ViewMapper)
        {
            [nameof(CustomEntry.Text)] = MapText,
            [nameof(CustomEntry.TextColor)] = MapTextColor
        };


        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
            //platformView.SetPadding(0, 0, 0, 0);
            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            //platformView.SetTextSize(ComplexUnitType.Sp, 14);
            platformView.ShowSoftInputOnFocus = true; //true: Show Keyboard, false: Hide Keyboard
            platformView.SetSingleLine(true);
            //platformView.InputType = inputTypes.ClassText;
            //platformView.SetOnKeyListener(new MyOnKeyListener(VirtualView));

            platformView.EditorAction += PlatformView_EditorAction;
        }

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var actionId = e.ActionId;
            var evt = e.Event;

            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            {
                VirtualView?.ReturnCommand.Execute(VirtualView?.ReturnCommandParameter);
            }

            e.Handled = true;
        }

        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            // Perform any native view cleanup here
            platformView.EditorAction -= PlatformView_EditorAction;
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }


        private static void MapText(CustomEntryHandler3 handler, CustomEntry entry)
        {
            handler.PlatformView.Text = entry.Text;
            handler.PlatformView?.SetSelection(handler.PlatformView?.Text?.Length ?? 0);
        }

        private static void MapTextColor(CustomEntryHandler3 handler, CustomEntry entry)
        {
            handler.PlatformView?.SetTextColor(entry.TextColor.ToPlatform());
        }


        //기본으로 3개 생성자
        public CustomEntryHandler3(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {

        }

        public CustomEntryHandler3(IPropertyMapper mapper) : base(mapper)
        {

        }

        public CustomEntryHandler3() : base(PropertyMapper)
        {

        }
    }
}
