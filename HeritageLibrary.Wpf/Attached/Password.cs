﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Heritage.Wpf.Attached;

/// <summary>
/// <see cref="Password"/> クラスは、パスワードに関連する添付プロパティを定義するクラスです。
/// <para>
/// <see cref="PasswordBox"/> に使用する添付プロパティです。
/// </para>
/// </summary>
public class Password
{
    #region DependencyProperty

    private static readonly DependencyProperty IsAttachedProperty =
        DependencyProperty.RegisterAttached(
            "IsAttached",
            typeof(bool),
            typeof(Password),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.None,
                (s, e) =>
                {
                    if (s is PasswordBox passwordBox)
                    {
                        if (passwordBox == null)
                        {
                            return;
                        }

                        if ((bool)e.OldValue)
                        {
                            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                        }

                        if ((bool)e.NewValue)
                        {
                            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                        }
                    }
                })
            );

    private static readonly DependencyProperty BindablePasswordProperty =
        DependencyProperty.RegisterAttached(
            "BindablePassword",
            typeof(string),
            typeof(Password),
            new FrameworkPropertyMetadata(
                "",
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (s, e) =>
                {
                    if (s is PasswordBox passwordBox)
                    {
                        var newPassword = (string)e.NewValue;

                        if (GetIsAttached(passwordBox) == false)
                        {
                            SetIsAttached(passwordBox, true);
                        }

                        // 例外
                        if (string.IsNullOrEmpty(passwordBox.Password) && 
                            string.IsNullOrEmpty(newPassword) ||
                            passwordBox.Password == newPassword)
                        {
                            return;
                        }

                        passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                        passwordBox.Password = newPassword;
                        passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                    }
                })
            );

    public static bool GetIsAttached(DependencyObject d)
    {
        return (bool)d.GetValue(IsAttachedProperty);
    }

    public static void SetIsAttached(DependencyObject d, bool value)
    {
        d.SetValue(IsAttachedProperty, value);
    }

    public static string GetBindablePassword(DependencyObject d)
    {
        return (string)d.GetValue(BindablePasswordProperty);
    }

    public static void SetBindablePassword(DependencyObject d, string value)
    {
        d.SetValue(BindablePasswordProperty, value);
    }

    #endregion

    private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        var passwordBox = sender as PasswordBox;

        if (passwordBox == null)
        {
            return;
        }

        SetBindablePassword(passwordBox, passwordBox.Password);
    }

}
