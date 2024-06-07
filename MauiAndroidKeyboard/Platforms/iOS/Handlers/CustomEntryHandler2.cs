using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
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
            [nameof(ExtendedEntry.Background)] = MapBackground,
            [nameof(ExtendedEntry.CharacterSpacing)] = MapCharacterSpacing,
            [nameof(ExtendedEntry.ClearButtonVisibility)] = MapClearButtonVisibility,
            [nameof(ExtendedEntry.CursorPosition)] = MapCursorPosition,
            [nameof(ExtendedEntry.FontFamily)] = MapFont,
            [nameof(ExtendedEntry.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
            [nameof(ExtendedEntry.IsPassword)] = MapIsPassword,
            [nameof(ExtendedEntry.IsReadOnly)] = MapIsReadOnly,
            [nameof(ExtendedEntry.IsTextPredictionEnabled)] = MapIsTextPredictionEnabled,
            [nameof(ExtendedEntry.Keyboard)] = MapKeyboard,
            [nameof(ExtendedEntry.MaxLength)] = MapMaxLength,
            [nameof(ExtendedEntry.Placeholder)] = MapPlaceholder,
            [nameof(ExtendedEntry.PlaceholderColor)] = MapPlaceholderColor,
            [nameof(ExtendedEntry.ReturnType)] = MapReturnType,
            [nameof(ExtendedEntry.SelectionLength)] = MapSelectionLength,
            [nameof(ExtendedEntry.Text)] = MapText,
            [nameof(ExtendedEntry.TextColor)] = MapTextColor,
            [nameof(ExtendedEntry.VerticalTextAlignment)] = MapVerticalTextAlignment
        };

        public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested,
            [nameof(HandlerEntry2.ClearFocusRequested)] = MapClearFocusRequested
        };

        public CustomEntryHandler2()
        {
        }

        public CustomEntryHandler2(IPropertyMapper mapper) : base(mapper)
        {
        }

        public CustomEntryHandler2(IPropertyMapper mapper, CommandMapper commandMapper) : base(mapper, commandMapper)
        {
        }



        protected override MauiTextField CreatePlatformView() => new MauiTextField();


        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here

            platformView.ResignFirstResponder(); //HideKeyboard
        }

        protected override void DisconnectHandler(MauiTextField platformView)
        {
            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }

        //Show Keyboard
        public static void MapShowKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            //ShowKeyboard
            handler.PlatformView.BecomeFirstResponder(); 
        }

        //Hide Keyboard
        public static void MapHideKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            //Hide
            handler.PlatformView.ResignFirstResponder();
        }


        //Clear Focus
        public static void MapClearFocusRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.ResignFirstResponder();
        }
    }
}
