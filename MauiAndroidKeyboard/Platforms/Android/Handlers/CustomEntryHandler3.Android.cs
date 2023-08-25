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

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler3 : ViewHandler<CustomEntry, AppCompatEditText>
    {
        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

        public static PropertyMapper<CustomEntry, CustomEntryHandler3> PropertyMapper = new(ViewHandler.ViewMapper)
        {
            [nameof(CustomEntry.Text)] = MapText,
            [nameof(CustomEntry.TextColor)] = MapTextColor
        };

        private static void MapText(CustomEntryHandler3 handler, CustomEntry entry)
        {
            handler.PlatformView.Text = entry.Text;
            handler.PlatformView?.SetSelection(handler.PlatformView?.Text?.Length ?? 0);
        }

        private static void MapTextColor(CustomEntryHandler3 handler, CustomEntry entry)
        {
            //handler.PlatformView?.SetTextColor(entry.TextColor.ToPlatform());
        }

        public CustomEntryHandler3(IPropertyMapper mapper) : base(mapper)
        {

        }

        public CustomEntryHandler3() : base(PropertyMapper)
        {
        }


        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
        }

        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            // Perform any native view cleanup here
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }
    }
}
