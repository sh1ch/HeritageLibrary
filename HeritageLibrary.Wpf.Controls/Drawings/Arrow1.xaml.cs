using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeritageLibrary.Wpf.Controls.Drawings
{
    /// <summary>
    /// Arrow1.xaml の相互作用ロジック
    /// </summary>
    public partial class Arrow1 : UserControl
    {
        public static readonly DependencyProperty ArrowColorProperty =
            DependencyProperty.Register(
                nameof(ArrowColor),
                typeof(Brush),
                typeof(Arrow1),
                new UIPropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x5F, 0x63, 0x68)))
            );

        public Brush ArrowColor
        {
            get => (Brush)GetValue(ArrowColorProperty);
            set => SetValue(ArrowColorProperty, value);
        }

        public static readonly DependencyProperty EllipseBackgroundProperty =
            DependencyProperty.Register(
                nameof(EllipseBackground),
                typeof(Brush),
                typeof(Arrow1),
                new UIPropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0xEE, 0xEE, 0xEE)))
            );

        public Brush EllipseBackground
        {
            get => (Brush)GetValue(EllipseBackgroundProperty);
            set => SetValue(EllipseBackgroundProperty, value);
        }

        public static readonly DependencyProperty IsFlipHorizontalProperty =
            DependencyProperty.Register(
                nameof(IsFlipHorizontal),
                typeof(bool),
                typeof(Arrow1),
                new UIPropertyMetadata(false)
            );

        public bool IsFlipHorizontal
        {
            get => (bool)GetValue(IsFlipHorizontalProperty);
            set => SetValue(IsFlipHorizontalProperty, value);
        }

        public Arrow1()
        {
            InitializeComponent();
        }
    }
}
