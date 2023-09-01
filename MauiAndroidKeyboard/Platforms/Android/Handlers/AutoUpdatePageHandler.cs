using Android.Content;
using Android.Util;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * VirtualView : Maui쪽 컨트롤의 property에 접근
 * PlatformView : native view 접근
 */
namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class AutoUpdatePageHandler : PageHandler
    {
        public AutoUpdatePageHandler(Context context) : base()
        {

        }

        //#1
        protected override ContentViewGroup CreatePlatformView()
        {
            return new ContentViewGroup(base.Context);
        }

        //#2, 네이티브 뷰 설정
        protected override void ConnectHandler(ContentViewGroup platformView)
        {
            base.ConnectHandler(PlatformView);

            var activity = this.Context;
            var intent = new Intent(activity, typeof(AutoUpdateActivity));

            AutoUpdateActivity.OnUpdateCompleted += () =>
            {
                (VirtualView as ContentPage).Navigation.PopAsync();
            };

            activity.StartActivity(intent);
        }

        //#3
        protected override void DisconnectHandler(ContentViewGroup platformView)
        {
            base.DisconnectHandler(platformView);
            platformView.Dispose();
        }

        //public override void SetVirtualView(IView view)
        //{
        //    base.SetVirtualView(view);

        //    var activity = this.Context;
        //    var intent = new Intent(activity, typeof(AutoUpdateActivity));

        //    try
        //    {
        //        AutoUpdateActivity.OnUpdateCompleted += () =>
        //        {
        //            (view as ContentPage).Navigation.PopAsync();
        //        };

        //        activity.StartActivity(intent);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
