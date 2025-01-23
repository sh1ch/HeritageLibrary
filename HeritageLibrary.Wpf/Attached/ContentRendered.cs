using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Heritage.Wpf.Attached;

/// <summary>
/// <see cref="ContentRendered"/> クラスは、<see cref="Window.ContentRendered"/> イベントを <see cref="UserControl"/> クラスで利用するための添付プロパティを定義するクラスです。
/// </summary>
public class ContentRendered
{
    private static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ContentRendered),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.None,
                (s, e) =>
                {
                    if (s is UserControl control)
                    {
                        if (control == null)
                        {
                            Debug.WriteLine($"{typeof(ContentRendered)}.{CommandProperty}: control is null.");
                            return;
                        }

                        var window = System.Windows.Window.GetWindow(control);

                        if (window == null)
                        {
                            Debug.WriteLine($"{typeof(ContentRendered)}.{CommandProperty}: parent window is null.");
                            return;
                        }

                        window.ContentRendered += (sender, args) =>
                        {
                            var command = GetCommand(control);
                            var parameter = GetCommandParameter(control);

                            if (parameter == null)
                            {
                                command?.Execute(e);
                            }
                            else
                            {
                                command?.Execute(parameter);
                            }
                        };
                    }
                })
            );

    private static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.RegisterAttached(
            "CommandParameter",
            typeof(object),
            typeof(ContentRendered),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.None)
            );

    public static ICommand GetCommand(DependencyObject d) => (ICommand)d.GetValue(CommandProperty);
    public static void SetCommand(DependencyObject d, ICommand value) => d.SetValue(CommandProperty, value);

    public static object GetCommandParameter(DependencyObject d) => d.GetValue(CommandParameterProperty);
    public static void SetCommandParameter(DependencyObject d, object value) => d.SetValue(CommandParameterProperty, value);
}
