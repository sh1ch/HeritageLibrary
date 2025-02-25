using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Heritage.Wpf.Controls;

public class EarlessTab : TabControl
{
    static EarlessTab()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(EarlessTab), new FrameworkPropertyMetadata(typeof(EarlessTab)));
    }
}
