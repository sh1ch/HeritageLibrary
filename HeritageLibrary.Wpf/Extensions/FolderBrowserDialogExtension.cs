using HeritageLibrary.Windows.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;

namespace HeritageLibrary.Windows.Dialogs;

public static class FolderBrowserDialogExtension
{
    public static DialogResult ShowDialog(this FolderBrowserDialog dialog, Window owner)
    {
        if (owner == null)
        {
            throw new ArgumentNullException("指定したウィンドウは null です。オーナーを正しく設定できません。");
        }

        var handle = new WindowInteropHelper(owner).Handle;

        return dialog.ShowDialog(handle);
    }
}
