using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Toasts;

/// <summary>
/// <see cref="EasyToast"/> クラスは、簡易なトースト通知をサポートします。
/// </summary>
public static class EasyToast
{
    /// <summary>
    /// トーストを表示します。
    /// </summary>
    /// <param name="title">タイトル。</param>
    /// <param name="text">テキスト。</param>
    public static void Show(string title, string text)
    {
        new ToastContentBuilder()
            .AddText(title)
            .AddText(text)
            .Show();
    }
}
