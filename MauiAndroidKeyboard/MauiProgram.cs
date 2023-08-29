
using Microsoft.Maui.Controls.Compatibility.Hosting;
using MauiAndroidKeyboard.Controls;
using CommunityToolkit.Maui;
using MauiAndroidKeyboard.Interfaces;

namespace MauiAndroidKeyboard;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCompatibility() //AddCompatibilityRenderer을 사용 할 경우만
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureMauiHandlers((handlers) =>
			{
#if ANDROID
			//handlers.AddCompatibilityRenderer(typeof(ExtendedEntry),typeof(MauiAndroidKeyboard.Platforms.Android.Renderers.ExtendedEntryRenderer));
			//handlers.AddHandler(typeof(HandlerEntry), typeof(MauiAndroidKeyboard.Platforms.Android.Handlers.CustomEntryHandler));

			//handlers.AddHandler(typeof(HandlerEntry2), typeof(MauiAndroidKeyboard.Platforms.Android.Handlers.CustomEntryHandler2));
			handlers.AddHandler(typeof(MauiAndroidKeyboard.Views.AutoUpdatePage), typeof(MauiAndroidKeyboard.Platforms.Android.Handlers.AutoUpdatePageHandler));

			//handlers.AddHandler(typeof(CustomEntry), typeof(MauiAndroidKeyboard.Platforms.Android.Handlers.CustomEntryHandler3));

			handlers.AddHandler(typeof(HandlerEntry4), typeof(MauiAndroidKeyboard.Platforms.Android.Handlers.CustomEntryHandler4));

			DependencyService.Register<MauiAndroidKeyboard.Interfaces.ISoftwareKeyboardService, MauiAndroidKeyboard.Platforms.Android.Services.SoftwareKeyboardService>();
#endif

#if IOS

#endif
			});

		return builder.Build();
	}
}
