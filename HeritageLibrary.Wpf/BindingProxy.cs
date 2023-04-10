using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Heritage.Wpf;

/// <summary>
/// <see cref="BindingProxy"/> クラスは、バインディング プロパティを中継するクラスです。
/// </summary>
public class BindingProxy : Freezable
{
    public static readonly DependencyProperty ProxyProperty =
        DependencyProperty.Register(
            nameof(Proxy),
            typeof(object),
            typeof(BindingProxy),
            new PropertyMetadata(null)
        );

    public object Proxy
    {
        get => GetValue(ProxyProperty);
        set
        {
            SetValue(ProxyProperty, value);
            Proxy = value;
        }
    }

    protected override Freezable CreateInstanceCore()
    {
        return new BindingProxy();
    }
}
