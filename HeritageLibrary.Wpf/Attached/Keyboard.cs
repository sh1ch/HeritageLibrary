using Heritage.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Heritage.Wpf.Attached
{
    /// <summary>
    /// <see cref="Keyboard"/> クラスは、キーボードに関連する添付プロパティを定義するクラスです。
    /// </summary>
    public class Keyboard
    {
        /// <summary>
        /// <see cref="IInputElement"/> に対して <see cref="System.Windows.Input.Keyboard.Focus(IInputElement)"/> を与えます。
        /// </summary>
        public static readonly DependencyProperty AutoFocusProperty =
            DependencyProperty.RegisterAttached(
                "AutoFocus", 
                typeof(bool), 
                typeof(Keyboard),
                new PropertyMetadata(false, SetKeyboardFocusOnChangedCallback)
                );

        /// <summary>
        /// <see cref="IInputElement"/> に対して IME の有効化または無効化を設定します。
        /// </summary>
        public static readonly DependencyProperty ImeProperty =
            DependencyProperty.RegisterAttached(
                "Ime",
                typeof(bool),
                typeof(Keyboard),
                new PropertyMetadata(true, SetImeOnChangedCallback)
                );

        public static bool GetAutoFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoFocusProperty);
        }

        public static void SetAutoFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoFocusProperty, value);
        }

        public static bool GetIme(DependencyObject obj)
        {
            return (bool)obj.GetValue(ImeProperty);
        }

        public static void SetIme(DependencyObject obj, bool value)
        {
            obj.SetValue(ImeProperty, value);
        }

        private static void SetKeyboardFocusOnChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                if (sender is UIElement ui)
                {
                    ui.Dispatcher.BeginInvoke(
                        DispatcherPriority.Input,
                        () => { System.Windows.Input.Keyboard.Focus(ui); }
                        );
                }
            }
        }

        private static void SetImeOnChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UIElement ui)
            {
                bool isEnabled = (bool)e.NewValue;

                ui.Dispatcher.BeginInvoke(
                    DispatcherPriority.Input,
                    () => { InputMethod.SetIsInputMethodEnabled(ui, isEnabled); }
                    );
            }
        }

    }
}
