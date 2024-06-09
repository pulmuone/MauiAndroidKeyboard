using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace MauiAndroidKeyboard.Platforms.iOS.Handlers
{
    public class CustomEntryHandler2 : EntryHandler
    {
        public static PropertyMapper<HandlerEntry2, CustomEntryHandler2> PropertyMapper = new(ViewMapper)
        {
            [nameof(HandlerEntry2.Background)] = MapBackground,
            [nameof(HandlerEntry2.CharacterSpacing)] = MapCharacterSpacing,
            [nameof(HandlerEntry2.ClearButtonVisibility)] = MapClearButtonVisibility,
            [nameof(HandlerEntry2.CursorPosition)] = MapCursorPosition,
            [nameof(HandlerEntry2.FontFamily)] = MapFont,
            [nameof(HandlerEntry2.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
            [nameof(HandlerEntry2.IsPassword)] = MapIsPassword,
            [nameof(HandlerEntry2.IsReadOnly)] = MapIsReadOnly,
            [nameof(HandlerEntry2.IsTextPredictionEnabled)] = MapIsTextPredictionEnabled,
            [nameof(HandlerEntry2.Keyboard)] = MapKeyboard,
            [nameof(HandlerEntry2.MaxLength)] = MapMaxLength,
            [nameof(HandlerEntry2.Placeholder)] = MapPlaceholder,
            [nameof(HandlerEntry2.PlaceholderColor)] = MapPlaceholderColor,
            [nameof(HandlerEntry2.ReturnType)] = MapReturnType,
            [nameof(HandlerEntry2.SelectionLength)] = MapSelectionLength,
            [nameof(HandlerEntry2.Text)] = MapText,
            [nameof(HandlerEntry2.TextColor)] = MapTextColor,
            [nameof(HandlerEntry2.VerticalTextAlignment)] = MapVerticalTextAlignment
        };


        public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested,
            [nameof(HandlerEntry2.ClearFocusRequested)] = MapClearFocusRequested
        };

        public CustomEntryHandler2() : base(PropertyMapper, CommandMapper)
        {
        }

        //public CustomEntryHandler2(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        //{
        //    Debug.WriteLine("test");
        //}

        //public CustomEntryHandler2(IPropertyMapper mapper) : base(mapper)
        //{
        //}


        //protected override MauiTextField CreatePlatformView() => new MauiTextField();


        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here

            platformView.InputView = new UIView();

            PlatformView.Layer.BorderColor = UIKit.UIColor.Gray.CGColor;
            PlatformView.BorderStyle = UIKit.UITextBorderStyle.RoundedRect;
            //PlatformView.BackgroundColor = UIKit.UIColor.White;

            //platformView.InputView.Hidden = false;
            platformView.InputAssistantItem.LeadingBarButtonGroups = null;
            platformView.InputAssistantItem.TrailingBarButtonGroups = null;

            //platformView.EditingDidBegin += (s, e) =>
            //{
            //    platformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            //};

            //platformView.BecomeFirstResponder(); //화면 뜨면 바로 포커스 잡힘
        }

        protected override void DisconnectHandler(MauiTextField platformView)
        {

            //platformView.InputView.Hidden = true;
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }

        //Show Keyboard
        public static void MapShowKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.InputView = null;
            handler.PlatformView.ResignFirstResponder();
            handler.PlatformView.BecomeFirstResponder();
        }

        //Hide Keyboard
        public static void MapHideKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {

            handler.PlatformView.InputView = new UIView();
            //handler.PlatformView.InputAssistantItem.LeadingBarButtonGroups = null;
            //handler.PlatformView.InputAssistantItem.TrailingBarButtonGroups = null;
            handler.PlatformView.ResignFirstResponder();
            handler.PlatformView.BecomeFirstResponder();
        }


        //Clear Focus
        public static void MapClearFocusRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.ResignFirstResponder(); //ㅍㅗㅋㅓㅅㅡ ㅇㅏㅇㅜㅅ
        }
    }
}
