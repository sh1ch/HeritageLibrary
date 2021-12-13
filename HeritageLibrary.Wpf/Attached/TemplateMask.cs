using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Heritage.Wpf.Attached;

/// <summary>
/// <see cref="TemplateMask"/> クラスは、入力値を制限する添付プロパティを定義するクラスです。
/// <para>
/// <para>
/// <see cref="TextBox"/> に使用する添付プロパティです。
/// </para>
/// </summary>
public class TemplateMask
{
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.RegisterAttached(
            "Type",
            typeof(MaskType),
            typeof(TemplateMask),
            new PropertyMetadata(MaskType.None, (sender, args) =>
            {
                if (sender is not TextBox textBox) return;

                var maskText = (MaskType)args.NewValue switch
                {
                    MaskType.HalfNumeric => "^[0-9]+$",
                    MaskType.HalfAlphanumeric => "^[a-zA-Z0-9]+$",
                    MaskType.HIRAGANA => "^[ぁ-ん!ー]+$",
                    MaskType.KATAKANA => "^[ァ-ヴ!ー]+$",
                    _ => "",
                };

                    // イベントの事前クリア
                    DataObject.RemovePastingHandler(textBox, Mask_Pasting);
                textBox.PreviewTextInput -= Mask_PreviewTextInput;
                textBox.PreviewKeyDown -= Mask_PreviewKeyDown;

                if (maskText != "")
                {
                    SetRegex(textBox, new Regex(maskText, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace));

                        // イベントを登録
                        DataObject.AddPastingHandler(textBox, Mask_Pasting);
                    textBox.PreviewTextInput += Mask_PreviewTextInput;
                    textBox.PreviewKeyDown += Mask_PreviewKeyDown;
                }
                else
                {
                        // 使用されていないときは、添付プロパティをクリアする
                        textBox.ClearValue(RegexProperty);
                }
            }));

    [AttachedPropertyBrowsableForType(typeof(TextBox))]
    public static MaskType GetType(DependencyObject obj)
    {
        return (MaskType)obj.GetValue(TypeProperty);
    }

    [AttachedPropertyBrowsableForType(typeof(TextBox))]
    public static void SetType(DependencyObject obj, MaskType value)
    {
        obj.SetValue(TypeProperty, value);
    }

    private static readonly DependencyPropertyKey RegexPropertyKey =
        DependencyProperty.RegisterAttachedReadOnly(
            "Regex",
            typeof(Regex),
            typeof(TemplateMask),
            new PropertyMetadata());

    public static readonly DependencyProperty RegexProperty =
        RegexPropertyKey.DependencyProperty;

    private static Regex GetRegex(DependencyObject obj)
    {
        return obj.GetValue(RegexProperty) as Regex;
    }

    private static void SetRegex(DependencyObject obj, Regex value)
    {
        obj.SetValue(RegexPropertyKey, value);
    }

    private static void Mask_Pasting(object sender, DataObjectPastingEventArgs e)
    {
        if (sender is not TextBox textBox) return;

        var regex = GetRegex(textBox);

        if (regex == null) return;

        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            var pastedText = e.DataObject.GetData(typeof(string)) as string;
            var proposedText = GetProposedText(textBox, pastedText);

            if (regex.IsMatch(proposedText) == false)
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    private static void Mask_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (sender is not TextBox textBox) return;

        var regex = GetRegex(textBox);

        if (regex == null) return;

        string input = "";

        // PreviewTextInput が発生しない原因不明の例外キー (例:Space) は、ここで処理します
        switch (e.Key)
        {
            case Key.Space:
                input = " ";
                break;
            default:
                break;
        }

        if (input != "")
        {
            var proposedText = GetProposedText(textBox, input);

            if (regex.IsMatch(proposedText) == false)
            {
                e.Handled = true;
            }
        }
    }

    private static void Mask_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        if (sender is not TextBox textBox) return;

        var regex = GetRegex(textBox);

        if (regex == null) return;

        var proposedText = GetProposedText(textBox, e.Text);

        if (regex.IsMatch(proposedText) == false)
        {
            e.Handled = true;
        }
    }

    private static string GetProposedText(TextBox textBox, string newText)
    {
        var text = textBox?.Text ?? "";

        if ((textBox?.SelectionStart ?? -1) != -1)
        {
            text = text.Remove(textBox.SelectionStart, textBox.SelectionLength);
        }

        text = text.Insert(textBox.CaretIndex, newText);

        return text;
    }

}
