using Heritage.Wpf.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Heritage.Wpf.Behaviors
{
    /// <summary>
    /// <see cref="AutoKeyboardFocusBehavior"/> クラスは、添付プロパティを定義するクラスです。
    /// <para>
    /// <see cref="IInputElement"/> に対して <see cref="Keyboard.Focus(IInputElement)"/> を与えます。
    /// </para>
    /// </summary>
    public class AutoKeyboardFocusBehavior
    {
        public static readonly DependencyProperty AutoKeyboardFocusProperty =
            DependencyProperty.RegisterAttached(
                "AutoKeyboardFocus", 
                typeof(bool), 
                typeof(AutoKeyboardFocusBehavior),
                new PropertyMetadata(false, SetKeyboardFocusOnChangedCallback)
                );

        public static bool GetAutoKeyboardFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoKeyboardFocusProperty);
        }

        public static void SetAutoKeyboardFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoKeyboardFocusProperty, value);
        }

        private static void SetKeyboardFocusOnChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                UIElement ui = sender as UIElement;

                if (ui != null)
                {
                    ui.Dispatcher.BeginInvoke(
                        DispatcherPriority.Input,
                        () => { Keyboard.Focus(ui); }
                        );
                }
            }
        }

    }
}
